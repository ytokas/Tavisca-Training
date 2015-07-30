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
    public class IdentityService : IIdentityService
    {
        public IdentityService(IIdentityManager manager)
        {
            _manager = manager;
        }

        IIdentityManager _manager;

        public DataContract.Employee Authenticate(DataContract.Credential credential)
        {
            try
            {
                var result = _manager.Authenticate(credential.ToDomainModel());
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

        public bool ChangePassword(DataContract.ChangePasswordRequest request)
        {
            try
            {
                return _manager.ChangePassword(request.Email, request.OldPassword, request.NewPassword);
            }
            catch (Exception ex)
            {
                Exception newEx;
                var rethrow = ExceptionPolicy.HandleException("service.policy", ex, out newEx);
                return false;
            }
        }
    }
}
