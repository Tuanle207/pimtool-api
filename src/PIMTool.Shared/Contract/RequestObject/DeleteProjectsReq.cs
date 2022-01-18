using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Contract.RequestObject
{
    public class DeleteProjectsReq
    {
        public Dictionary<long, string> ListIdAndRowVersion { get; set; }
    }
}
