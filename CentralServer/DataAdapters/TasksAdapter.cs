using CentralServer.DataObjects;
using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.DataAdapters
{
    public class TasksAdapter
    {
        /// <summary>
        /// Возвращает список задач для определённого компьютера по его Id
        /// </summary>
        /// <param name="csContext">Контекст базы данных</param>
        /// <param name="computerId">Id компьютера, для которого получаем список задач</param>
        /// <returns>Возвращаем список задач в виде коллекции PrxTask</returns>
        public static List<PrxTask> GetList(CsContext csContext, int computerId, bool activeOnly)
        {
            IQueryable<PrxTask> tasksQuery = from qr in csContext.Tasks
                                             where qr.ComputerId == computerId & (activeOnly==false | qr.Off==false)
                                             select new PrxTask
                                             {
                                                 Id = qr.TaskId,
                                                 //Во избежание перекрёстных ссылок свойство компьютера не загружаем
                                                 DayInMonth = qr.DayInMonth,
                                                 DayOfWeek = qr.DayOfWeek,
                                                 StartMomentType = (StartTimeEnum)qr.StartMomentType,
                                                 TaskTime = qr.TaskTime,
                                                 WorkType = (WorkEnum)qr.WorkType,
                                                 Off=qr.Off
                                             };
            List<PrxTask> tasks = tasksQuery.ToList();
            // Присваиваем имя задачи по её типу. 
            // Делаем это в цикле, поскольку в LINQ TO SQL запросе это сделать невозможно
            foreach (PrxTask item in tasks)
            {
                item.Name = DataObjects.Task.GetTaskName(item.WorkType);
                item.StartDescription = DataObjects.Task.GetStartDescription(item.StartMomentType, item.TaskTime, item.DayOfWeek, item.DayInMonth);
            }
            return tasks;
        }

        /// <summary>
        /// Возвращает список задач для компьютера по его Мас-адресу
        /// </summary>
        /// <param name="csContext">Контекст базы данных</param>
        /// <param name="macAddress">Мас-адрес компьютера</param>
        /// <returns></returns>
        public static List<PrxTask> GetList(CsContext csContext, string macAddress, bool activeOnly)
        {
            // Получаем компьютер по его Мас-адресу, чтобы передать его Id для получения списка задач
            Computer computer = ComputersAdapter.GetItem(csContext, macAddress);
            // Возвращаем список задач компьютера по его Id
            return GetList(csContext, computer.ComputerId, activeOnly);
        }

        /// <summary>
        /// Возвращаем задачу в виде класса-сущности по её Id в базе данных
        /// </summary>
        /// <param name="csContext">Контекст базы данных</param>
        /// <param name="id">Id задачи в базе данных</param>
        /// <returns></returns>
        public static DataObjects.Task GetItem(CsContext csContext, int id)
        {
            DataObjects.Task task = (from qr in csContext.Tasks where qr.TaskId == id select qr).FirstOrDefault();
            if (task == null)
            {
                throw new ItemNotFoundException(Resources.Messages.TaskNotFound, id.ToString());
            }
            return task;
        }

        /// <summary>
        /// Сохраняем данные по задаче из объекта prxTask в базе данных
        /// </summary>
        /// <param name="csContext">Контекст базы данных</param>
        /// <param name="prxComputer">Объект типа PrxTask, из которого производим сохранение</param>
        /// <returns>Возвращаем сущность типа Task, которая была сохранена в базе данных через csContext</returns>
        public static DataObjects.Task Update(CsContext csContext, PrxTask prxTask)
        {
            if (prxTask.StartMomentType==StartTimeEnum.DayInMonth && prxTask.DayInMonth <= 0)
            {
                throw new UpdateException(Resources.Messages.DayInMonthNotSet);
            }

            DataObjects.Task task;

            try
            {
                task = GetItem(csContext, prxTask.Id);
            }
            catch(ItemNotFoundException ex)
            {
                task = new DataObjects.Task();
                csContext.Tasks.Add(task);
            }

            task.ComputerId = prxTask.Computer.Id;
            task.DayInMonth = prxTask.DayInMonth;
            task.DayOfWeek = prxTask.DayOfWeek;
            task.StartMomentType = (int)prxTask.StartMomentType;
            task.TaskTime = prxTask.TaskTime;
            task.WorkType = (int)prxTask.WorkType;
            task.Off = prxTask.Off;

            csContext.SaveChanges();
            return task;
        }
    }
}
