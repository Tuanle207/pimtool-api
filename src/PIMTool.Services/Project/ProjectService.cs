using PIMTool.Shared.Contract.Common;
using PIMTool.Shared.Contract.RequestObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Project
{
    public class ProjectService : IProjectService
    {
        public CheckProjectNumberExistenceRes CheckProjectNumberExistence(short projectNumber)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDto> CreateProjectAsync(NewProjectDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProjectAsync(long projectId, int rowVersion)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProjects(Dictionary<long, int> listIdAndRowVersion)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDto> GetProjectByIdAsync(long projectId)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateProjectDto> GetProjectForUpdateAsync(long projectId)
        {
            throw new NotImplementedException();
        }

        public Task<PaginationModel<ProjectDto>> GetProjectsWithFilterAsync(ProjectFilterReq input)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectDto> UpdateProjectAsync(long projectId, UpdateProjectDto project)
        {
            throw new NotImplementedException();
        }
    }
}
