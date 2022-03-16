using ScrumPokerAPI.Data;
using ScrumPokerAPI.Restful.Response.Chat;
using ScrumPokerAPI.Restful.Response.Group;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Services.Interfaces
{
    public interface IBaseGroupService
    {
        Task RefreshGroup(string groupId);
        Task RefreshGroupList();
        Task<GroupDTO> GetGroupbyId(string groupId);
        Task<Group_GetAll_Response> GetAll();
        Task AppedMessage(Chat_MessageDetail_Response messageDetail, string groupId);
        Task<ReadOnlyCollection<string>> GetUserListbyGroupId(string groupId);
        Task<ReadOnlyCollection<string>> GetUserListbyGroupId(string groupId, string connectionId);
    }
}
