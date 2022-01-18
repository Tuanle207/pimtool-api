using PIMTool.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Project
{
    public interface IProjectRepository : IRepositoryBase<ProjectEntity>
    {
        Task<ProjectEntity> GetByProjectNumber(short projectNumber);
        Task<ProjectEntity> GetProjectByIdForUpdate(long projectId);
    }
}
