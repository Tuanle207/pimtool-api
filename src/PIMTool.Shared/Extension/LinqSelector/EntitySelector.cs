using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace PIMTool.Shared.Extension
{
    public class EntitySelector<TEntity, TResult>
    {

        public EntitySelector(Expression<Func<TEntity, TResult>> expression)
        {
            Expression = expression;
        }

        public Expression<Func<TEntity, TResult>> Expression { get; }
    }
}
