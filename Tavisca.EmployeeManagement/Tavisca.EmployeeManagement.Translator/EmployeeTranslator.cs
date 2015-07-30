using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.Translator
{
    public static class EmployeeTranslator
    {
        public static Model.Employee ToDomainModel(this DataContract.Employee employee)
        {
            if (employee == null) return null;
            return new Model.Employee()
            {
                Id = employee.Id,
                Title = employee.Title,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                JoiningDate = employee.JoiningDate,
                Phone = employee.Phone,
                Roles = employee.Roles
            };
        }

        public static DataContract.Employee ToDataContract(this Model.Employee employee)
        {
            if (employee == null) return null;
            return new DataContract.Employee()
            {
                Id = employee.Id,
                Title = employee.Title,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                JoiningDate = employee.JoiningDate,
                Phone = employee.Phone,
                Roles = employee.Roles
            };
        }

        public static DataContract.PagedList<DataContract.Employee> ToDataContract(this Model.PagedList< Model.Employee> employeeList)
        {
            if (employeeList == null) return null;
            var pagedList = new DataContract.PagedList<DataContract.Employee>();
            pagedList.PageNumber = employeeList.PageNumber;
            pagedList.PageSize = employeeList.PageSize;
            pagedList.TotalRecords = employeeList.TotalRecords;
            pagedList.Results = new List<DataContract.Employee>();
            employeeList.ForEach(employee => 
                {
                    pagedList.Results.Add(new DataContract.Employee()
                    {
                        Id = employee.Id,
                        Title = employee.Title,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email,
                        JoiningDate = employee.JoiningDate,
                        Phone = employee.Phone,
                        Roles = employee.Roles
                    });
                });
            return pagedList;
        }
    }
}
