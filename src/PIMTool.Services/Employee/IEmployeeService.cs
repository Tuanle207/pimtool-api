using PIMTool.Services.Common;
using PIMTool.Shared.Contract.ResponseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Employee
{
    public interface IEmployeeService : IServiceBase<EmployeeEntity>
    {
        Task<CheckEmpExistenceRes> CheckExistenceAsync(List<string> listVisa);
    }
}
