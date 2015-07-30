using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.EnterpriseLibrary
{
    public static class CryptoProvider
    {
        public static string CreateHash(string plaintext)
        {
            return Cryptographer.CreateHash("MD5CryptoServiceProvider", plaintext);
        }

        public static bool CompareHash(string plaintext, string hashedText)
        {
            return Cryptographer.CompareHash("MD5CryptoServiceProvider", plaintext, hashedText);
        }
    }
}
