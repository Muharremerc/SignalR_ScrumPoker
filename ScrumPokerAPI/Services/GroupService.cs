using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using ScrumPokerAPI.Data;
using ScrumPokerAPI.Hubs;
using ScrumPokerAPI.Hubs.Response.Group;
using ScrumPokerAPI.Restful.Request.Group;
using ScrumPokerAPI.Restful.Response.Group;
using ScrumPokerAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Services
{

    public class GroupService : IGroupService
    {
        private readonly IHubContext<GroupHub> _hubContext;
        private readonly IBaseGroupService _baseGroupService;
        private readonly IMapper _mapper;
        public GroupService(IHubContext<GroupHub> hubContext, IBaseGroupService baseGroupService, IMapper mapper)
        {
            _hubContext = hubContext;
            _baseGroupService = baseGroupService;
            _mapper = mapper;
        }

        public async Task<Group_Create_Response> Create(Group_Create_Request request)
        {
            if (!Dummy.ConnectionIdList.Where(x => x == request.ConnectionId).Any())
                throw new Exception("User not found.");

            var addedGroup = await CreateGroup(request.GroupName);
            await JoinGroup(addedGroup.GroupId, request.ConnectionId, request.UserName);
            await _baseGroupService.RefreshGroupList();
            var otherGroup = Dummy.Groups.Where(x => x.Users.Where(x => x.ConnectionId == request.ConnectionId).Any() && x.GroupId != addedGroup.GroupId).ToList();
            foreach (var item in otherGroup)
            {
                await Leave(new Group_Leave_Request
                {
                    ConnectionId = request.ConnectionId,
                    GroupId = item.GroupId
                });
            }
            await _baseGroupService.RefreshGroup(addedGroup.GroupId);
            return _mapper.Map<Group_Create_Response>(addedGroup);
        }
        public async Task Join(Group_Join_Request request)
        {
            if (!Dummy.ConnectionIdList.Where(x => x == request.ConnectionId).Any())
                throw new Exception("User not found.");

            var group = await _baseGroupService.GetGroupbyId(request.GroupId);
            var user = await JoinGroup(request.GroupId, request.ConnectionId, request.UserName);
            var otherGroup = Dummy.Groups.Where(x => x.Users.Where(x => x.ConnectionId == request.ConnectionId).Any() && x.GroupId != request.GroupId).ToList();
            foreach (var item in otherGroup)
            {
                await Leave(new Group_Leave_Request
                {
                    ConnectionId = request.ConnectionId,
                    GroupId = item.GroupId
                });
            } 
            await _baseGroupService.RefreshGroup(request.GroupId);
            await _baseGroupService.RefreshGroupList();
        }
        public async Task<GroupDTO> CreateGroup(string groupName)
        {
            return await Task.Run(() =>
            {
                if (Dummy.Groups.Any(x => x.Name == groupName))
                    throw new Exception("Already created.");

                var tempGroup = new GroupDTO
                {
                    GroupId = Guid.NewGuid().ToString(),
                    Name = groupName
                };
                Dummy.Groups.Add(tempGroup);
                return tempGroup;
            });

        }
        public async Task<UserDTO> JoinGroup(string groupId, string connectionId, string userName)
        {
            return await Task.Run(async () =>
            {
                var tempGroup = await _baseGroupService.GetGroupbyId(groupId);
                if (tempGroup == null)
                    throw new Exception("Group not found.");

                var isExist = tempGroup.Users.Where(x => x.ConnectionId == connectionId).FirstOrDefault();
                if (isExist != null)
                {
                    if (isExist.DisconnectedDate == null)
                        throw new Exception("Allready added.");
                    else
                        isExist.DisconnectedDate = null;
                    return isExist;
                }
                else
                {
                    var newUser = new UserDTO
                    {
                        ConnectionId = connectionId,
                        Name = userName,
                        Point = "0"
                    };
                    tempGroup.Users.Add(newUser);
                    return newUser;
                }
            });

        }
        public async Task SendDisconnetMessageOtherUser(string groupId, string groupName, string userName)
        {
            await this._hubContext.Clients.Clients(await _baseGroupService.GetUserListbyGroupId(groupId))
                .SendAsync("disconnededUser",
                new GroupHub_DisconnectUser_Response
                {
                    GroupName = groupName,
                    UserName = userName,
                    GroupId = groupId
                });
        }
        public async Task Disconnect(string connectionId)
        {
            if (!Dummy.ConnectionIdList.Where(x => x == connectionId).Any())
                throw new Exception("User not found.");

            var groupList = Dummy.Groups.Where(x => x.Users.Where(x => x.ConnectionId == connectionId).Any()).ToList();
            foreach (var group in groupList)
            {
                var user = group.Users.Where(x => x.ConnectionId == connectionId).FirstOrDefault();
                group.Users.Remove(user);
                await _baseGroupService.RefreshGroup(group.GroupId);
                if (!group.Users.Where(x => x.DisconnectedDate == null).Any())
                    Dummy.Groups.Remove(group);
            }
            await _baseGroupService.RefreshGroupList();

        }
        public async Task<bool> UpdateHideState(Group_Update_HideState_Request request)
        {
            var tempGroup = await _baseGroupService.GetGroupbyId(request.GroupId);
            if (tempGroup == null)
                throw new Exception("Group not found.");

            if (tempGroup.Users.Where(x => x.ConnectionId == request.ConnectionId).FirstOrDefault() == null)
                throw new Exception("User not found");

            tempGroup.HidePoints = !tempGroup.HidePoints;
            await _baseGroupService.RefreshGroup(request.GroupId);
            return true;
        }
        public async Task Clear(Group_Clear_Request request)
        {
            var group = await _baseGroupService.GetGroupbyId(request.GroupId);
            if (group == null)
                throw new Exception("Group not found.");

            if (group.Users.Where(x => x.ConnectionId == request.ConnectionId).FirstOrDefault() == null)
                throw new Exception("User not found");


            group.Users.ForEach(x => x.Point = "0");
            await _baseGroupService.RefreshGroup(request.GroupId);
        }

        public async Task Leave(Group_Leave_Request request)
        {
            var tempGroup = await _baseGroupService.GetGroupbyId(request.GroupId);
            if (tempGroup == null)
                throw new Exception("Group not found.");
            var tempUser = tempGroup.Users.Where(x => x.ConnectionId == request.ConnectionId).FirstOrDefault();
            if (tempUser == null)
                throw new Exception("User not found.");
            tempUser.DisconnectedDate = DateTime.Now;
            await _baseGroupService.RefreshGroup(request.GroupId);
        }
    }
}
