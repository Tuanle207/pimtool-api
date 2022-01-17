using Microsoft.AspNetCore.Mvc;
using PIMTool.Services.Group;
using PIMTool.Shared.Contract;
using PIMTool.Shared.Contract.Common;
using System.Threading.Tasks;

namespace PIMTool.Controllers
{
    [Route(RouteConstants.Group.Api)]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet(RouteConstants.Group.GetAllBasic)]
        public async Task<IActionResult> GetGroups()
        {
            PaginationModel<BasicGroupDto> projects = await _groupService.GetAllGroupsAsync();
            return Ok(projects);
        }
    }
}
