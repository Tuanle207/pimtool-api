using PIMTool.Services.Common;
using PIMTool.Services.Common.Exception;
using PIMTool.Shared.BusinessConstant;
using PIMTool.Shared.Contract.Common;
using PIMTool.Shared.Extension.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Project.Validation
{
    public class NewProjectValidator : Validator<NewProjectDto>
    {
        public override bool Validate(NewProjectDto input)
        {
            base.Validate(input);

            AddValidatorError(nameof(NewProjectDto.ProjectNumber), ValidateProjectNumber(input.ProjectNumber));
            AddValidatorError(nameof(NewProjectDto.Name), ValidateName(input.Name));
            AddValidatorError(nameof(NewProjectDto.Customer), ValidateCustomerName(input.Customer));
            AddValidatorError(nameof(NewProjectDto.GroupId), ValidateGroupId(input.GroupId));
            AddValidatorError(nameof(NewProjectDto.Employees), ValidateEmployees(input.Employees));
            AddValidatorError(nameof(NewProjectDto.Status), ValidateStatus(input.Status));
            AddValidatorError(nameof(NewProjectDto.StartDate), ValidateStartDate(input.StartDate));
            AddValidatorError(nameof(NewProjectDto.EndDate), ValidateEndDate(input.StartDate, input.EndDate));

            return m_errors.Count > 0;
        }

        public override void ValidateAndThrow(NewProjectDto input)
        {
            Validate(input);
            if (ErrorCount > 0)
            {
                throw new DataValidationException(Errors);
            }
        }

        private string ValidateProjectNumber(short projectNumber)
        {
            if (projectNumber < 1000 || projectNumber > 9999)
            {
                return "Project number must have exactly 4 numeric characters, the fist one must not be 0";
            }
            return string.Empty;
        }

        private string ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Project name must not be empty";
            }
            return string.Empty;
        }

        private string ValidateCustomerName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Customer name must not be empty";
            }
            return string.Empty;
        }

        private string ValidateGroupId(long groupId)
        {
            if (groupId <= 0)
            {
                return "Group Id is invalid";
            }
            return string.Empty;
        }

        private string ValidateEmployees(string employees)
        {
            var members = new List<string>(); ;

            if (!string.IsNullOrWhiteSpace(employees))
            {
                members = StringHelper.RemoveWhiteSpaces(employees).Split(',').ToList();
                foreach (var member in members)
                {
                    if (member.Length != 3)
                    {
                        return "Each visa must have exactly 3 characters, must be separated by comma";
                    }
                }
            }
            if (members.Distinct().Count() != members.Count)
            {
                return "Each visa must be unique";
            }

            return string.Empty;
        }

        private string ValidateStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return "Status must not be empty";
            }
            else
            {
                var validStatus = new List<string>
                {
                    ProjectStatus.New,
                    ProjectStatus.Planned,
                    ProjectStatus.InProgress,
                    ProjectStatus.Finished,
                };

                if (!validStatus.Contains(status))
                {
                    return "Status is invalid";
                }
            }

            return string.Empty;
        }

        private string ValidateStartDate(DateTime startDate)
        {
            if (startDate == default(DateTime))
            {
                return "Start date is required";
            }
            return string.Empty;
        }

        private string ValidateEndDate(DateTime startDate, DateTime? endDate)
        {

            if (endDate.HasValue && endDate.Value.Subtract(startDate).TotalDays <= 0)
            {
                return "End date must be greater than start date";
            }
            return string.Empty;
        }
    }
}
