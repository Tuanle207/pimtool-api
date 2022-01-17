using PIMTool.Services.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Db.Repository
{
    public class GroupRepository : RepositoryBase<GroupEntity>, IGroupRepository
    {
        public GroupRepository(AppDbContext context) : base(context)
        {
        }
    }
}
