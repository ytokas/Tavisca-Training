using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.Interface;
using Newtonsoft.Json;
using Tavisca.EmployeeManagement.ErrorSpace;
using Tavisca.EmployeeManagement.EnterpriseLibrary;
using System.Data.SqlClient;

namespace Tavisca.EmployeeManagement.MSSqlStorage
{
    public class RemarkStorage : IRemarkStorage
    {
        public Model.Remark AddRemark(string employeeId, Model.Remark remark)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["employeedb"].ConnectionString))
            {
                throw new NotImplementedException();
            }
        }

        public Model.PagedList<Model.Remark> GetRemarks(string employeeId, int pageNumber = 1, int pageSize = 20, string orderBy = "Id", Model.SortingOrder sortingOrder = Model.SortingOrder.DESC)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["employeedb"].ConnectionString))
            {
                throw new NotImplementedException();
            }
        }
    }
}
