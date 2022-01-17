using Microsoft.AspNetCore.Mvc;
using PIMTool.Services.Employee;
using PIMTool.Shared.Contract;
using PIMTool.Shared.Contract.RequestObject;
using PIMTool.Shared.Contract.ResponseObject;
using System.Threading.Tasks;

namespace PIMTool.Controllers
{
    [Route(RouteConstants.Employee.Api)]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost(RouteConstants.Employee.CheckVisasExistence)]
        public async Task<IActionResult> CheckEmployeesExistence(CheckEmpExistenceReq input)
        {
            CheckEmpExistenceRes result = await _employeeService.CheckExistenceAsync(input.ListVisa);
            return Ok(result);
        }
    }
}
