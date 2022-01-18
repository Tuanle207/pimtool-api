using PIMTool.Services.Employee;
using PIMTool.Services.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Common.Entity
{
    public class ProjectEmployeeEntity
    {
        public ProjectEmployeeEntity()
        {
            JoinDate = DateTime.Now;
        }

        public DateTime JoinDate { get; set; }
        public long ProjectId { get; set; }
        public long EmployeeId { get; set; }
        public ProjectEntity Project { get; set; }
        public EmployeeEntity Employee { get; set; }
    }
}
