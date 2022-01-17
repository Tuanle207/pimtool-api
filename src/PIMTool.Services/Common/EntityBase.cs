using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Common
{
    public class EntityBase
    {
        public long Id { get; set; }
        public int RowVersion { get; set; }
    }
}
