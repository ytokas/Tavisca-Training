using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tavisca.EmployeeManagement.DataContract
{
    [DataContract]
    public class PagedList<T>
    {
        [DataMember]
        public List<T> Results { get; set; }

        [DataMember]
        public long PageNumber { get; set; }

        [DataMember]
        public long PageSize { get; set; }
        
        [DataMember]
        public long TotalRecords { get; set; }

        public new void ForEach(Action<T> action)
        {
            if (this.Results != null)
                this.Results.ForEach(action);
            else
                foreach (var item in this.Results)
                    action(item);
        }
    }
}
