using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.ServiceImpl
{
    internal static class PagingHelper
    {
        internal static dynamic GetPagingInfo(string pSize, string pNum, string orderBy, string isDesc)
        { 
            int pageSize, pageNumber = 0;
            bool isDescending;
            if (string.IsNullOrWhiteSpace(pSize) || int.TryParse(pSize, out pageSize) == false)
                pageSize = 20;

            if (string.IsNullOrWhiteSpace(pNum) || int.TryParse(pNum, out pageNumber) == false)
                pageNumber = 1;

            if (string.IsNullOrWhiteSpace(isDesc) || bool.TryParse(isDesc, out isDescending) == false)
                isDescending = false;

            return new { PageSize = pageSize, PageNumber = pageNumber, OrderBy = orderBy, IsDescending = isDescending };
        }
    }
}
