using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavisca.EmployeeManagement.Model;

namespace Tavisca.EmployeeManagement.Interface
{
    public interface IRemarkStorage
    {
        Remark AddRemark(string employeeId, Remark remark);

        PagedList<Remark> GetRemarks(string employeeId, int pageNumber = 1, int pageSize = 20, string orderBy = "Id", SortingOrder sortingOrder = SortingOrder.DESC);
    }
}
