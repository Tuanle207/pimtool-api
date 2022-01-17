using PIMTool.Services.Common;
using PIMTool.Services.Employee;
using PIMTool.Services.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Group
{
    public class GroupEntity : EntityBase
    {
        public long LeaderId { get; set; }

        public EmployeeEntity Leader { get; set; }
    }
}
