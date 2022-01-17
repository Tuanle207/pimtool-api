using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Contract.Common
{
    public class PaginationFilter
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
    }
}
