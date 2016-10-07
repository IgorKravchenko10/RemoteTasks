using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ProxyClasses;
using CentralServer.DataObjects;
using CentralServer.DataAdapters;

namespace CentralServer
{
    [ServiceBehavior(MaxItemsInObjectGraph = int.MaxValue)]
    public class WebBaseClass : IDisposable
    {
        public void Dispose()
        {
        }

        private CsContext _CsContext;

        public CsContext CsContext
        {
            get
            {
                if (_CsContext == null)
                {
                    _CsContext = new CsContext(SQLSettings.CsConnectionString(), SQLSettings.CommandTimeout());
                }
                return _CsContext;
            }
        }
    }

    public class WebCommands : WebBaseClass, IWebCommands
    {
        public string TestService(string userName)
        {
            return "Hello " + userName;
        }

        public PrxComputer GetMyComputerData()
        {
            System.Net.IPHostEntry HostEntry = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName());

            PrxComputer computer = new PrxComputer { Id = 1, LocalIPAddress = HostEntry.AddressList[0].Address, Name = System.Environment.MachineName, State = ComputerStateEnum.Ready };
            return computer;
        }
    }
}

