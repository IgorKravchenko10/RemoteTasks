using ProxyClasses;
using RemoteTasksClient.DataSources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteTasksExecutive
{
    public partial class ServiceExecutive : ServiceBase
    {
        private List<PrxTask> _Tasks;
        private DsComputerRows _DsComputerRows;
        private DsTaskRows _DsTaskRows;
        private DsTaskResultRows _DsTaskResultRows;
        private Timer _MainCycleTimer;
        private SrvSettings _HostSettings;
        private string[] _StartArgs;
        private TaskProcessor _TaskProcessor;

        public ServiceExecutive()
        {
            InitializeComponent();
        }

        [MTAThread()]
        public static void Main(params string[] args)
        {
            //Этот метод автоматически вызывается при старте программы
            //Проверим ключ запуска. 
            if (args.Length > 0 && args[0] == "-console")
            {
                //Если ключ задан для консоли, запускаем программу как консольное приложение
                using (ServiceExecutive myServ = new ServiceExecutive())
                {
                    myServ.OnStart(args);
                    Console.WriteLine("Ok");
                    Console.ReadLine();
                }
                return;
            }
            else
            //Запускаем программу как Windows-сервис
            {
                System.ServiceProcess.ServiceBase[] ServicesToRun = null;
                // More than one NT Service may run within the same process. To add
                // another service to this process, change the following line to
                // create a second service object. For example,
                //
                ServicesToRun = new System.ServiceProcess.ServiceBase[] { new ServiceExecutive() };
                System.ServiceProcess.ServiceBase.Run(ServicesToRun);
            }
        }

        protected override void OnStart(string[] args)
        {
            if (args.Length > 0 && args[0] == "-console")
            {
                
            }

            _StartArgs = args;

            //настройка, которую проверяем в новой версии (по умолчанию - true)
            if (Properties.Settings.Default.ShouldUpgrade)
            {
                Properties.Settings.Default.Upgrade();
                //если она не отличается от значения по умолчанию, значит была компиляция и настройки сбросились и надо зачитать настройки из предыдущей версии
                Properties.Settings.Default.ShouldUpgrade = false;
                //в свою очередь надо сказать системе, что настройки из прошлой версии уже были зачитаны
            }

            _HostSettings = new SrvSettings(SrvSettings.HostTypeEnum.TCP, "localhost", 9025, false);
            _DsComputerRows = new DsComputerRows(_HostSettings);
            _DsTaskRows = new DsTaskRows(_HostSettings);
            _DsTaskResultRows = new DsTaskResultRows(_HostSettings);

            _TaskProcessor = new TaskProcessor(_DsTaskResultRows);

            TimerCallback tcbMainCycleTimer = MainCycleSub;
            _MainCycleTimer = new Timer(tcbMainCycleTimer, null, 5000, Properties.Settings.Default.MainCyclePeriod);
        }

        /// <summary>
        /// Подпрограмма главного цикла может выполняться дольше, чем наступит время следующего срабатывания таймера
        /// Поэтому нам нужен триггер, который отключит повторный вызов процедуры главного цикла, пока не завершено выполнение текущей процедуры
        /// </summary>
        private bool _MainCycleTimerIsBusy = false;
        /// <summary>
        /// Главный цикл работы службы
        /// </summary>
        /// <param name="stateInfo"></param>
        private void MainCycleSub(object stateInfo)
        {
            try
            {
                // Если таймер сработал, а прошлый MainCycleSub ещё не завершил свою работу, выходим
                if (_MainCycleTimerIsBusy) { return; }
                // Запускаем главный цикл и сообщаем об єтом в переменной _MainCycleTimerIsBusy
                _MainCycleTimerIsBusy = true;
                // Получаем список задач и сохраняем их в файле
                // Обработка исключений, которые могут возникнуть при выполнении GetTasksAndSaveToFile(), находится внутри
                // что не приводит к завершению главного цикла
                // Сведения об исключениях будут записаны в журнал службы внутри процедуры
                GetTasksFromServerAndSaveToFile();
                // Получаем список задач из файла, который мы записали с помощью предыдущей процедуры
                // Или не записали, если не было Интернета
                ReadAndSetTasksFromFile();

                _TaskProcessor.ExecuteAllTasks(_Tasks);
            }
            catch(Exception ex)
            {
                AddExceptionToLog(ex);
            }
            finally
            {
                _MainCycleTimerIsBusy = false;
            }
        }

        /// <summary>
        /// Получает список задач от сервера и сохраняет их в xml-файл
        /// </summary>
        private void GetTasksFromServerAndSaveToFile()
        {
            // Обкладываем try catch, чтобы отсутствие Интернета или другие исключительные ситуации не повлияли на чтение задач из файла и их выполнения
            try
            {
                PrxComputer prxComputer = GetMyComputer();
                _DsComputerRows.Update(prxComputer);

                _DsTaskRows.LoadData(prxComputer.MacAddress);

                ProxyUtils.SaveTasksToFile(_DsTaskRows.List, GetPathToTasksFile());
            }
            catch (Exception ex)
            {
                AddExceptionToLog(ex);
            }
        }

        private string GetPathToTasksFile()
        {
            string strAppDir = Path.GetDirectoryName((new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath);
            string fileName = Path.Combine(strAppDir, "Tasks.xml");
            return fileName;
        }

        /// <summary>
        /// Читает задачи из файла и сохраняет их в _Tasks
        /// </summary>
        private void ReadAndSetTasksFromFile()
        {
            // Читаем задачи из файла, сохраняем их в taskListFromFile
            List<PrxTask> taskListFromFile = ProxyUtils.ReadTasksFromFile(GetPathToTasksFile());
            if (_Tasks != null)
            {
                // Перебираем зачитанные из файлы задачи
                foreach (PrxTask taskFromFile in taskListFromFile)
                {
                    // Ищем такую же задачу в рабочем списке _Tasks
                    PrxTask task = (from qr in _Tasks where qr.Id == taskFromFile.Id select qr).FirstOrDefault();
                    if (task != null)
                    {
                        // Если нашли, переназначаем коллекцию результатов выполнения из старой задачи, которая находилась в _Tasks,
                        // в новую задачу, которую прочитали из файла
                        if (task.Results != null)
                        {
                            taskFromFile.Results = task.Results;
                            // Поскольку каждый результат выполнения задачи ссылался на задачу из _Tasks, 
                            // надо переназначить ссылку на задачу из файла
                            foreach (PrxTaskResult item in taskFromFile.Results)
                            {
                                item.Task = taskFromFile;
                            }
                        }
                    }
                }
            }
            // Делаем рабочей коллекцией нашу коллекцию, загруженную из файла
            _Tasks = taskListFromFile;
        }

        /// <summary>
        /// Получаем данные о компьютере и возвращаем их в объекте типа PrxComputer
        /// </summary>
        /// <returns></returns>
        private PrxComputer GetMyComputer()
        {
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            PrxComputer prxComputer = new PrxComputer();

            prxComputer.State = ComputerStateEnum.Ready;
            prxComputer.Name = computerProperties.HostName;
            prxComputer.DomainName = computerProperties.DomainName;
            System.Net.IPHostEntry HostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName());
            prxComputer.LocalIPAddress = HostEntry.AddressList[0].Address;
            prxComputer.InternetIPAddress = GetInternetIpAddress();

            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterface adapter2 = (from adapter in nics where adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet select adapter).FirstOrDefault();

            if (adapter2 != null)
            {
                prxComputer.MacAddress = GetMacAddressString(adapter2.GetPhysicalAddress());
            }

            //Данный цикл мы заменили LINQ-запросом, который представлен выше
            //foreach (NetworkInterface adapter in nics)
            //{
            //    if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
            //    {
            //        prxComputer.MacAddress = GetMacAddressString(adapter.GetPhysicalAddress());
            //    }
            //}
            return prxComputer;
        }

        /// <summary>
        /// Преобразует Мас-адрес в строку
        /// </summary>
        /// <param name="address">Мас-адрес</param>
        /// <returns></returns>
        private string GetMacAddressString(PhysicalAddress address)
        {
            string macAddress = "";
            byte[] bytes = address.GetAddressBytes();
            for (int i = 0; i < bytes.Length; i++)
            {
                // Display the physical address in hexadecimal.
                macAddress = macAddress + bytes[i].ToString("X2");
                // Insert a hyphen after each byte, unless we are at the end of the 
                // address.
                if (i != bytes.Length - 1)
                {
                    macAddress = macAddress + "-";
                }
            }
            return macAddress;
        }

        /// <summary>
        /// Возвращает IP-адрес Интернет соединения, через которое компьютер выходит в Интернет
        /// </summary>
        /// <returns></returns>
        private long GetInternetIpAddress()
        {
            try
            {
                string externalIP;
                externalIP = (new WebClient()).DownloadString("http://checkip.dyndns.org/");
                externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}"))
                             .Matches(externalIP)[0].ToString();
                return IPAddress.Parse(externalIP).Address;
            }
            catch { return 0; }
        }

        protected override void OnStop()
        {
        }

        /// <summary>
        /// Сохраняем информацию об исключительной ситуации в лог службы
        /// </summary>
        /// <param name="ex"></param>
        public void AddExceptionToLog(Exception ex)
        {
            AddLog("Exception: " + ex.Message);
            AddLog(ex.StackTrace);
            AddLog("Function or Sub: " + ex.TargetSite.Name + " Current Class: " + ex.Source + System.Environment.NewLine + "Module: " + ex.TargetSite.Module.Name + System.Environment.NewLine + ex.Message);
        }

        /// <summary>
        /// Сохраняем строку в журнал службы
        /// </summary>
        /// <param name="log"></param>
        public void AddLog(string log)
        {
            try
            {
                if (!EventLog.SourceExists("RemoteTasksExecutiveLog"))
                {
                    EventLog.CreateEventSource("RemoteTasksExecutiveLog", "RemoteTasksExecutiveLog");
                }
                eventLog1.Source = "RemoteTasksExecutiveLog";
                eventLog1.WriteEntry(log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ExecuteTaskList(List<PrxTask> taskList)
        {
            foreach (PrxTask item in taskList)
            {
                ExecuteTask(item);
            }
        }

        private void ExecuteTask(PrxTask prxTask)
        {
            
        }
    }
}
