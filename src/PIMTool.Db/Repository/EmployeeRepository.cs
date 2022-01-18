using Microsoft.EntityFrameworkCore;
using PIMTool.Services.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Db.Repository
{
    public class EmployeeRepository : RepositoryBase<EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IList<EmployeeEntity>> GetByListVisa(IList<string> listVisa)
        {
            IList<EmployeeEntity> items = await dbSet
                .Where(x => listVisa.Contains(x.Visa))
                .ToListAsync();

            return items;
        }
    }
}
