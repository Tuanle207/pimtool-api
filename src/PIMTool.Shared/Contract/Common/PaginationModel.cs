using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Contract.Common
{
    public class PaginationModel<T>
    {
        public PaginationModel(IList<T> items, int totalCount)
        {
            Items = items;
            TotalCount = totalCount;
        }

        public PaginationModel(IList<T> items, int totalCount, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public int? TotalCount { get; set; }
        public IList<T> Items { get; set; }
    }
}
