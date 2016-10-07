using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProxyClasses
{

    /// <summary>
    /// Результат выполнения задачи (Выполнена, Выполняется, Невыполнена)
    /// </summary>
    [DataContract()]
    public enum TaskResultEnum : int
    {
        [EnumMember]
        Any = 0,
        [EnumMember]
        Completed = 1,
        [EnumMember]
        Performing = 2,
        [EnumMember]
        Fail = 4
    }

    /// <summary>
    /// Результат выполнения задачи на удалённом компьютере
    /// </summary>
    [DataContract()]
    public class PrxTaskResult : PrxBaseClass
    {
        /// <summary>
        /// Задача, на которую ссылается результат выполнения
        /// </summary>
        [DataMember()]
        public PrxTask Task { get; set; }

        /// <summary>
        /// Возвращает имя задачи из свойства Task
        /// </summary>
        public string TaskName
        {
            get
            {
                if (this.Task != null)
                {
                    return this.Task.Name;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// Дата-время начала выполнения задачи
        /// </summary>
        [DataMember()]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Продолжительность выполнения задачи (мс)
        /// </summary>
        public int Duration
        {
            get
            {
                return (this.EndTime - this.StartTime).Milliseconds;
            }
        }

        /// <summary>
        /// Дата-время завершения задачи
        /// </summary>
        [DataMember()]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Количество файлов, которые были удалены
        /// </summary>
        [DataMember()]
        public int DeletedFilesNumber { get; set; }

        /// <summary>
        /// Размер удалённых файлов
        /// </summary>
        [DataMember()]
        public long DeletedFilesSize { get; set; }

        /// <summary>
        /// Путь к папке, из которой производилось удаление
        /// </summary>
        [DataMember()]
        public string DeletedFilesPath { get; set; }

        /// <summary>
        /// Результат выполнения задачи
        /// </summary>
        [DataMember()]
        public TaskResultEnum Result { get; set; }

        /// <summary>
        /// Причина неудачи выполнения операции (Exception.Message + Exception.StackTrace)
        /// </summary>
        [DataMember()]
        public string FailReason { get; set; }
    }
}
