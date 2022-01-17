using PIMTool.Services.Common;
using PIMTool.Services.Group;
using PIMTool.Services.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Employee
{
    public class EmployeeEntity : EntityBase
    {
        public string Visa { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public IList<ProjectEntity> Projects { get; set; }
        public IList<GroupEntity> LeadingGroups { get; set; }
    }
}
