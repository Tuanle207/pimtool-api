using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Contract.RequestObject
{
    public class CheckProjectNumberExistenceRes
    {
        public bool Exists { get; set; }
        public string ValidationError { get; set; }
    }
}
