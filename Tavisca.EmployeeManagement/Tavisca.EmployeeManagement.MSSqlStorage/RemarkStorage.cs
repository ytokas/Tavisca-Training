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
            using (var connection = new SqlConnection(Configurations.EmployeeDbConnectionString))
            {
                var addRemarkCommand = new SqlCommand("spAddRemark", connection);
                addRemarkCommand.CommandType = System.Data.CommandType.StoredProcedure;
                addRemarkCommand.Parameters.Add(new SqlParameter("@EmployeeId", long.Parse(employeeId)));
                addRemarkCommand.Parameters.Add(new SqlParameter("@RemarkText", remark.Text));
                addRemarkCommand.Parameters.Add(new SqlParameter("@CreateTimestamp", remark.CreateTimeStamp));
                addRemarkCommand.ExecuteNonQuery();
                return remark;
            }
        }

        public Model.PagedList<Model.Remark> GetRemarks(string employeeId, int pageNumber = 1, int pageSize = 20, string orderBy = "Id", Model.SortingOrder sortingOrder = Model.SortingOrder.DESC)
        {
            using (var connection = new SqlConnection(Configurations.EmployeeDbConnectionString))
            {
                var addRemarkCommand = new SqlCommand("spGetRemarksForEmployee", connection);
                addRemarkCommand.CommandType = System.Data.CommandType.StoredProcedure;
                addRemarkCommand.Parameters.Add(new SqlParameter("@EmployeeId", long.Parse(employeeId)));
                addRemarkCommand.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                addRemarkCommand.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                addRemarkCommand.Parameters.Add(new SqlParameter("@OrderBy", orderBy));
                addRemarkCommand.Parameters.Add(new SqlParameter("@SortingOrder", sortingOrder.ToString()));
                var resultReader = addRemarkCommand.ExecuteReader();
                int totalRecords = 0;
                var pagedList = new Model.PagedList<Model.Remark>();
                if (resultReader.HasRows)
                {
                    while (resultReader.Read())
                    {
                        pagedList.Add(new Model.Remark()
                        {
                            Text = (string)resultReader["RemarkText"],
                            CreateTimeStamp = (DateTime)resultReader["CreateTimestamp"]
                        });
                        totalRecords = (int)resultReader["TotalResults"];
                    }
                }
                pagedList.PageSize = pageSize;
                pagedList.PageNumber = pageNumber;
                pagedList.TotalRecords = totalRecords;
                return pagedList;
            }
        }
    }
}
