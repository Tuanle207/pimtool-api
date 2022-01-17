using PIMTool.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Employee
{
    public interface IEmployeeRepository : IRepositoryBase<EmployeeEntity>
    {
        Task<IList<EmployeeEntity>> GetByListVisa(IList<string> listVisa);
    }
}
