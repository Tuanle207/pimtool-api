using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Contract.ResponseObject
{
    public class CheckEmpExistenceRes
    {
        public Dictionary<string, bool> VisaExists { get; set; }
        public string ValidationError { get; set; }
    }
}
