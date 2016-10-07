using CentralServer.DataAdapters;
using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.Wcf
{
    public class WcfTasks : WcfBaseClass, IWcfTasks
    {
        
        /// <summary>
        /// Возвращает список задач для компьютера по его Id
        /// </summary>
        /// <param name="computerId">Id компьютера в базе данных</param>
        /// <returns></returns>
        public List<PrxTask> GetList(int computerId)
        {
            return TasksAdapter.GetList(this.CsContext, computerId, false);
        }

        /// <summary>
        /// Возвращает список задач для компьютера по Мас-адресу
        /// </summary>
        /// <param name="macAddress">Мас-адрес сетевой карты компьютера</param>
        /// <param name="activeOnly">Истина, если хотим получить только активные задачи, иначе возвращает все задачи</param>
        /// <returns></returns>
        public List<PrxTask> GetListByMacAddress(string macAddress, bool activeOnly)
        {
            try
            {
                return TasksAdapter.GetList(this.CsContext, macAddress, activeOnly);
            }
            catch(Exception ex)
            {
                PrxSimpleException sEx = new PrxSimpleException(ex, ServiceUtils.VersionWithAllLibraries());
                throw new System.ServiceModel.FaultException<PrxSimpleException>(sEx, ex.Message);
            }
        }

        /// <summary>
        /// Сохраняет задачу в базе данных сервера
        /// </summary>
        /// <param name="prxTask">Сериализованный объект, в котором передаётся задача</param>
        /// <returns></returns>
        public int Update(PrxTask prxTask)
        {
            try
            {
                DataObjects.Task task = TasksAdapter.Update(this.CsContext, prxTask);
                return task.TaskId;
            }
            catch(Exception ex)
            {
                PrxSimpleException sEx = new PrxSimpleException(ex, ServiceUtils.VersionWithAllLibraries());
                throw new System.ServiceModel.FaultException<PrxSimpleException>(sEx, ex.Message);
            }
        }

        /// <summary>
        /// Возвращает задачу по её Id в базе данных сервера
        /// </summary>
        /// <param name="id">Id задачи в базе данных</param>
        /// <returns></returns>
        public PrxTask GetItem(int id)
        {
            try
            {
                return TasksAdapter.GetItem(this.CsContext, id).CopyToProxy();
            }
            catch(Exception ex)
            {
                PrxSimpleException sEx = new PrxSimpleException(ex, ServiceUtils.VersionWithAllLibraries());
                throw new System.ServiceModel.FaultException<PrxSimpleException>(sEx, ex.Message);
            }
        }
    }
}
