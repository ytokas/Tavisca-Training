using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.Interface;
using Tavisca.EmployeeManagement.Model;

namespace Tavisca.EmployeeManagement.BusinessLogic
{
    public class EmployeeManager : IEmployeeManager
    {
        public EmployeeManager(IEmployeeStorage storage, IRemarkStorage remarkStorage)
        {
            _storage = storage;
            _remarkStorage = remarkStorage;
        }

        IEmployeeStorage _storage;
        IRemarkStorage _remarkStorage;

        public Employee Get(string employeeId)
        {
            return _storage.Get(employeeId);
        }

        public PagedList<Employee> GetAll(int pageNumber = 1, int pageSize = 20, string orderBy = "Id", Model.SortingOrder sortingOrder = Model.SortingOrder.DESC)
        {
            return _storage.GetEmployees(pageNumber, pageSize , orderBy , sortingOrder);
        }

        public PagedList<Remark> GetRemarks(string employeeId, int pageNumber = 1, int pageSize = 20, string orderBy = "Id", Model.SortingOrder sortingOrder = Model.SortingOrder.DESC)
        {
            return _remarkStorage.GetRemarks(employeeId, pageNumber, pageSize, orderBy, sortingOrder);
        }
    }
}
