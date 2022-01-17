using PIMTool.Shared.Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Contract.RequestObject
{
    public class ProjectFilterReq : PaginationFilter
    {
        public string Name { get; set; }
        public short? Number { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
