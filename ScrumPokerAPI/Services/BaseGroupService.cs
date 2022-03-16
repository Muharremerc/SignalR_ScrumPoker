using Microsoft.AspNetCore.SignalR;
using ScrumPokerAPI.Data;
using ScrumPokerAPI.Hubs;
using ScrumPokerAPI.Hubs.Response.Group;
using ScrumPokerAPI.Restful.Response.Chat;
using ScrumPokerAPI.Restful.Response.Group;
using ScrumPokerAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Services
{
    public class BaseGroupService : IBaseGroupService
    {
        private readonly IHubContext<GroupHub> _hubContext;
        public BaseGroupService(IHubContext<GroupHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task RefreshGroup(string groupId)
        {
            var tempGroup = await GetGroupbyId(groupId);
            var response = new GroupHub_GroupUserList_Response();
            foreach (var user in tempGroup.Users)
            {
                response.UserList.Add(new GroupHub_User_Response
                {
                    ConnectionId = user.ConnectionId,
                    Point = tempGroup.HidePoints ? "X" : user.Point.ToString(),
                    Name = user.Name,
                    IsDisconnected = user.DisconnectedDate != null
                });
            }
            await this._hubContext.Clients.Clients(await GetUserListbyGroupId(groupId))
            .SendAsync("refreshGroup", response);
        }


        public async Task AppedMessage(Chat_MessageDetail_Response messageDetail,string groupId)
        {
            await this._hubContext.Clients.Clients(await GetUserListbyGroupId(groupId))
            .SendAsync("appedMessage", messageDetail);
        }

        public async Task<GroupDTO> GetGroupbyId(string groupId)
        {
            return await Task.Run(() =>
            {
                return Dummy.Groups.Where(x => x.GroupId == groupId).FirstOrDefault();
            });

        }
        public async Task<ReadOnlyCollection<string>> GetUserListbyGroupId(string groupId)
        {
            return await Task.Run(() =>
            {
                return Dummy.Groups.Where(x => x.GroupId == groupId).FirstOrDefault().Users.Where(x => x.DisconnectedDate == null).Select(x => x.ConnectionId).ToList().AsReadOnly();
            });
        }
        public async Task<ReadOnlyCollection<string>> GetUserListbyGroupId(string groupId, string connectionId)
        {
            return await Task.Run(() =>
            {
                return Dummy.Groups.Where(x => x.GroupId == groupId).FirstOrDefault().Users.Select(x => x.ConnectionId).Where(x => x != connectionId).ToList().AsReadOnly();
            });
        }
        public async Task RefreshGroupList()
        {
            await this._hubContext.Clients.Clients(Dummy.ConnectionIdList).SendAsync("groupList", await GetAll());
        }
        public async Task<Group_GetAll_Response> GetAll()
        {
            return await Task.Run(() =>
            {
                Group_GetAll_Response response = new();
                Dummy.Groups.ForEach(x =>
                {
                    response.Groups.Add(new Group_Get_Response
                    {
                        MemberCount = x.Users.Where(x => x.DisconnectedDate == null).ToList().Count,
                        Name = x.Name,
                        GroupId = x.GroupId
                    });
                });
                return response;
            });
        }
    }
}
