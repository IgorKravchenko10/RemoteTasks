using CentralServer.DataAdapters;
using CentralServer.DataObjects;
using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.Wcf
{
    public class WcfComputers : WcfBaseClass, IWcfComputers
    {

        /// <summary>
        /// Возвращает коллекцию компьютеров
        /// </summary>
        /// <returns></returns>
        public List<PrxComputer> GetList()
        {
            return ComputersAdapter.GetList(this.CsContext);
        }

        /// <summary>
        /// Сохраняет в базе данных данные о компьютере
        /// </summary>
        /// <param name="prxComputer">Сериализованный объект типа PrxComputer</param>
        /// <returns></returns>
        public int Update(PrxComputer prxComputer)
        {
            Computer computer = ComputersAdapter.UpdateByMacAddress(this.CsContext, prxComputer);
            
            return computer.ComputerId;
        }

        /// <summary>
        /// Возвращает информацию о компьютере по его Id
        /// </summary>
        /// <param name="id">Id компьютера в базе данных</param>
        /// <returns></returns>
        public PrxComputer GetItem(int id)
        {
            return ComputersAdapter.GetItem(this.CsContext, id).CopyToProxy();
        }


        /// <summary>
        /// Возвращает информацию о компьютере по его Mac-адресу
        /// </summary>
        /// <param name="macAddress">Мас-адрес компьютера</param>
        /// <returns></returns>
        public PrxComputer GetItemByMacAddress(string macAddress)
        {
            try
            {
                return ComputersAdapter.GetItem(this.CsContext, macAddress).CopyToProxy();
            }
            catch(Exception ex)
            {
                PrxSimpleException sEx = new PrxSimpleException(ex, ServiceUtils.VersionWithAllLibraries());
                throw new System.ServiceModel.FaultException<PrxSimpleException>(sEx, ex.Message);
            }
        }
    }
}
