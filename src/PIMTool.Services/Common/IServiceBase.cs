using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Common
{
    public interface IServiceBase<TEntity> where TEntity : EntityBase
    {
    }
}
