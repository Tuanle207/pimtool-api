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
    public interface IProjectService : IServiceBase<ProjectEntity>
    {
        Task<ProjectDto> GetProjectByIdAsync(long projectId);

        Task<UpdateProjectDto> GetProjectForUpdateAsync(long projectId);

        Task<PaginationModel<ProjectDto>> GetProjectsWithFilterAsync(ProjectFilterReq filter);

        Task<ProjectDto> UpdateProjectAsync(long projectId, UpdateProjectDto project);

        Task<ProjectDto> CreateProjectAsync(NewProjectDto newProject);

        Task DeleteProjectAsync(long projectId, string rowVersion);

        Task DeleteProjectsAsync(Dictionary<long, string> listIdAndRowVersion);

        Task<CheckProjectNumberExistenceRes> CheckProjectNumberExistenceAsync(short projectNumber);
    }
}
