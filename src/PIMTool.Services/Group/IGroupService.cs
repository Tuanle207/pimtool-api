using PIMTool.Services.Common;
using PIMTool.Shared.Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Group
{
    public interface IGroupService : IServiceBase
    {
        Task<PaginationModel<BasicGroupDto>> GetAllGroupsAsync();
    }
}
