using PIMTool.Services.Employee;
using PIMTool.Services.Group;
using PIMTool.Services.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IProjectRepository Projects { get; }

        IGroupRepository Groups { get; }

        IEmployeeRepository Employees { get; }

        Task CompleteAsync();
    }
}
