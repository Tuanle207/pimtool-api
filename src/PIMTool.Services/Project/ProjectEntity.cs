using PIMTool.Services.Common;
using PIMTool.Services.Employee;
using PIMTool.Services.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Project
{
    public class ProjectEntity : EntityBase
    {
        public ProjectEntity()
        {
            Employees = new List<EmployeeEntity>();
        }

        public short ProjectNumber { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long GroupdId { get; set; }

        public GroupEntity Group { get; set; }
        public IList<EmployeeEntity> Employees { get; set; }
    }
}
