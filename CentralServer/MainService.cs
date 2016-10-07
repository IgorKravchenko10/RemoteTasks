using CentralServer.Wcf;
using CentralServer.Web;
using ProxyClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CentralServer
{
    /// <summary>
    /// Класс службы, которая может запускаться как служба или консольное приложение
    /// </summary>
    public partial class MainService : ServiceBase
    {
        public MainService()
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
                using (MainService myServ = new MainService())
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
                ServicesToRun = new System.ServiceProcess.ServiceBase[] { new MainService() };
                System.ServiceProcess.ServiceBase.Run(ServicesToRun);
            }
        }

        /// <summary>
        /// Таймер для старта web- и wcf-сервисов. Срабатывает с некоторой задержкой после старта службы, чтобы Windows не отклонила службу по таймауту при высокой загрузке в момент запуска операционной системы
        /// </summary>
        private Timer _WcfServicesTimer;
        private string[] _StartArgs;
        protected override void OnStart(string[] args)
        {
            try
            {
                int timeOut = 10000;
                if (args.Length > 0 && args[0] == "-console")
                {
                    //Если мы в режиме консоли, стартуем сервисы с минимальной задержкой
                    timeOut = 300;
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
                
                //Создание базы данных, если не существует
                using (UpdateContext updateContext = new UpdateContext())
                {
                    updateContext.Database.Initialize(true);
                    updateContext.SaveChanges();
                }

                //Присваивание процедуры-делегата StartServices, которая запустится при срабатывании таймера
                System.Threading.TimerCallback tcbWcfServiceTimer = StartServices;
                //Инициализация таймера запуска wcf- и web-сервисов
                _WcfServicesTimer = new System.Threading.Timer(tcbWcfServiceTimer, null, timeOut, 0);
            }
            catch (Exception ex)
            {
                AddExceptionToLog(ex);
            }
        }

        private TaskServices _Services;
        private bool _WcfServicesTimerWasTick = false;

        private void StartServices(object stateInfo)
        {
            try
            {
                if (_WcfServicesTimerWasTick)
                {
                    return;
                }
                _WcfServicesTimerWasTick = true;

                if (string.IsNullOrEmpty(Properties.Settings.Default.SrvHTTPHostName))
                    Properties.Settings.Default.SrvHTTPHostName = "localhost";
                if (string.IsNullOrEmpty(Properties.Settings.Default.SrvTCPHostName))
                    Properties.Settings.Default.SrvTCPHostName = "localhost";

                //Создаём объект для хранения web- и wcf-сервисов
                _Services = new TaskServices(Properties.Settings.Default.IsSecureConnection, Properties.Settings.Default.CertificateName);
                //Добавляем и запускаем сервисы
                _Services.AddWebServiceHost(typeof(WebCommands), typeof(IWebCommands), _Services.WebSettings.CommandsSuffix);

                _Services.AddWebServiceHost(typeof(WebComputers), typeof(IWebComputers), _Services.WebSettings.ComputersSuffix);

                _Services.AddServiceHost(typeof(WcfComputers), typeof(IWcfComputers), _Services.WebSettings.ComputersSuffix);

                _Services.AddServiceHost(typeof(WcfTasks), typeof(IWcfTasks), _Services.WebSettings.TasksSuffix);

                _Services.AddServiceHost(typeof(WcfTaskResults), typeof(IWcfTaskResults), _Services.WebSettings.TaskResultsSuffix);

                AddLog("All services started OK! " + ServiceUtils.VersionWithoutDbContext());
                Console.WriteLine("All services started OK!" + ServiceUtils.VersionWithoutDbContext());

            }
            catch (Exception ex)
            {
                AddExceptionToLog(ex);
            }
        }

        protected override void OnStop()
        {
        }

        public void AddExceptionToLog(Exception ex)
        {
            AddLog("Exception: " + ex.Message);
            AddLog(ex.StackTrace);
            AddLog("Function or Sub: " + ex.TargetSite.Name + " Current Class: " + ex.Source + System.Environment.NewLine + "Module: " + ex.TargetSite.Module.Name + System.Environment.NewLine + ex.Message);
        }

        public void AddLog(string log)
        {
            try
            {
                if (!EventLog.SourceExists("RemoteTasksLog"))
                {
                    EventLog.CreateEventSource("RemoteTasksLog", "RemoteTasksLog");
                }
                eventLog1.Source = "RemoteTasksLog";
                eventLog1.WriteEntry(log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Класс, хранящий коллекции запущенных web- и wcf-сервисов. Мы в основном работает с wcf-сервисами, которые предоставляют больше возможностей, легче программируются, имеют более компактный код и более компактный трафик. web-сервисы понадобятся в дальнейшем для взаимодействия с неWindows платформами
        /// </summary>
        public class TaskServices
        {
            /// <summary>
            /// Коллекция запущенных wcf-сервисов
            /// </summary>
            public List<ServiceHost> ServiceHosts { get; set; }
            /// <summary>
            /// Коллекция запущенных web-сервисов
            /// </summary>
            public List<WebServiceHost> WebServiceHosts { get; set; }

            private SrvSettings _TcpSettings;
            /// <summary>
            /// Настройки wcf-сервисов, использующих net.tcp-протокол
            /// </summary>
            public SrvSettings TcpSettings
            {
                get { return _TcpSettings; }
            }

            private SrvSettings _WebSettings;
            /// <summary>
            /// Настройки web-сервисов, использующих http-протокол (web-сервисы используют только этот протокол)
            /// </summary>
            public SrvSettings WebSettings
            {
                get { return _WebSettings; }
            }

            private SrvSettings _HttpSettings;
            /// <summary>
            /// Настройки wcf-сервисов, использующих http-протокол
            /// </summary>
            public SrvSettings HttpSettings
            {
                get { return _HttpSettings; }
            }

            private bool _IsSecureConnection;
            /// <summary>
            /// признак безопасного соединения
            /// </summary>
            public bool IsSecureConnection
            {
                get { return _IsSecureConnection; }
            }

            private string _CertificateName;
            /// <summary>
            /// Имя SSL-сертификата для безопасного соединения
            /// </summary>
            public string CertificateName
            {
                get { return _CertificateName; }
            }

            /// <summary>
            /// Создание объекта для хранения web- и wcf-сервисов
            /// </summary>
            /// <param name="isSecureConnection">Признак безопасного соединения</param>
            /// <param name="certificateName">Имя SSL-сертификата для безопасного соединения</param>
            public TaskServices(bool isSecureConnection, string certificateName = "")
            {
                // Создаем коллекции сервисов и сохраняем в перменных
                this.ServiceHosts = new List<ServiceHost>();
                this.WebServiceHosts = new List<WebServiceHost>();

                _IsSecureConnection = isSecureConnection;
                _CertificateName = certificateName;
                //Создаем объекты с настройками подключения web-сервисов
                _TcpSettings = new SrvSettings(SrvSettings.HostTypeEnum.TCP, Properties.Settings.Default.SrvTCPHostName, Properties.Settings.Default.SrvTCPHostPort, this.IsSecureConnection, this.CertificateName);
                _HttpSettings = new SrvSettings(SrvSettings.HostTypeEnum.HTTP, Properties.Settings.Default.SrvHTTPHostName, Properties.Settings.Default.SrvHTTPHostPort, this.IsSecureConnection, this.CertificateName);
                _WebSettings = new SrvSettings(SrvSettings.HostTypeEnum.Web, Properties.Settings.Default.SrvHTTPHostName, Properties.Settings.Default.SrvWebHostPort, this.IsSecureConnection, this.CertificateName);
            }

            /// <summary>
            /// Создаёт новый wcf-сервис и добавляет его в коллекцию wcf-сервисов
            /// </summary>
            /// <param name="serviceType">Класс, который реализует wcf-сервис (тип сервиса), например, наш класс WebCommands</param>
            /// <param name="implementedContract">Интерфейс, который воплощается классом wcf-сервиса. Класс wcf-сервиса передан в предыдущем параметре </param>
            /// <param name="serviceSuffix">Суффикс url-адреса, указывающий на вызов данного сервиса</param>
            /// <returns></returns>
            public ServiceHost AddServiceHost(System.Type serviceType, System.Type implementedContract, string serviceSuffix)
            {
                //Создаём новый wcf-сервис и сохраняем в переменную serviceHost
                ServiceHost serviceHost = new ServiceHost(serviceType, new Uri(_HttpSettings.GetUrlString(serviceSuffix)));
                if (this.IsSecureConnection)
                {
                    serviceHost.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, this.CertificateName);
                    serviceHost.Credentials.ClientCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.PeerTrust;
                }

                //Добавляем конечную точку wcf-сервиса для реализации net.tcp-протокола
                serviceHost.AddServiceEndpoint(implementedContract, TcpSettings.Binding, TcpSettings.GetUrlString(serviceSuffix));
                //Добавляем конечную точку wcf-сервиса для реализации http-протокола
                serviceHost.AddServiceEndpoint(implementedContract, HttpSettings.Binding, HttpSettings.GetUrlString(serviceSuffix));
                //Разрешить публикацию метаданных о сервисе
                serviceHost.Description.Behaviors.Add(new System.ServiceModel.Description.ServiceMetadataBehavior { HttpGetEnabled = true });
                //Поднимаем wcf-сервис
                serviceHost.Open();
                //Добавляем в коллекцию сервисов  наш сервис
                this.ServiceHosts.Add(serviceHost);
                //Возвращаем serviceHost, который подняли
                return serviceHost;
            }
            
            /// <summary>
            /// Создаёт новый web-сервис и добавляет его в коллекцию web-сервисов
            /// </summary>
            /// <param name="serviceType">Класс, который реализует web-сервис (тип сервиса), например, наш класс WebCommands</param>
            /// <param name="implementedContract">Интерфейс, который воплощается классом web-сервиса. Класс web-сервиса передан в предыдущем параметре </param>
            /// <param name="serviceSuffix">Суффикс url-адреса, указывающий на вызов данного сервиса</param>
            /// <returns></returns>
            public WebServiceHost AddWebServiceHost(System.Type serviceType, System.Type implementedContract, string serviceSuffix)
            {
                //Создаём новый web-сервис и сохраняем в переменную serviceHost
                WebServiceHost webServiceHost = new WebServiceHost(serviceType, new Uri(_WebSettings.GetUrlString(serviceSuffix)));
                //Добавляем конечную точку web-сервиса для реализации http-протокола
                webServiceHost.AddServiceEndpoint(implementedContract, _WebSettings.Binding, "");
                if (this.IsSecureConnection)
                {
                    webServiceHost.Credentials.ServiceCertificate.SetCertificate(StoreLocation.LocalMachine, StoreName.My, X509FindType.FindBySubjectName, this.CertificateName);
                }

                /// Производим дополнительные настройки web-сервиса
                WebBihaviorTune(webServiceHost);
                //Поднимаем web-сервис
                webServiceHost.Open();
                //Добавляем web-сервис в коллекцию web-сервисов
                this.WebServiceHosts.Add(webServiceHost);
                return webServiceHost;
            }
            
            /// <summary>
            /// Производим дополнительные настройки web-сервиса
            /// </summary>
            /// <param name="WebHost"></param>
            private void WebBihaviorTune(WebServiceHost WebHost)
            {
                ////Разрешить публикацию метаданных о сервисе
                WebHost.Description.Behaviors.Find<ServiceDebugBehavior>().HttpHelpPageEnabled = true;
                //Находим первую конечную точку сервиса
                ServiceEndpoint EP1 = WebHost.Description.Endpoints[0];

                WebHttpBehavior webBehavior = EP1.Behaviors.Find<WebHttpBehavior>();

                if (webBehavior == null)
                {
                    webBehavior = new WebHttpBehavior();
                    webBehavior.AutomaticFormatSelectionEnabled = true;
                    EP1.Behaviors.Add(webBehavior);
                }

                webBehavior.AutomaticFormatSelectionEnabled = false;
                webBehavior.DefaultOutgoingRequestFormat = WebMessageFormat.Json;
                webBehavior.DefaultOutgoingResponseFormat = WebMessageFormat.Json;
                webBehavior.HelpEnabled = true;

                //Dim stp As ServiceDebugBehavior = WebUOMHost.Description.Behaviors.Find(Of ServiceDebugBehavior)()
                //stp.HttpHelpPageEnabled = False
            }

            /// <summary>
            /// Перебираем все сервисы в коллекциях и закрываем их
            /// </summary>
            public void CloseAllServices()
            {
                foreach (var service in this.ServiceHosts)
                {
                    service.Close();
                }

                foreach (var service in this.WebServiceHosts)
                {
                    service.Close();
                }
            }
        }
    }
}
