using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.DataContract;

namespace Tavisca.EmployeeManagement.ServiceContract
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [WebGet(UriTemplate = "employee/{employeeId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Employee Get(string employeeId);

        [WebGet(UriTemplate = "employee?pSize={pageSize}&pNum={pageNumber}&sortBy={orderBy}&isDesc={isDescending}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PagedList<Employee> GetEmployees(string pageSize, string pageNumber, string orderBy, string isDescending);

        [WebGet(UriTemplate = "employee/{employeeId}/remark?pSize={pageSize}&pNum={pageNumber}&sortBy={orderBy}&isDesc={isDescending}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        PagedList<Remark> GetRemarks(string employeeId, string pageSize, string pageNumber, string orderBy, string isDescending);
    }
}
