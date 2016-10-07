using CentralServer.DataObjects;
using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.DataAdapters
{
    public class TaskResultsAdapter
    {
        /// <summary>
        /// Получаем список результатов выполнения задач для компьютера с определённым Id
        /// </summary>
        /// <param name="id">Id компьютера</param>
        /// <param name="startDate">Дата начала периода, за который выводится список выполнения задачи</param>
        /// <param name="endDate">Дата конца периода, за который выводится список выполнения задачи</param>
        /// <returns></returns>
        public static List<PrxTaskResult> GetListForComputer(CsContext csContext, int id, DateTime startDate, DateTime endDate)
        {
            DateTime endDateForLinqQuery;
            if (endDate==DateTime.MaxValue)
            {
                endDateForLinqQuery = endDate;
            }
            else
            {
                endDateForLinqQuery = endDate.Date.AddDays(1);
            }
            IQueryable<PrxTaskResult> taskResultsQuery = from taskResult in csContext.TaskResults 
                                                         join task in csContext.Tasks on taskResult.TaskId equals task.TaskId
                                                         where task.ComputerId == id & taskResult.StartTime >= startDate & taskResult.StartTime < endDateForLinqQuery
                                                         select new PrxTaskResult
                                                         {
                                                             Id=taskResult.TaskResultId,
                                                             DeletedFilesNumber=taskResult.DeletedFilesNumber,
                                                             DeletedFilesPath=taskResult.DeletedFilesPath,
                                                             DeletedFilesSize=taskResult.DeletedFilesSize,
                                                             EndTime=taskResult.EndTime,
                                                             FailReason=taskResult.FailReason,
                                                             Result=(TaskResultEnum)taskResult.Result,
                                                             StartTime=taskResult.StartTime,
                                                             Task=new PrxTask
                                                             {
                                                                 Id=task.TaskId,
                                                                 DayInMonth=task.DayInMonth,
                                                                 DayOfWeek=task.DayOfWeek,
                                                                 StartMomentType=(StartTimeEnum)task.StartMomentType,
                                                                 TaskTime=task.TaskTime,
                                                                 WorkType=(WorkEnum)task.WorkType
                                                             }
                                                         };
            List<PrxTaskResult> taskResults = taskResultsQuery.ToList();
            SetTaskNames(taskResults);
            return taskResults;
        }

        /// <summary>
        /// Получаем список результатов выполнения для задачи с определённым Id
        /// </summary>
        /// <param name="id">Id задачи</param>
        /// <param name="startDate">Дата начала периода, за который выводится список выполнения задачи</param>
        /// <param name="endDate">Дата конца периода, за который выводится список выполнения задачи</param>
        /// <returns></returns>
        public static List<PrxTaskResult> GetListForTask(CsContext csContext, int id, DateTime startDate, DateTime endDate)
        {
            IQueryable<PrxTaskResult> taskResultsQuery = from taskResult in csContext.TaskResults
                                                         join task in csContext.Tasks on taskResult.TaskId equals task.TaskId
                                                         where task.TaskId == id && taskResult.StartTime.Date >= startDate && taskResult.StartTime.Date <= endDate
                                                         select new PrxTaskResult
                                                         {
                                                             Id = taskResult.TaskResultId,
                                                             DeletedFilesNumber = taskResult.DeletedFilesNumber,
                                                             DeletedFilesPath = taskResult.DeletedFilesPath,
                                                             DeletedFilesSize = taskResult.DeletedFilesSize,
                                                             EndTime = taskResult.EndTime,
                                                             FailReason = taskResult.FailReason,
                                                             Result = (TaskResultEnum)taskResult.Result,
                                                             StartTime = taskResult.StartTime,
                                                             Task = null
                                                         };
            List<PrxTaskResult> taskResults = taskResultsQuery.ToList();
            return taskResults;
        }

        /// <summary>
        /// Получаем список резутатов выполнения всех задач за определённый период
        /// </summary>
        /// <param name="startDate">Дата начала периода, за который выводится список выполнения задачи</param>
        /// <param name="endDate">Дата конца периода, за который выводится список выполнения задачи</param>
        /// <returns></returns>
        public static List<PrxTaskResult> GetList(CsContext csContext, DateTime startDate, DateTime endDate)
        {
            IQueryable<PrxTaskResult> taskResultsQuery = from taskResult in csContext.TaskResults
                                                         join task in csContext.Tasks on taskResult.TaskId equals task.TaskId 
                                                         join computer in csContext.Computers on task.ComputerId equals computer.ComputerId
                                                         where taskResult.StartTime.Date >= startDate && taskResult.StartTime.Date <= endDate
                                                         select new PrxTaskResult
                                                         {
                                                             Id = taskResult.TaskResultId,
                                                             DeletedFilesNumber = taskResult.DeletedFilesNumber,
                                                             DeletedFilesPath = taskResult.DeletedFilesPath,
                                                             DeletedFilesSize = taskResult.DeletedFilesSize,
                                                             EndTime = taskResult.EndTime,
                                                             FailReason = taskResult.FailReason,
                                                             Result = (TaskResultEnum)taskResult.Result,
                                                             StartTime = taskResult.StartTime,
                                                             Task = new PrxTask
                                                             {
                                                                 Id = task.TaskId,
                                                                 Computer = new PrxComputer
                                                                 {
                                                                     Id=computer.ComputerId,
                                                                     Name=computer.Name,
                                                                     InternetIPAddress=computer.InternetIPAddress,
                                                                     LocalIPAddress=computer.LocalIPAddress,
                                                                     State=(ComputerStateEnum)computer.State
                                                                 },
                                                                 DayInMonth = task.DayInMonth,
                                                                 DayOfWeek = task.DayOfWeek,
                                                                 StartMomentType = (StartTimeEnum)task.StartMomentType,
                                                                 TaskTime = task.TaskTime,
                                                                 WorkType = (WorkEnum)task.WorkType
                                                             }
                                                         };
            List<PrxTaskResult> taskResults = taskResultsQuery.ToList();
            SetTaskNames(taskResults);
            return taskResults;
        }

        private static void SetTaskNames(List<PrxTaskResult> taskResults)
        {
            foreach (PrxTaskResult item in taskResults)
            {
                item.Task.Name = DataObjects.Task.GetTaskName(item.Task.WorkType);
            }
        }
        /// <summary>
        /// Сохраняем данные о выполнении задачи из объекта prxTaskResult в базе данных
        /// </summary>
        /// <param name="csContext">Контекст базы данных</param>
        /// <param name="prxComputer">Объект, из которого производим сохранение</param>
        /// <returns>Возвращаем сущность типа TaskResult, которая была сохранена в базе данных через csContext</returns>
        public static TaskResult Update(CsContext csContext, PrxTaskResult prxTaskResult)
        {
            TaskResult taskResult = GetItem(csContext, prxTaskResult.Id);
            if (taskResult == null)
            {
                taskResult = new TaskResult();
                csContext.TaskResults.Add(taskResult);
            }
            taskResult.DeletedFilesNumber = prxTaskResult.DeletedFilesNumber;
            taskResult.DeletedFilesPath = prxTaskResult.DeletedFilesPath;
            taskResult.DeletedFilesSize = prxTaskResult.DeletedFilesSize;
            taskResult.EndTime = prxTaskResult.EndTime;
            taskResult.FailReason = prxTaskResult.FailReason;
            taskResult.Result = (int)prxTaskResult.Result;
            taskResult.StartTime = prxTaskResult.StartTime;
            taskResult.TaskId = prxTaskResult.Task.Id;

            csContext.SaveChanges();
            return taskResult;
        }

        /// <summary>
        /// Возвращаем результат выполнения задачи в виде класса-сущности 
        /// </summary>
        /// <param name="csContext">Контекст базы данных</param>
        /// <param name="id">Id записи с результатом выполнения задачи в базе данных</param>
        /// <returns>Возвращает результат выполнения задачи</returns>
        public static TaskResult GetItem(CsContext csContext, int id)
        {
            TaskResult taskResult = (from qr in csContext.TaskResults
                                     where qr.TaskResultId == id
                                     select qr).FirstOrDefault();
            return taskResult;
        }
    }
}
