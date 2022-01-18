using AutoMapper;
using PIMTool.Services.Common;
using PIMTool.Services.Common.Exception;
using PIMTool.Services.Employee;
using PIMTool.Services.Employee.Exception;
using PIMTool.Services.Group;
using PIMTool.Services.Group.Exception;
using PIMTool.Services.Project.Exception;
using PIMTool.Shared.BusinessConstant;
using PIMTool.Shared.Contract.Common;
using PIMTool.Shared.Contract.RequestObject;
using PIMTool.Shared.Extension;
using PIMTool.Shared.Extension.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIMTool.Services.Project
{
    public class ProjectService : ServiceBase<ProjectEntity>, IProjectService
    {
        private readonly IUnitOfWork _uow;
        private readonly IEntitySelectorFactory _selectorFactory;
        private readonly IMapper _mapper;
        private readonly IValidator<NewProjectDto> _newProjectValidator;

        public ProjectService(IUnitOfWork uow, IEntitySelectorFactory selectorFactory, IMapper mapper, 
            IValidator<NewProjectDto> newProjectValidator)
        {
            _uow = uow;
            _selectorFactory = selectorFactory;
            _mapper = mapper;
            _newProjectValidator = newProjectValidator;
        }

        public async Task<CheckProjectNumberExistenceRes> CheckProjectNumberExistenceAsync(short projectNumber)
        {
            ProjectEntity existingProject = await _uow.Projects.GetByProjectNumber(projectNumber);
            if (existingProject == null)
            {
                return new CheckProjectNumberExistenceRes
                {
                    Exists = false,
                    ValidationError = string.Empty
                };
            }

            return new CheckProjectNumberExistenceRes
            {
                Exists = true,
                ValidationError = $"The project number {projectNumber} already existed. Please select a different number"
            };
        }

        public async Task<ProjectDto> CreateProjectAsync(NewProjectDto newProject)
        {
            // Input validation 
            _newProjectValidator.ValidateAndThrow(newProject);

            // Check if project number already exist?
            ProjectEntity existingProject = await _uow.Projects.GetByProjectNumber(newProject.ProjectNumber);
            if (existingProject != null)
            {
                throw new ProjectNumberAlreadyExistsException(existingProject.ProjectNumber, existingProject.Id);
            }

            // Mapping and create new project
            ProjectEntity project = _mapper.Map<NewProjectDto, ProjectEntity>(newProject);

            // Get Group by Group Id
            GroupEntity group = await _uow.Groups.GetByIdAsync(newProject.GroupId, true);
            project.SetGroup(group);
            if (group is null)
            {
                throw new GroupIdNotExistException(project.GroupId);
            }

            // Get project's employees by visas
            if (!string.IsNullOrWhiteSpace(newProject.Employees))
            {
                IList<string> visas = ParseVisasList(newProject.Employees);
                IList<EmployeeEntity> employees = await _uow.Employees.GetByListVisa(visas);

                var employeeVisasNotExistsExp = new VisasNotExistException();
                foreach (var visa in visas)
                {
                    if (!employees.Any(x => x.Visa == visa))
                    {
                        employeeVisasNotExistsExp.AddVisa(visa);
                    }
                }
                if (employeeVisasNotExistsExp.Visas.Count > 0)
                {
                    throw employeeVisasNotExistsExp;
                }

                project.AddEmployee(employees);

            }

            // Add project
            _uow.Projects.Add(project);
            await _uow.CompleteAsync();

            // Return create project
            ProjectDto result = _mapper.Map<ProjectEntity, ProjectDto>(project);

            return result;
        }

        public async Task DeleteProjectAsync(long projectId, string rowVersion)
        {
            ProjectEntity project = await _uow.Projects.GetByIdAsync(projectId, true);

            // Prevent deleting non-new project
            if (project.Status != ProjectStatus.New)
            {
                throw new CoreException.BadRequestException("Can not delete a project which has status other than NEW");
            }

            //// Check if project has been modified?
            CheckAndThrowIfObjectModified(project, rowVersion);

            _uow.Projects.Delete(project);

            await _uow.CompleteAsync();
        }

        public async Task DeleteProjectsAsync(Dictionary<long, string> listIdAndRowVersion)
        {
            if (listIdAndRowVersion is null)
            {
                return;
            }

            foreach (var idAndRow in listIdAndRowVersion)
            {
                if (idAndRow.Key <= 0 || string.IsNullOrEmpty(idAndRow.Value))
                {
                    throw new CoreException.BadRequestException("Please provide invalid Id and Row version list");
                }
            }

            IList<long> listId = listIdAndRowVersion.Keys.ToList();
            IList<ProjectEntity> projects = await _uow.Projects.GetByIdAsync(listId);

            // Prevent deleting deleting if there is any non-new project or modified project
            foreach (var project in projects)
            {
                if (project.Status != ProjectStatus.New)
                {
                    throw new CoreException.BadRequestException("Can not delete a project which has status other than NEW");
                }

                string oldRowVersion = listIdAndRowVersion[project.Id];
                if (!CheckIfObjectNotModified(oldRowVersion, project.RowVersion))
                {
                    throw new CoreException.DataHasBeenModifiedException(entityName: nameof(ProjectEntity),
                        id: project.Id, oldVersion: oldRowVersion, newVersion: StringHelper.GetString(project.RowVersion));
                }
            }

            _uow.Projects.Delete(projects);

            await _uow.CompleteAsync();
        }

        public async Task<ProjectDto> GetProjectByIdAsync(long projectId)
        {
            var selector = _selectorFactory.CreateSelector<ProjectEntity, ProjectDto>();
            ProjectDto result = await _uow.Projects.GetByIdAsync(projectId, selector);
            return result;
        }

        public async Task<UpdateProjectDto> GetProjectForUpdateAsync(long projectId)
        {
            var selector = _selectorFactory.CreateSelector<ProjectEntity, UpdateProjectDto>();
            UpdateProjectDto result = await _uow.Projects.GetByIdAsync(projectId, selector);
            return result;
        }

        public async Task<PaginationModel<ProjectDto>> GetProjectsWithFilterAsync(ProjectFilterReq filter)
        {
            var selector = _selectorFactory.CreateSelector<ProjectEntity, ProjectDto>();
            var pagedModel = new PaginationModel<ProjectDto>(new List<ProjectDto>(), 0, 1, 0);

            // Filter by project number as the highest priority
            if (filter.Number.HasValue)
            {
                IQueryable<ProjectEntity> singleQuery = _uow.Projects.AsQueryable();

                // Filter by status
                if (!string.IsNullOrWhiteSpace(filter.Status))
                {
                    singleQuery = singleQuery.Where(x => x.Status == filter.Status);
                }

                singleQuery = singleQuery.Where(x => x.ProjectNumber == filter.Number.Value);

                ProjectDto project = await _uow.Projects.FirstOrDefaultAsync(singleQuery, selector);

                // Return if there is any project found
                if (project != null)
                {
                    pagedModel.Items = new List<ProjectDto> { project };
                    pagedModel.TotalCount = 1;
                    pagedModel.PageSize = 1;
                    return pagedModel;
                }
            }

            // If there is no single project found, continue with other keyword
            IQueryable<ProjectEntity> query = _uow.Projects.AsQueryable();

            // Filter by status
            if (!string.IsNullOrWhiteSpace(filter.Status))
            {
                query = query.Where(x => x.Status == filter.Status);
            }

            // Fitler by project name or customer name
            string projectName = string.IsNullOrWhiteSpace(filter.Name) ? string.Empty : filter.Name;
            string customerName = string.IsNullOrWhiteSpace(filter.Customer) ? string.Empty : filter.Customer;
            query = query.Where(x => x.Name.Contains(projectName) || x.Customer.Contains(customerName));

            // Count number of records
            pagedModel.TotalCount = await _uow.Projects.CountAllAsync(query);

            // Pagination
            int pageIndex = filter.PageIndex ?? 1;
            int pageSize = filter.PageSize ?? 15;
            query = query.Skip(pageIndex)
                .Take(pageSize);
            IList<ProjectDto> projects = await _uow.Projects.GetAllAsync(query, selector);

            // Return final resul
            pagedModel.Items = projects;
            pagedModel.PageIndex = pageIndex;
            pagedModel.PageSize = pageSize;
            return pagedModel;
        }

        public async Task<ProjectDto> UpdateProjectAsync(long projectId, UpdateProjectDto project)
        {
            // Input validation 
            _newProjectValidator.ValidateAndThrow(project);

            // Get project via id
            ProjectEntity existingProject = await _uow.Projects.GetProjectByIdForUpdate(projectId);
            if (existingProject == null)
            {
                throw new CoreException.NotFoundException("ProjectId", projectId, "Project not found!");
            }

            // Check if project has been modified?
            CheckAndThrowIfObjectModified(existingProject, project.RowVersion);

            // Update basic info
            existingProject.Name = project.Name;
            existingProject.Customer = project.Customer;
            existingProject.Status = project.Status;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;

            // Update Group
            GroupEntity group = await _uow.Groups.GetByIdAsync(project.GroupId, true);
            if (group is null)
            {
                throw new GroupIdNotExistException(project.GroupId);
            }
            else
            {
                existingProject.SetGroup(group);
            }

            // Delete old project emp list
            existingProject.ClearEmployees();

            // Update project emp list if specified
            if (!string.IsNullOrWhiteSpace(project.Employees))
            {
                IList<string> visas = ParseVisasList(project.Employees);
                IList<EmployeeEntity> employees = await _uow.Employees.GetByListVisa(visas);

                var employeeVisasNotExistsExp = new VisasNotExistException();
                foreach (var visa in visas)
                {
                    if (employees.Where(x => x.Visa == visa).Count() == 0)
                    {
                        employeeVisasNotExistsExp.AddVisa(visa);
                    }
                }
                if (employeeVisasNotExistsExp.Visas.Count > 0)
                {
                    throw employeeVisasNotExistsExp;
                }

                if (employees.Count > 0)
                {
                    existingProject.Employees.Clear();
                    existingProject.AddEmployee(employees);
                }
            }

            _uow.Projects.Update(existingProject);

            // Return create project
            ProjectDto result = _mapper.Map<ProjectEntity, ProjectDto>(existingProject);

            // Save changes
            await _uow.CompleteAsync();

            return result;
        }

        #region Helpers

        private IList<string> ParseVisasList(string visaList)
        {
            string trimmedVisaList = StringHelper.RemoveWhiteSpaces(visaList);
            return trimmedVisaList.Split(',');
        }

        #endregion
    }
}
