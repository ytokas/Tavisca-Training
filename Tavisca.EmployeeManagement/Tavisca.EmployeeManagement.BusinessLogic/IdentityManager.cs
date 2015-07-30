using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.EnterpriseLibrary;
using Tavisca.EmployeeManagement.Interface;

namespace Tavisca.EmployeeManagement.BusinessLogic
{
    public class IdentityManager : IIdentityManager
    {
        public IdentityManager(IEmployeeStorage storage)
        {
            _storage = storage;
        }

        IEmployeeStorage _storage;

        public Model.Employee Authenticate(Model.Credential credential)
        {
            // get employee for email
            var employee = _storage.GetByEmail(credential.Email);
            if (employee == null)
                throw new Exception("Invalid email.");
            // check if password matches
            if (CryptoProvider.CompareHash(credential.Password, employee.Password))
                return employee;
            else
                throw new Exception("Invalid password.");
        }

        public bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            // get employee for email
            var employee = _storage.GetByEmail(email);
            if (employee == null)
                throw new Exception("Invalid email.");
            // check if password matches
            if (CryptoProvider.CompareHash(oldPassword, employee.Password))
            {
                // update password
                employee.Password = CryptoProvider.CreateHash(newPassword);
                _storage.Save(employee);
                return true;
            }
            else
                throw new Exception("Invalid password.");
        }
    }
}
