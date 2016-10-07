using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProxyClasses;

namespace CentralServer.DataObjects
{
    /// <summary>
    /// Класс-сущность, отражающая результат выполнения задачи
    /// </summary>
    public class TaskResult
    {
        /// <summary>
        /// Id результата выполнения задачи в базе данных
        /// </summary>
        [Key()]
        public int TaskResultId { get; set; }

        /// <summary>
        /// Внешний ключ на запись задачи в таблице Tasks
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Ссылка на задачу
        /// </summary>
        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }

        /// <summary>
        /// Дата-время начала выполнения задачи
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Дата-время завершения задачи
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Количество файлов, которые были удалены
        /// </summary>
        public int DeletedFilesNumber { get; set; }

        /// <summary>
        /// Размер удалённых файлов
        /// </summary>
        public long DeletedFilesSize { get; set; }

        /// <summary>
        /// Путь к папке, из которой производилось удаление
        /// </summary>
        public string DeletedFilesPath { get; set; }

        /// <summary>
        /// Результат выполнения задачи
        /// </summary>
        public int Result { get; set; }

        /// <summary>
        /// Причина неудачи выполнения операции (Exception.Message + Exception.StackTrace)
        /// </summary>
        public string FailReason { get; set; }

        /// <summary>
        /// Возвращает копию задачи в виде объекта PrxTaskResult
        /// </summary>
        /// <returns></returns>
        public PrxTaskResult CopyToProxy()
        {
            PrxTaskResult prxTaskResult = new PrxTaskResult
            {
                Id = this.TaskResultId,
                Task = this.Task.CopyToProxy(),
                DeletedFilesNumber = this.DeletedFilesNumber,
                DeletedFilesPath = this.DeletedFilesPath,
                DeletedFilesSize = this.DeletedFilesSize,
                EndTime = this.EndTime,
                FailReason = this.FailReason,
                Result = (TaskResultEnum)this.Result,
                StartTime = this.StartTime
            };
            return prxTaskResult;
        }
    }
}
