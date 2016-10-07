using ProxyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CentralServer
{
    public class ServiceUtils
    {
        public static string Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version.Build.ToString(); }
        }

        public static string ProjectName()
        {
            return Assembly.GetExecutingAssembly().GetName().FullName;
        }

        public static string VersionWithAllLibraries()
        {
            return ServiceUtils.ProjectName() + System.Environment.NewLine + ProxyUtils.ProjectName();
        }

        public static string VersionWithoutDbContext()
        {
            return ProjectName() + System.Environment.NewLine + ProxyUtils.ProjectName();
        }

    }
}
