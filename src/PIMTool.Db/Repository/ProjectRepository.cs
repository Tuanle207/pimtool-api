using Microsoft.EntityFrameworkCore;
using PIMTool.Services.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Db.Repository
{
    public class ProjectRepository : RepositoryBase<ProjectEntity>, IProjectRepository
    {
        public ProjectRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ProjectEntity> GetByProjectNumber(short projectNumber)
        {
            ProjectEntity project = await dbSet.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProjectNumber == projectNumber);

            return project;
        }

        public async Task<ProjectEntity> GetProjectByIdForUpdate(long projectId)
        {
            ProjectEntity project = await dbSet.Include(x => x.ProjectEmployees)
                 .FirstOrDefaultAsync(x => x.Id == projectId);

            return project;
        }
    }
}
