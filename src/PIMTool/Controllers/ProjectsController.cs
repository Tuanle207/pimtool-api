using Microsoft.AspNetCore.Mvc;
using PIMTool.Services.Employee;
using PIMTool.Services.Project;
using PIMTool.Shared.Contract;
using PIMTool.Shared.Contract.Common;
using PIMTool.Shared.Contract.RequestObject;
using System.Net;
using System.Threading.Tasks;

namespace PIMTool.Controllers
{
    [Route(RouteConstants.Project.Api)]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet(RouteConstants.Project.GetAll)]
        public async Task<IActionResult> GetProjects([FromQuery] ProjectFilterReq input)
        {
            PaginationModel<ProjectDto> projects = await _projectService.GetProjectsWithFilterAsync(input);
            return Ok(projects);
        }

        [HttpGet(RouteConstants.Project.GetOne)]
        public async Task<IActionResult> GetProject(long projectId)
        {
            ProjectDto result = await _projectService.GetProjectByIdAsync(projectId);
            return Ok(result);
        }

        [HttpGet(RouteConstants.Project.GetOneForUpdate)]
        public async Task<IActionResult> GetProjectForUpdate(long projectId)
        {
            UpdateProjectDto result = await _projectService.GetProjectForUpdateAsync(projectId);
            return Ok(result);
        }

        [HttpPut(RouteConstants.Project.UpdateOne)]
        public async Task<IActionResult> UpdateProject(long projectId, [FromBody] UpdateProjectDto project)
        {
            ProjectDto result = await _projectService.UpdateProjectAsync(projectId, project);
            return Ok(result);
        }

        [HttpPost(RouteConstants.Project.CreateOne)]
        public async Task<IActionResult> CreateProject([FromBody] NewProjectDto input)
        {
            ProjectDto result = await _projectService.CreateProjectAsync(input);
            return CreatedAtAction(nameof(GetProject), new { id = result.Id }, result);
        }

        [HttpDelete(RouteConstants.Project.DeleteOne)]
        public async Task<IActionResult> DeleteProject(long projectId, [FromQuery] int rowVersion)
        {
            await _projectService.DeleteProjectAsync(projectId, rowVersion);
            return NoContent();
        }

        [HttpPut(RouteConstants.Project.DeleteAll)]
        public async Task<IActionResult> DeleteProjects([FromBody] DeleteProjectsReq input)
        {
            await _projectService.DeleteProjects(input.ListIdAndRowVersion);
            return NoContent();
        }

        [HttpPost(RouteConstants.Project.CheckProjectNumber)]
        public IActionResult CheckProjectNumberExistence([FromBody] CheckProjectNumberExistenceReq input)
        {
            CheckProjectNumberExistenceRes result = _projectService.CheckProjectNumberExistence(input.Number);
            return Ok(result);
        }
    }
}
