using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Contract.Common
{
    public class NewProjectDto
    {
        public short ProjectNumber { get; set; }

        public string Name { get; set; }

        public string Customer { get; set; }

        public long GroupId { get; set; }

        public string Employees { get; set; }

        public string Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
