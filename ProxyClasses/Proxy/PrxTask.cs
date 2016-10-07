using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProxyClasses
{
    /// <summary>
    /// Задачи, которые должен выполнять удалённый компьютер
    /// </summary>
    [DataContract]
    public enum WorkEnum : int
    {
        [EnumMember]
        Any = 0,
        [EnumMember]
        CleanRecycleBin = 1,
        [EnumMember]
        CleanTmpFiles = 2,
        [EnumMember]
        RemoveSystemErrorMemoryDumpFiles = 4,
        [EnumMember]
        RemoveThumbnails = 8,
        [EnumMember]
        RemoveOldChkdskFiles = 16,
        [EnumMember]
        RemoveTemporaryInstallationFiles = 32,
        [EnumMember]
        RemoveTemporaryInternetFiles = 64,
        [EnumMember]
        RemoveDownloadedProgramFiles = 128,
        [EnumMember]
        RemoveTemporaryWindowsInstallerFiles = 256,
        [EnumMember]
        RemoveWindowsUpdateLogFile = 512
    }

    /// <summary>
    /// Определяет конкретный момент времени в неделе, в месяце или конкретную дату
    /// </summary>
    [DataContract]
    public enum StartTimeEnum : int
    {
        /// <summary>
        /// На определённую дату
        /// </summary>
        /// <remarks></remarks>
        [EnumMember]
        AtDate = 0,
        /// <summary>
        /// Номер дня в неделе
        /// </summary>
        /// <remarks></remarks>
        [EnumMember]
        DayInWeek = 1,
        /// <summary>
        /// Номер дня в месяце
        /// </summary>
        /// <remarks></remarks>
        [EnumMember]
        DayInMonth = 2
    }

    /// <summary>
    /// Описывает задачу, которая будет выполняться в определённый момент времени
    /// </summary>
    [DataContract]
    public class PrxTask : PrxBaseDictionary
    {
        /// <summary>
        /// Какая работа будет выполняться
        /// </summary>
        [DataMember]
        public WorkEnum WorkType { get; set; }
        /// <summary>
        /// Компьютер, который должен выполнить работу
        /// </summary>
        [DataMember]
        public PrxComputer Computer { get; set; }
        /// <summary>
        /// Определяет конкретный момент времени в неделе, в месяце или конкретную дату, когда должна выполняться задача
        /// </summary>
        [DataMember]
        public StartTimeEnum StartMomentType { get; set; }

        /// <summary>
        /// Описывает в свободном текстовом формате момент запуска задачи
        /// </summary>
        [DataMember]
        public string StartDescription { get; set; }
        /// <summary>
        /// Время, когда должна быть выполнена задача. Если StartMomentType задано на конкретную дату, содержит и дату, и время. Для остальных - только время
        /// </summary>
        [DataMember]
        public DateTime TaskTime { get; set; }
        /// <summary>
        /// Значение дня в месяце, когда надо выполнить задачу, если StartMomentType задан равным дню месяца
        /// </summary>
        [DataMember]
        public int DayInMonth { get; set; }

        /// <summary>
        /// Значение дня недели, когда надо выполнить задачу, если StartMomentType задан равным дню недели
        /// </summary>
        [DataMember]
        public int DayOfWeek { get; set; }

        /// <summary>
        /// Истина, если задача отключена (неактивна)
        /// </summary>
        [DataMember]
        public bool Off { get; set; }
        
        /// <summary>
        /// Перечень результатов выполнения задачи. Не сериализуется при передаче во избежание циклических ссылок
        /// между PrxTask и PrxTaskResult в Json
        /// </summary>
        public List<PrxTaskResult> Results { get; set; }

        /// <summary>
        /// Добавляет новый результат выполнения данной задачи в перечень результатов
        /// </summary>
        /// <param name="prxTaskResult"></param>
        public void AddResult(PrxTaskResult prxTaskResult)
        {
            // Если коллекция Results не существует, создаём её
            if (this.Results == null) this.Results = new List<PrxTaskResult>();
            // Добавляем результат в коллекцию
            this.Results.Add(prxTaskResult);
        }

        /// <summary>
        /// Имя компьютера из свойства Computer (для удобства отображения в GridView)
        /// </summary>
        public string ComputerName
        {
            get
            {
                if (this.Computer != null)
                {
                    return this.Computer.Name;
                }
                else
                {
                    return "";
                }                
            }
        }
    }
}
