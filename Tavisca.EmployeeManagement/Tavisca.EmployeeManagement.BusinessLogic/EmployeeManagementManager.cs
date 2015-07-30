using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.EnterpriseLibrary;
using Tavisca.EmployeeManagement.Interface;
using Tavisca.EmployeeManagement.Model;

namespace Tavisca.EmployeeManagement.BusinessLogic
{
    public class EmployeeManagementManager : IEmployeeManagementManager
    {
        public EmployeeManagementManager(IEmployeeStorage storage, IRemarkStorage remarkStorage)
        {
            _storage = storage;
            _remarkStorage = remarkStorage;
        }

        IEmployeeStorage _storage;
        IRemarkStorage _remarkStorage;

        public Employee Create(Employee employee)
        {
            employee.Validate();
            employee.Id = Guid.NewGuid().ToString();
            employee.Password = CryptoProvider.CreateHash("p@ssw0rd");
            return _storage.Save(employee);
        }

        public Remark AddRemark(string employeeId, Remark remark)
        {
            remark.Validate();
            remark.CreateTimeStamp = DateTime.UtcNow;
            return _remarkStorage.AddRemark(employeeId, remark);
        }
    }
}
