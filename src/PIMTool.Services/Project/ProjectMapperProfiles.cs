using AutoMapper;
using PIMTool.Shared.Contract.Common;
using PIMTool.Shared.Extension.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Project
{
    public class ProjectMapperProfiles : Profile
    {
        public ProjectMapperProfiles()
        {
            CreateMap<ProjectEntity, ProjectDto>()
                .ConvertUsing(x => MapProjectEntityToProjectDto(x));

            CreateMap<ProjectEntity, UpdateProjectDto>()
                .ConvertUsing(x => MapProjectEntityToUpdateProjectDto(x));

            CreateMap<NewProjectDto, ProjectEntity>()
                .ConvertUsing(x => MapNewProjectDtoEntityToProject(x));
        }

        private static ProjectDto MapProjectEntityToProjectDto(ProjectEntity src)
        {
            if (src is null)
            {
                return null;
            }
            var dest = new ProjectDto
            {
                Id = src.Id,
                Name = src.Name,
                Number = src.ProjectNumber,
                Customer = src.Customer,
                StartDate = src.StartDate,
                Status = src.Status,
                RowVersion = StringHelper.GetString(src.RowVersion),
            };
            return dest;
        }

        public static UpdateProjectDto MapProjectEntityToUpdateProjectDto(ProjectEntity src)
        {
            if (src is null)
            {
                return null;
            }
            var dest = new UpdateProjectDto
            {
                Id = src.Id,
                Name = src.Name,
                ProjectNumber = src.ProjectNumber,
                Customer = src.Customer,
                StartDate = src.StartDate,
                Employees = string.Join(",", src.Employees.Select(x => x.Visa)),
                Status = src.Status,
                EndDate = src.EndDate,
                GroupId = src.GroupId,
                RowVersion = StringHelper.GetString(src.RowVersion),
            };
            return dest;
        }

        public static ProjectEntity MapNewProjectDtoEntityToProject(NewProjectDto src)
        {
            if (src is null)
            {
                return null;
            }
            var dest = new ProjectEntity
            {
                Name = src.Name,
                ProjectNumber = src.ProjectNumber,
                Customer = src.Customer,
                Status = src.Status,
                StartDate = src.StartDate,
                EndDate = src.EndDate
            };
            return dest;
        }
    }
}
