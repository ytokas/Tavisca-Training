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
    public class EmployeeStorage : IEmployeeStorage
    {
        public Model.Employee Save(Model.Employee employee)
        {
            using (var connection = new SqlConnection(Configurations.EmployeeDbConnectionString))
            {
                var addRemarkCommand = new SqlCommand("spSaveEmployee", connection);
                addRemarkCommand.CommandType = System.Data.CommandType.StoredProcedure;
                addRemarkCommand.Parameters.Add(new SqlParameter("@Title", employee.Title));
                addRemarkCommand.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                addRemarkCommand.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                addRemarkCommand.Parameters.Add(new SqlParameter("@Email", employee.Email));
                addRemarkCommand.Parameters.Add(new SqlParameter("@Password", employee.Password));
                addRemarkCommand.Parameters.Add(new SqlParameter("@Phone", employee.Phone));
                addRemarkCommand.Parameters.Add(new SqlParameter("@JoiningDate", employee.JoiningDate));
                addRemarkCommand.Parameters.Add(new SqlParameter("@Roles", employee.Roles != null ? string.Join(",",employee.Roles) : null));
                var outputParameter = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                outputParameter.Direction = System.Data.ParameterDirection.Output;
                addRemarkCommand.Parameters.Add(outputParameter);
                addRemarkCommand.ExecuteNonQuery();
                employee.Id = outputParameter.Value.ToString();
                return employee;
            }
        }

        public Model.Employee Get(string employeeId)
        {
            using (var connection = new SqlConnection(Configurations.EmployeeDbConnectionString))
            {
                var addRemarkCommand = new SqlCommand("spGetEmployeeById", connection);
                addRemarkCommand.CommandType = System.Data.CommandType.StoredProcedure;
                addRemarkCommand.Parameters.Add(new SqlParameter("@Id", long.Parse(employeeId)));
                var resultReader = addRemarkCommand.ExecuteReader();
                var employee = new Model.Employee();
                if (resultReader.HasRows)
                {
                    while (resultReader.Read())
                    {
                        employee = ParseEmployee(resultReader);
                    }
                }
                return employee;
            }
        }

        public Model.PagedList<Model.Employee> GetEmployees(int pageNumber = 1, int pageSize = 20, string orderBy = "Id", Model.SortingOrder sortingOrder = Model.SortingOrder.DESC)
        {
            using (var connection = new SqlConnection(Configurations.EmployeeDbConnectionString))
            {
                var addRemarkCommand = new SqlCommand("spGetEmployees", connection);
                addRemarkCommand.CommandType = System.Data.CommandType.StoredProcedure;
                addRemarkCommand.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));
                addRemarkCommand.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                addRemarkCommand.Parameters.Add(new SqlParameter("@OrderBy", orderBy));
                addRemarkCommand.Parameters.Add(new SqlParameter("@SortingOrder", sortingOrder.ToString()));
                var resultReader = addRemarkCommand.ExecuteReader();
                int totalRecords = 0;
                var pagedList = new Model.PagedList<Model.Employee>();
                if (resultReader.HasRows)
                {
                    while (resultReader.Read())
                    {
                        pagedList.Add(ParseEmployee(resultReader));
                        totalRecords = (int)resultReader["TotalResults"];
                    }
                }
                pagedList.PageSize = pageSize;
                pagedList.PageNumber = pageNumber;
                pagedList.TotalRecords = totalRecords;
                return pagedList;
            }
        }

        private static Model.Employee ParseEmployee(SqlDataReader resultReader)
        {
            return new Model.Employee()
            {
                Id = ((long)resultReader["Id"]).ToString(),
                Title = resultReader.IsDBNull(resultReader.GetOrdinal("Title")) ?  string.Empty : (string)resultReader["Title"],
                FirstName = (string)resultReader["FirstName"],
                LastName = resultReader.IsDBNull(resultReader.GetOrdinal("LastName")) ? string.Empty : (string)resultReader["LastName"],
                Email = (string)resultReader["Email"],
                Phone = resultReader.IsDBNull(resultReader.GetOrdinal("Phone")) ? string.Empty : (string)resultReader["Phone"],
                Password = (string)resultReader["Password"],
                Roles = GetRoles(resultReader),
                JoiningDate = (DateTime)resultReader["JoiningDate"]
            };
        }

        private static List<string> GetRoles(SqlDataReader resultReader)
        {
            if(resultReader.IsDBNull(resultReader.GetOrdinal("Title")) )
                return new List<string>();
            return ((string)resultReader["Roles"]).Split(',').ToList();
        }


        public Model.Employee GetByEmail(string email)
        {
            using (var connection = new SqlConnection(Configurations.EmployeeDbConnectionString))
            {
                var addRemarkCommand = new SqlCommand("spGetEmployeeByEmail", connection);
                addRemarkCommand.CommandType = System.Data.CommandType.StoredProcedure;
                addRemarkCommand.Parameters.Add(new SqlParameter("@Email", email));
                var resultReader = addRemarkCommand.ExecuteReader();
                var employee = new Model.Employee();
                if (resultReader.HasRows)
                {
                    while (resultReader.Read())
                    {
                        employee = ParseEmployee(resultReader);
                    }
                }
                return employee;
            }
        }
    }
}
