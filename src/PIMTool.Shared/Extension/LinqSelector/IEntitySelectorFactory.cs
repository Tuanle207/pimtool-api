using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Extension
{
    public interface IEntitySelectorFactory
    {
        EntitySelector<TEntity, TResult> CreateSelector<TEntity, TResult>();
    }
}
