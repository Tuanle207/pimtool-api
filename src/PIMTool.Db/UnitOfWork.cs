using Microsoft.Extensions.DependencyInjection;
using PIMTool.Services.Common;
using PIMTool.Services.Employee;
using PIMTool.Services.Group;
using PIMTool.Services.Project;
using System;
using System.Threading.Tasks;

namespace PIMTool.Db
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly AppDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        private IProjectRepository _projects;
        private IGroupRepository _groups;
        private IEmployeeRepository _employees;

        public UnitOfWork(AppDbContext dbContext, IServiceProvider serviceProvider)
        {
            _context = dbContext;
            _serviceProvider = serviceProvider;
        }

        public IProjectRepository Projects
        {
            get 
            {
                if (_projects == null)
                {
                    _projects = GetService<IProjectRepository>();
                }
                return _projects;
            }
        }

        public IGroupRepository Groups
        {
            get
            {
                if (_groups == null)
                {
                    _groups = GetService<IGroupRepository>();
                }
                return _groups;
            }
        }

        public IEmployeeRepository Employees
        {
            get
            {
                if (_employees == null)
                {
                    _employees = GetService<IEmployeeRepository>();
                }
                return _employees;
            }
        }

        public Task CompleteAsync()
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private TService GetService<TService>()
        {
            return _serviceProvider.GetRequiredService<TService>();
        }
    }
}
