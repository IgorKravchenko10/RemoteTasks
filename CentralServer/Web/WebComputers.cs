using CentralServer.DataAdapters;
using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.Web
{
    public class WebComputers : WebBaseClass, IWebComputers
    {
        public List<PrxComputer> GetList()
        {
            return ComputersAdapter.GetList(this.CsContext);
        }
    }
}
