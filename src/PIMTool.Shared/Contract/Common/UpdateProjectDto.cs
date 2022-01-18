using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Contract.Common
{
    public class UpdateProjectDto : NewProjectDto
    {
        public long Id { get; set; }
        public string RowVersion { get; set; }
    }
}
