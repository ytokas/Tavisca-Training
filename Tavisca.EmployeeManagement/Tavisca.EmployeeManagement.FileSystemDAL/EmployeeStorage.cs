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

namespace Tavisca.EmployeeManagement.FileStorage
{
    public class EmployeeStorage : IEmployeeStorage
    {
        readonly string EXTENSION = ".emp";

        public Model.Employee Save(Model.Employee employee)
        {
            try
            {
                if (Directory.Exists(Configurations.StoragePath) == false)
                {
                    Directory.CreateDirectory(Configurations.StoragePath);
                }

                var filePath = GetFileName(employee.Id);

                File.WriteAllText(filePath, JsonConvert.SerializeObject(employee));
                return employee;
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("data.policy", ex);
                if (rethrow) throw;
                return null;
            }
        }

        public Model.Employee Get(string employeeId)
        {
            try
            {
                if (Directory.Exists(Configurations.StoragePath) == false)
                {
                    throw new DataAccessException("Invalid storage path configuration.");
                }

                var filePath = GetFileName(employeeId);

                var employeeString = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<Model.Employee>(employeeString);
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("data.policy", ex);
                if (rethrow) throw;
                return null;
            }
        }

        public List<Model.Employee> GetAll()
        {
            try
            {
                if (Directory.Exists(Configurations.StoragePath) == false)
                {
                    throw new DataAccessException("Invalid storage path configuration.");
                }

                var employees = new List<Model.Employee>();
                var fileNamesArray = Directory.GetFiles(Configurations.StoragePath, "*", SearchOption.TopDirectoryOnly);

                if (fileNamesArray != null && fileNamesArray.Length > 0)
                {
                    var fileNames = fileNamesArray.ToList();
                    fileNames.RemoveAll(file => Path.GetExtension(file).Equals(EXTENSION, StringComparison.OrdinalIgnoreCase) == false);
                    fileNames.ForEach(fileName =>
                        {
                            var employeeString = File.ReadAllText(fileName);
                            employees.Add(JsonConvert.DeserializeObject<Model.Employee>(employeeString));
                        });
                }
                return employees;
            }
            catch (Exception ex)
            {
                var rethrow = ExceptionPolicy.HandleException("data.policy", ex);
                if (rethrow) throw;
                return null;
            }
        }

        private string GetFileName(string employeeId)
        {
            return string.Format(@"{0}\{1}.emp", Configurations.StoragePath, employeeId);
        }
    }
}
