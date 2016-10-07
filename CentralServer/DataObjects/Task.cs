using ProxyClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.DataObjects
{
    /// <summary>
    /// Описывает задачу, которая будет выполняться в определённый момент времени
    /// </summary>
    public class Task
    {
        [Key()]
        public int TaskId { get; set; }

        /// <summary>
        /// Какая работа будет выполняться
        /// </summary>
        public int WorkType { get; set; }

        /// <summary>
        /// Внешний ключ на запись в таблице Computers 
        /// </summary>
        public int ComputerId { get; set; }

        /// <summary>
        /// Объектная ссылка на компьютер, который будет выполнять эту задачу
        /// </summary>
        [ForeignKey("ComputerId")]
        public virtual Computer Computer { get; set; }

        /// <summary>
        /// Определяет конкретный момент времени в неделе, в месяце или конкретную дату, когда должна выполняться задача
        /// </summary>
        public int StartMomentType { get; set; }
        /// <summary>
        /// Время, когда должна быть выполнена задача. Если StartMomentType задано на конкретную дату, содержит и дату, и время. Для остальных - только время
        /// </summary>
        public DateTime TaskTime { get; set; }
        /// <summary>
        /// Значение дня в месяце, когда надо выполнить задачу, если StartMomentType задан равным дню месяца
        /// </summary>
        public int DayInMonth { get; set; }

        /// <summary>
        /// Значение дня недели, когда надо выполнить задачу, если StartMomentType задан равным дню недели
        /// </summary>
        public int DayOfWeek { get; set; }

        /// <summary>
        /// Истина, если задача отключена
        /// </summary>
        public bool Off { get; set; }

        /// <summary>
        /// Возвращает копию задачи в виде объекта PrxTask
        /// </summary>
        /// <returns></returns>
        public PrxTask CopyToProxy()
        {
            PrxTask prxTask = new PrxTask
            {
                Id = this.TaskId,
                WorkType = (WorkEnum)this.WorkType,
                Name = Task.GetTaskName((WorkEnum)this.WorkType),
                Computer = this.Computer.CopyToProxy(),
                DayInMonth = this.DayInMonth,
                DayOfWeek=this.DayOfWeek,
                StartMomentType = (StartTimeEnum)this.StartMomentType,
                TaskTime = this.TaskTime,
                Off=this.Off
            };
            return prxTask;
        }

        /// <summary>
        /// Возвращает название задачи по её типу
        /// </summary>
        public static string GetTaskName(WorkEnum workType)
        {
            switch (workType)
            {
                case WorkEnum.CleanRecycleBin:
                    return Resources.CommonTranslator.CleanRecycleBin;
                case WorkEnum.CleanTmpFiles:
                    return Resources.CommonTranslator.CleanTmpFiles;
                case WorkEnum.RemoveDownloadedProgramFiles:
                    return Resources.CommonTranslator.RemoveDownloadedProgramFiles;
                case WorkEnum.RemoveOldChkdskFiles:
                    return Resources.CommonTranslator.RemoveOldChkdskFiles;
                case WorkEnum.RemoveSystemErrorMemoryDumpFiles:
                    return Resources.CommonTranslator.RemoveSystemErrorMemoryDumpFiles;
                case WorkEnum.RemoveTemporaryInstallationFiles:
                    return Resources.CommonTranslator.RemoveTemporaryInstallationFiles;
                case WorkEnum.RemoveTemporaryInternetFiles:
                    return Resources.CommonTranslator.RemoveTemporaryInternetFiles;
                case WorkEnum.RemoveTemporaryWindowsInstallerFiles:
                    return Resources.CommonTranslator.RemoveTemporaryWindowsInstallerFiles;
                case WorkEnum.RemoveThumbnails:
                    return Resources.CommonTranslator.RemoveThumbnails;
                case WorkEnum.RemoveWindowsUpdateLogFile:
                    return Resources.CommonTranslator.RemoveWindowsUpdateLogFile;
            }
            return "";
        }

        /// <summary>
        /// Возвращает принцип и дату запуска
        /// </summary>
        /// <param name="startMomentType">Принцип запуска</param>
        /// <param name="taskTime">Время дня</param>
        /// <param name="dayOfWeek">День недели</param>
        /// <param name="dayInMonth">День месяца</param>
        /// <returns></returns>
        public static string GetStartDescription(StartTimeEnum startMomentType, DateTime taskTime, int dayOfWeek, int dayInMonth)
        {
            switch (startMomentType)
            {
                case StartTimeEnum.AtDate:
                    return string.Format(Resources.CommonTranslator.AtDate, taskTime);
                case StartTimeEnum.DayInMonth:
                    return string.Format(Resources.CommonTranslator.DayInMonth, dayInMonth, taskTime.ToShortTimeString());
                case StartTimeEnum.DayInWeek:
                    return string.Format(Resources.CommonTranslator.DayInWeek, ProxyUtils.GetNameDayOfWeek(dayOfWeek), taskTime.ToShortTimeString());
            }
            return "";
        }
    }
}
