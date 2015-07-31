using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.MSSqlStorage
{
    internal static class Configurations
    {
        internal static string EmployeeDbConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["employeedb"].ConnectionString;
            }
        }
    }
}
