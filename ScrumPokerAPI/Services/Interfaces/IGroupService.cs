using ScrumPokerAPI.Data;
using ScrumPokerAPI.Restful.Request.Group;
using ScrumPokerAPI.Restful.Response.Group;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Services.Interfaces
{
    public interface IGroupService
    {
        Task<Group_Create_Response> Create(Group_Create_Request request);
        Task<GroupDTO> CreateGroup(string groupName);
        Task Join(Group_Join_Request request);
        Task Leave(Group_Leave_Request request);
        Task Clear(Group_Clear_Request request);
        Task<UserDTO> JoinGroup(string groupName, string connectionId, string userName);
        Task Disconnect(string connectionId);
        Task SendDisconnetMessageOtherUser(string groupId, string groupName, string userName);
        Task<bool> UpdateHideState(Group_Update_HideState_Request request);

    }
}
