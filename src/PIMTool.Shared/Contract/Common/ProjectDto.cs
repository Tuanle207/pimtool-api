using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Contract.Common
{
    public class ProjectDto : DtoBase
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Customer { get; set; }
        public DateTime StartDate { get; set; }
        public int RowVersion { get; set; }
    }
}
