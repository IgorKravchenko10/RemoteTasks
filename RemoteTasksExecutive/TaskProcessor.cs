using ProxyClasses;
using RemoteTasksClient.DataSources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

enum RecycleFlags : int
{
    // No confirmation dialog when emptying the recycle bin
    SHERB_NOCONFIRMATION = 0x00000001,
    // No progress tracking window during the emptying of the recycle bin
    SHERB_NOPROGRESSUI = 0x00000001,
    // No sound whent the emptying of the recycle bin is complete
    SHERB_NOSOUND = 0x00000004
}

namespace RemoteTasksExecutive
{
    /// <summary>
    /// Класс, отвечающий за выполнение задач на удалённом компьютере в назначенные моменты времени
    /// </summary>
    public class TaskProcessor
    {
        [DllImport("Shell32.dll")]
        // The signature of SHEmptyRecycleBin (located in Shell32.dll)
        static extern int SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

        /// <summary>
        /// Источник данных для сохранения результатов выполнения задач
        /// </summary>
        private DsTaskResultRows _DsTaskResultRows;

        /// <summary>
        /// Создаёт объект типа TaskProcessor для выполнения назначенных задач
        /// </summary>
        /// <param name="dsTaskResultRows">Источник данных для сохранения результатов выполнения задач</param>
        public TaskProcessor(DsTaskResultRows dsTaskResultRows)
        {
            _DsTaskResultRows = dsTaskResultRows;
        }

        /// <summary>
        /// Выполняет те назначенные задания из списка, для которых наступило время выполнения, указанное в параметрах каждой задачи
        /// </summary>
        /// <param name="tasks">Список задач, которые могут быть выполнены, если наступило время выполнения, указанное в параметрах каждой задачи</param>
        public void ExecuteAllTasks(List<PrxTask> tasks)
        {
            foreach (PrxTask task in tasks)
            {
                if (NeedToExecuteNow(task))
                {
                    ExecuteTask(task);
                }
            }
        }

