using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.EnterpriseLibrary;
using Tavisca.EmployeeManagement.Interface;
using Tavisca.EmployeeManagement.ServiceContract;
using Tavisca.EmployeeManagement.Translator;

namespace Tavisca.EmployeeManagement.ServiceImpl
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService(IEmployeeManager manager)
        {
            _manager = manager;
        }

        IEmployeeManager _manager;

        public DataContract.Employee Get(string employeeId)
        {
            try
            {
                var result = _manager.Get(employeeId);
                if (result == null) return null;
                return result.ToDataContract();
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("service.policy", ex);
                if (rethrow) throw;
                return null;
            }
        }

        public DataContract.PagedList<DataContract.Employee> GetEmployees(string pageSize, string pageNum, string orderBy, string isDescending)
        {
            try
            {
                var pagingInfo = PagingHelper.GetPagingInfo(pageSize, pageNum, orderBy, isDescending);

                var result = _manager.GetAll(pagingInfo.PageNumber, pagingInfo.PageSize, pagingInfo.OrderBy, pagingInfo.IsDescending);
                if (result == null) return null;
                return result.ToDataContract();
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("service.policy", ex);
                if (rethrow) throw;
                return null;
            }

        }

        public DataContract.PagedList<DataContract.Remark> GetRemarks(string employeeId, string pageSize, string pageNum, string orderBy, string isDescending)
        {
            try
            {
                var pagingInfo = PagingHelper.GetPagingInfo(pageSize, pageNum, orderBy, isDescending);

                var result = _manager.GetRemarks(employeeId, pagingInfo.PageNumber, pagingInfo.PageSize, pagingInfo.OrderBy, pagingInfo.IsDescending);
                if (result == null) return null;
                return result.ToDataContract();
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("service.policy", ex);
                if (rethrow) throw;
                return null;
            }
        }
    }
}
