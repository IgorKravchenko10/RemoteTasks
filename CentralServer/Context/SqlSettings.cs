using CentralServer.DataObjects;
using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer
{
    public class SQLSettings
    {

        public static string CsConnectionString()
        {
            return ConnectionString(Properties.Settings.Default.SqlServerName, Properties.Settings.Default.DatabaseName, Properties.Settings.Default.SqlPassword, Properties.Settings.Default.SqlLogin, (AuthenticationTypeEnum)Properties.Settings.Default.SqlServerAutenticationType);
        }

        public static int CommandTimeout()
        {
            return 30000;
        }

        public static string ConnectionString(string SQLServerName, string DatabaseName, string SQLPassword, string SQLLogin, AuthenticationTypeEnum AuthenticationType)
        {
            if ((AuthenticationTypeEnum)AuthenticationType == AuthenticationTypeEnum.SQLServer)
            {
                return string.Format("Data Source={0};Initial Catalog={1}; Password={2};Persist Security Info=True;User ID={3}", SQLServerName, DatabaseName, SQLPassword, SQLLogin);
            }
            else
            {
                return string.Format("Integrated Security=SSPI; Persist Security Info=False; Initial Catalog={0};Data Source={1}", DatabaseName, SQLServerName);
            }
        }

    }
}
