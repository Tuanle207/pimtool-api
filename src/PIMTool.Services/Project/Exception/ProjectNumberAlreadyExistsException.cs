using PIMTool.Services.Common.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Project.Exception
{
    public class ProjectNumberAlreadyExistsException : DataValidationException
    {
        public long ProjectNumber { get; private set; }
        public long ProjectId { get; private set; }

        public ProjectNumberAlreadyExistsException(short projectNumber, long projectId)
            : base(nameof(ProjectEntity.ProjectNumber),
                    $"The project number {projectNumber} already existed. Please select a different number")
        {
            ProjectNumber = projectNumber;
            ProjectId = projectId;
        }
    }
}
