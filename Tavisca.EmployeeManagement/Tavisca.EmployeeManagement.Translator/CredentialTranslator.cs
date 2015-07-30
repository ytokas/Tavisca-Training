using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.Translator
{
    public static class CredentialTranslator
    {
        public static Model.Credential ToDomainModel(this DataContract.Credential credential)
        {
            if(credential == null) return null;
            return new Model.Credential()
            {
                Email = credential.Email,
                Password = credential.Password
            };
        }

        public static DataContract.Credential ToDataContract(this Model.Credential credential)
        {
            if (credential == null) return null;
            return new DataContract.Credential()
            {
                Email = credential.Email,
                Password = credential.Password
            };
        }
    }
}