        /// <summary>
        /// Принимает решение, наступил ли момент выполнения задачи
        /// </summary>
        /// <param name="prxTask">Задача, для которой необходимо оценить, наступило ли время её выполнения</param>
        /// <returns></returns>
        private bool NeedToExecuteNow(PrxTask prxTask)
        {
            // Засекаем текущее время
            DateTime currentTime = DateTime.Now;
            // Сохраняем временной промежуток между текущим временем и временем, когда необходимо запустить задачу (только время, без учёта даты)
            TimeSpan timeDifference = currentTime.TimeOfDay - prxTask.TaskTime.TimeOfDay;
            switch (prxTask.StartMomentType)
            {
                // Если задача назначена на конкретную дату-время
                case StartTimeEnum.AtDate:
                    // Переопределяем временной промежуток, учитывая и дату, и время
                    timeDifference = currentTime - prxTask.TaskTime;
                    // Если момент выполнения задачи уже наступил и не прошло более 5 минут
                    if (timeDifference.Milliseconds >= 0 && timeDifference < TimeSpan.FromMinutes(5))
                    {
                        // Эту задачу нужно выполнить
                        return true;
                    }
                    break;
                    
                // Если задача должна выполняться в определённое время в определённый день месяца
                case StartTimeEnum.DayInMonth:
                    // Сохраняем номер сегодняшнего дня в месяце
                    int currentDayInMonth = currentTime.Day;
                    // Если номер сегодняшнего дня в месяце совпадает с номером дня в месяце в задаче
                    // и наступил момент времени выполнения задачи
                    // и прошло не более 5 минут
                    if (currentDayInMonth == prxTask.DayInMonth && timeDifference.Milliseconds >= 0 && timeDifference < TimeSpan.FromMinutes(5))
                    {
                        // Эту задачу нужно выполнить
                        return true;
                    }
                    break;

                // Если задача должна выполняться в определённое время в определённый день недели
                case StartTimeEnum.DayInWeek:
                    // Получаем номер сегодняшнего дня недели
                    int currentDayOfWeek = (int)currentTime.DayOfWeek;
                    // Поскольку у нас дни недели нумеруются с первого по седьмой и первым днём недели считаем понедельник,
                    // исправляем номер воскресенья с нулевого на седьмой. Остальные номера дней не меняются
                    if (currentDayOfWeek == 0) currentDayOfWeek = 7;
                    // Если номер сегодняшнего дня недели совпадает с номером дня недели в задаче
                    // и наступил момент времени выполнения задачи
                    // и прошло не более 5 минут
                    if (currentDayOfWeek == prxTask.DayOfWeek && timeDifference.Milliseconds >= 0 && timeDifference < TimeSpan.FromMinutes(5))
                    {
                        // Эту задачу нужно выполнить
                        return true;
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Выполняет работу, указанную в задаче
        /// </summary>
        /// <param name="prxTask">Задача, в которой в зависимости от свойства WorkType будет выполнена та или иная работа</param>
        private void ExecuteTask(PrxTask prxTask)
        {
            switch (prxTask.WorkType)
            {
                case WorkEnum.CleanRecycleBin:
                    CleanRecycleBin(prxTask);
                    break;
                case WorkEnum.CleanTmpFiles:
                    CleanTmpFiles(prxTask);
                    break;
                case WorkEnum.RemoveDownloadedProgramFiles:
                    RemoveDownloadedProgramFiles();
                    break;
                case WorkEnum.RemoveOldChkdskFiles:
                    RemoveOldChkdskFiles();
                    break;
                case WorkEnum.RemoveSystemErrorMemoryDumpFiles:
                    RemoveSystemErrorMemoryDumpFiles();
                    break;
                case WorkEnum.RemoveTemporaryInstallationFiles:
                    RemoveTemporaryInstallationFiles();
                    break;
                case WorkEnum.RemoveTemporaryInternetFiles:
                    RemoveTemporaryInternetFiles(prxTask);
                    break;
                case WorkEnum.RemoveTemporaryWindowsInstallerFiles:
                    RemoveTemporaryWindowsInstallerFiles();
                    break;
                case WorkEnum.RemoveThumbnails:
                    RemoveThumbnails();
                    break;
                case WorkEnum.RemoveWindowsUpdateLogFile:
                    RemoveWindowsUpdateLogFile();
                    break;
            }
        }

        #region "Выполнение задач"
        private void CleanRecycleBin(PrxTask prxTask)
        {
            CleanFolderGeneralTask(prxTask, "Recycle bin");
        }

        private void CleanTmpFiles(PrxTask prxTask)
        {
            string dir = Environment.GetEnvironmentVariable("SystemRoot") + @"\Temp";
            CleanFolderGeneralTask(prxTask, dir);            
        }

        private void RemoveDownloadedProgramFiles()
        {

        }

        private void RemoveOldChkdskFiles()
        {

        }

        private void RemoveSystemErrorMemoryDumpFiles()
        {
            string dir = Environment.GetEnvironmentVariable("SystemRoot") + @"\memory.dmp";
        }

        private void RemoveTemporaryInstallationFiles()
        {

        }

        private void RemoveTemporaryInternetFiles(PrxTask prxTask)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            CleanFolderGeneralTask(prxTask, path);
        }

        private void RemoveTemporaryWindowsInstallerFiles()
        {

        }

        private void RemoveThumbnails()
        {

        }

        private void RemoveWindowsUpdateLogFile()
        {

        }
        #endregion

        /// <summary>
        /// Оценивает, выполнялась ли уже данная задача в назначенный промежуток времени
        /// </summary>
        /// <param name="prxTask">Задача, которая должна быть выполнена</param>
        /// <param name="atTime">Время, когда должна была выполниться эта задача</param>
        /// <returns></returns>
        private bool IsTaskAlreadyCompleted(PrxTask prxTask, DateTime atTime)
        {
            // Если для данной задачи уже имеются результаты выполнения
            if (prxTask.Results != null)
            {
                // Ищем результат выполнения задачи на указанный в atTime момент времени (не позднее 5 минут с этого момента)
                PrxTaskResult prevResult = (from qr in prxTask.Results where (atTime - qr.StartTime) < TimeSpan.FromMinutes(5) select qr).FirstOrDefault();
                if (prevResult != null)
                {
                    // Если нашли результат, значит, задача уже выполнилась
                    return true;
                }
                else
                {
                    // Если результат не нашли, значит, задача не выполнилась
                    return false;
                }
            }
            else
            {
                // Если коллекция Results == null, значит, задача ещё никогда не выполнялась
                return false;
            }
        }


        /// <summary>
        /// Процедура, которая удаляет файлы по определённому пути
        /// </summary>
        /// <param name="prxTask">Задача, согласно которой производится очистка</param>
        /// <param name="pathToFolder">Путь, в котором удаляются файлы</param>
        private void CleanFolderGeneralTask(PrxTask prxTask, string pathToFolder)
        {
            // Засекаем время
            DateTime startTime = DateTime.Now;
            //Если эта задача уже выполнялась, выходим из процедуры и ничего не делаем
            if (IsTaskAlreadyCompleted(prxTask, startTime))
                return;
            // Создаём результат выполнения задачи
            PrxTaskResult taskResult = CreateTaskResult(prxTask, startTime, pathToFolder);
            try
            {
                // Пытаемся произвести очистку
                if (prxTask.WorkType == WorkEnum.CleanRecycleBin)
                {
                    // Для очистки корзины в порядке исключения вызываем API-функцию,
                    // параметр pathToFolder не используется
                    SHEmptyRecycleBin(IntPtr.Zero, null, RecycleFlags.SHERB_NOSOUND | RecycleFlags.SHERB_NOCONFIRMATION);
                    Console.WriteLine("Корзина очищена " + pathToFolder);
                }
                else
                {
                    // Во всех остальных случаях чистим файлы по указанному пути
                    CleanFolder(pathToFolder);
                    Console.WriteLine("Временные файлы очищены. " + pathToFolder);
                }
            }
            catch (Exception ex)
            {
                // В случае, если при очистке возникла проблема, сохраняем запись о проблеме в результате выполнения задачи
                taskResult.FailReason = ex.Message + System.Environment.NewLine + ex.StackTrace;
                taskResult.Result = TaskResultEnum.Fail;
            }
            // Сохраняем время окончания выполнения задачи
            taskResult.EndTime = DateTime.Now;
            
            // Добавляем результат выполнения в список результатов для нашей задачи
            prxTask.AddResult(taskResult);
            // Отправляем результат выполнения задачи на сервер для сохранения в базе данных
            _DsTaskResultRows.Update(taskResult);
            Console.WriteLine("Результат отправлен на сервер. Задача: " + prxTask.Name);
        }

        /// <summary>
        /// Формирует результат выполнения для указанной задачи
        /// </summary>
        /// <param name="prxTask">Задача, для которой формируется результат выполнения</param>
        /// <param name="startTime">Время, когда началось выполнение задачи</param>
        /// <param name="pathToFolder">Путь, по которому производится очистка</param>
        /// <returns></returns>
        private PrxTaskResult CreateTaskResult(PrxTask prxTask, DateTime startTime, string pathToFolder)
        {
            PrxTaskResult taskResult = new PrxTaskResult()
            {
                DeletedFilesPath = pathToFolder,
                StartTime = startTime,
                Result = TaskResultEnum.Completed,
                Task = prxTask
            };
            return taskResult;
        }

        /// <summary>
        /// Удаляет файлы и папки по указанному пути
        /// </summary>
        /// <param name="pathToFolder">Путь для удаления</param>
        private void CleanFolder(string pathToFolder)
        {
            foreach (string file in Directory.GetFiles(pathToFolder))
                File.Delete(file);

            foreach (string directory in Directory.GetDirectories(pathToFolder))
                Directory.Delete(directory, true);
        }
    }
}
