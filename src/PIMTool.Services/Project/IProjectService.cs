using PIMTool.Services.Common;
using PIMTool.Shared.Contract.Common;
using PIMTool.Shared.Contract.RequestObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Project
{
    public interface IProjectService : IServiceBase
    {
        Task<ProjectDto> GetProjectByIdAsync(long projectId);

        Task<UpdateProjectDto> GetProjectForUpdateAsync(long projectId);

        Task<PaginationModel<ProjectDto>> GetProjectsWithFilterAsync(ProjectFilterReq input);

        Task<ProjectDto> UpdateProjectAsync(long projectId, UpdateProjectDto project);

        Task<ProjectDto> CreateProjectAsync(NewProjectDto input);

        Task DeleteProjectAsync(long projectId, int rowVersion);

        Task DeleteProjects(Dictionary<long, int> listIdAndRowVersion);

        CheckProjectNumberExistenceRes CheckProjectNumberExistence(short projectNumber);
    }
}
