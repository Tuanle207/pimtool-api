using AutoMapper;
using PIMTool.Services.Common;
using PIMTool.Shared.Contract.Common;
using PIMTool.Shared.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Group
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _uow;
        private readonly IEntitySelectorFactory _selectorFactory;

        public GroupService(IUnitOfWork uow, IEntitySelectorFactory selectorFactory)
        {
            _uow = uow;
            _selectorFactory = selectorFactory;
        }

        public async Task<PaginationModel<BasicGroupDto>> GetAllGroupsAsync()
        {
            var selector = _selectorFactory.CreateSelector<GroupEntity, BasicGroupDto>();
            IList<BasicGroupDto> items = await _uow.Groups.GetAllAsync(selector);
            int totalCount = items.Count;

            PaginationModel<BasicGroupDto>  result = new PaginationModel<BasicGroupDto>(items, totalCount);

            return result;
        }
    }
}
