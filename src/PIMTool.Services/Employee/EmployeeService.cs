using PIMTool.Services.Common;
using PIMTool.Shared.Contract.ResponseObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uow;

        public EmployeeService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<CheckEmpExistenceRes> CheckExistenceAsync(List<string> listVisa)
        {
            if (listVisa is null)
            {
                //throw new CoreException.BadRequestException("Please provide list of visa for checking");
            }
            var result = new CheckEmpExistenceRes
            {
                ValidationError = string.Empty,
                VisaExists = listVisa.ToDictionary(x => x, x => false)
            };

            IList<EmployeeEntity> emps = await _uow.Employees.GetByListVisa(listVisa);
            IList<string> notExistsVisas = new List<string>();

            foreach (string visa in listVisa)
            {
                if (!emps.Any(x => x.Visa == visa))
                {
                    notExistsVisas.Add(visa);
                }
                else
                {
                    result.VisaExists[visa] = true;
                }
            }

            if (notExistsVisas.Count > 0)
            {
                string visaSeq = string.Join(", ", notExistsVisas);
                result.ValidationError = $"The following visas do not exist: {visaSeq}";
            }

            return result;
        }
    }
}
