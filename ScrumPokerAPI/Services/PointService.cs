using Microsoft.AspNetCore.SignalR;
using ScrumPokerAPI.Data;
using ScrumPokerAPI.Hubs;
using ScrumPokerAPI.Restful.Request.Point;
using ScrumPokerAPI.Restful.Response.Group;
using ScrumPokerAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Services
{
    public class PointService : IPointService
    {
        private readonly IBaseGroupService _baseGroupService;
        public PointService(IBaseGroupService baseGroupService)
        {
            _baseGroupService = baseGroupService;
        }

        public async Task GivePoint(Point_Give_Request request)
        {
            var tempGroup = await _baseGroupService.GetGroupbyId(request.GroupId);
            if (tempGroup == null)
                throw new Exception("Group not found.");

            var tempUser = tempGroup.Users.Where(x => x.ConnectionId == request.ConnectionId && x.DisconnectedDate == null).FirstOrDefault();
            if (tempUser == null)
                throw new Exception("User not found.");

            tempUser.Point = request.Point;
           await _baseGroupService.RefreshGroup(request.GroupId);
        }

    }
}
