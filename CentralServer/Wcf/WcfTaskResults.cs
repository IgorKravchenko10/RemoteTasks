using CentralServer.DataAdapters;
using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.Wcf
{
    public class WcfTaskResults : WcfBaseClass, IWcfTaskResults
    {
        /// <summary>
        /// Возвращает список результатов выполнения задач для компьютера с определённым Id в базе данных
        /// </summary>
        /// <param name="id">Id компьютера в базе данных</param>
        /// <param name="startDate">Дата начала периода, за который выводится список выполнения задачи</param>
        /// <param name="endDate">Дата конца периода, за который выводится список выполнения задачи</param>
        /// <returns></returns>
        public List<PrxTaskResult> GetListForComputer(int id, DateTime startDate, DateTime endDate)
        {
            try
            {
                return TaskResultsAdapter.GetListForComputer(this.CsContext, id, startDate, endDate);
            }
            catch (Exception ex)
            {
                PrxSimpleException sEx = new PrxSimpleException(ex, ServiceUtils.VersionWithAllLibraries());
                throw new System.ServiceModel.FaultException<PrxSimpleException>(sEx, ex.Message);
            }
        }

        /// <summary>
        /// Возвращает список результатов выполнения для задачи с определённым Id в базе данных
        /// </summary>
        /// <param name="id">Id задачи в базе данных</param>
        /// <param name="startDate">Дата начала периода, за который выводится список выполнения задачи</param>
        /// <param name="endDate">Дата конца периода, за который выводится список выполнения задачи</param>
        /// <returns></returns>
        public List<PrxTaskResult> GetListForTask(int id, DateTime startDate, DateTime endDate)
        {
            try
            {
                return TaskResultsAdapter.GetListForTask(this.CsContext, id, startDate, endDate);
            }
            catch (Exception ex)
            {
                PrxSimpleException sEx = new PrxSimpleException(ex, ServiceUtils.VersionWithAllLibraries());
                throw new System.ServiceModel.FaultException<PrxSimpleException>(sEx, ex.Message);
            }
        }

        /// <summary>
        /// Возвращает список резутатов выполнения всех задач за определённый период
        /// </summary>
        /// <param name="startDate">Дата начала периода, за который выводится список выполнения задачи</param>
        /// <param name="endDate">Дата конца периода, за который выводится список выполнения задачи</param>
        /// <returns></returns>
        public List<PrxTaskResult> GetList(DateTime startDate, DateTime endDate)
        {
            try
            {
                return TaskResultsAdapter.GetList(this.CsContext, startDate, endDate);
            }
            catch(Exception ex)
            {
                PrxSimpleException sEx = new PrxSimpleException(ex, ServiceUtils.VersionWithAllLibraries());
                throw new System.ServiceModel.FaultException<PrxSimpleException>(sEx, ex.Message);
            }
        }

        /// <summary>
        /// Сохраняет результат выполнения задачи в базе данных сервера
        /// </summary>
        /// <param name="prxTaskResult">Информация о результате выполнения задачи</param>
        /// <returns></returns>
        public int Update(PrxTaskResult prxTaskResult)
        {
            try
            {
                DataObjects.TaskResult taskResult = TaskResultsAdapter.Update(this.CsContext, prxTaskResult);
                return taskResult.TaskResultId;
            }
            catch(Exception ex)
            {
                PrxSimpleException sEx = new PrxSimpleException(ex, ServiceUtils.VersionWithAllLibraries());
                throw new System.ServiceModel.FaultException<PrxSimpleException>(sEx, ex.Message);
            }
        }

        /// <summary>
        /// Возвращает результат выполнения задачи по его Id в базе данных
        /// </summary>
        /// <param name="id">Id результата выполнения задачи в базе данных</param>
        /// <returns></returns>
        public PrxTaskResult GetItem(int id)
        {
            try
            {
                return TaskResultsAdapter.GetItem(this.CsContext, id).CopyToProxy();
            }
            catch(Exception ex)
            {
                PrxSimpleException sEx = new PrxSimpleException(ex, ServiceUtils.VersionWithAllLibraries());
                throw new System.ServiceModel.FaultException<PrxSimpleException>(sEx, ex.Message);
            }
        }
    }
}