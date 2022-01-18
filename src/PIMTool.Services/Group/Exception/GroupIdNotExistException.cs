using PIMTool.Services.Common.Exception;
using PIMTool.Shared.Contract.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Services.Group.Exception
{
    public class GroupIdNotExistException : DataValidationException
    {
        public GroupIdNotExistException(long groupId) 
            : base(nameof(NewProjectDto.GroupId), "Group id does not exist")
        {
            GroupdId = groupId;
        }

        public long GroupdId { get; private set; }
    }
}
