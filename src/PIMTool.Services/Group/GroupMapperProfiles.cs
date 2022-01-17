using AutoMapper;
using PIMTool.Shared.Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Group
{
    public class GroupMapperProfiles : Profile
    {
        public GroupMapperProfiles()
        {
            CreateMap<GroupEntity, BasicGroupDto>();
        }
    }
}
