using PIMTool.Services.Common;
using PIMTool.Services.Common.Entity;
using PIMTool.Services.Employee;
using PIMTool.Services.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Project
{
    public class ProjectEntity : EntityBase
    {
        public ProjectEntity()
        {
            Employees = new List<EmployeeEntity>();
            ProjectEmployees = new List<ProjectEmployeeEntity>();
        }

        public short ProjectNumber { get; set; }
        public string Name { get; set; }
        public string Customer { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public long GroupId { get; set; }
        public GroupEntity Group { get; private set; }
        public IList<EmployeeEntity> Employees { get; }
        public IList<ProjectEmployeeEntity> ProjectEmployees { get; }

        public void SetGroup(GroupEntity group)
        {
            Group = group;
        }

        public void AddEmployee(EmployeeEntity employee, DateTime? JoinDate = null)
        {
            if (employee == null)
            {
                return;
            }

            var projEmp = new ProjectEmployeeEntity
            {
                Project = this,
                ProjectId = this.Id,
                Employee = employee,
                EmployeeId = employee.Id,
            };
            if (JoinDate.HasValue)
            {
                projEmp.JoinDate = JoinDate.Value;
            }

            ProjectEmployees.Add(projEmp);
        }

        public void RemoveEmployee(EmployeeEntity employee)
        {
            if (employee == null)
            {
                return;
            }
            ProjectEmployeeEntity emp = ProjectEmployees.FirstOrDefault(x => x.EmployeeId == employee.Id);
            if (emp != null)
            {
                ProjectEmployees.Remove(emp);
            }

        }

        public void AddEmployee(IList<EmployeeEntity> employees)
        {
            if (employees == null)
            {
                return;
            }
            foreach (var emp in employees)
            {
                if (emp != null)
                {
                    AddEmployee(emp);
                }
            }
        }

        public void RemoveEmployee(IList<EmployeeEntity> employees)
        {
            if (employees == null)
            {
                return;
            }
            foreach (var emp in employees)
            {
                if (emp != null)
                {
                    RemoveEmployee(emp);
                }
            }

        }

        public void ClearEmployees()
        {
            ProjectEmployees.Clear();
        }
    }
}
