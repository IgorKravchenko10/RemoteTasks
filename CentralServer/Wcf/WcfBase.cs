using CentralServer.DataAdapters;
using CentralServer.DataObjects;
using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer.Wcf
{
    [ServiceBehavior(MaxItemsInObjectGraph = int.MaxValue)]
    public abstract class WcfBaseClass : IDisposable
    {
        public void Dispose()
        {

        }

        private CsContext _CsContext;

        /// <summary>
        /// Контекст базы данных
        /// </summary>
        public CsContext CsContext
        {
            get
            {
                // Если контекст базы данных не существует, 
                if (_CsContext == null)
                {
                    // создаём его и сохраняем в переменной _CsContext
                    _CsContext = new CsContext(SQLSettings.CsConnectionString(), SQLSettings.CommandTimeout());
                }
                return _CsContext;
            }
        }
    }    
}

