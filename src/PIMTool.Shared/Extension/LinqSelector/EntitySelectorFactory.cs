using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Shared.Extension
{
    public class EntitySelectorFactory : IEntitySelectorFactory
    {
        private readonly IMapper _mapper;

        public EntitySelectorFactory(IMapper mapper)
        {
            _mapper = mapper;
        }
        public EntitySelector<TEntity, TResult> CreateSelector<TEntity, TResult>()
        {
            return new EntitySelector<TEntity, TResult>(x => _mapper.Map<TEntity, TResult>(x));
        }
    }
}
