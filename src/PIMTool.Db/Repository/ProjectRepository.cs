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
    }
}
