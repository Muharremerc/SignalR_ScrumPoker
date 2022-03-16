using ScrumPokerAPI.Data;
using ScrumPokerAPI.Restful.Request.Chat;
using ScrumPokerAPI.Restful.Response.Chat;
using ScrumPokerAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Services
{
    public class ChatService : IChatService
    {
        private readonly IBaseGroupService _baseGroupService;
        public ChatService(IBaseGroupService baseGroupService)
        {
            _baseGroupService = baseGroupService;
        }

        public async Task AddMessage(Chat_Message_Add_Request request)
        {
            var tempGroup = await _baseGroupService.GetGroupbyId(request.GroupId);
            if (tempGroup == null)
                throw new Exception("Group not found.");

            var tempUser = tempGroup.Users.Where(x => x.ConnectionId == request.ConnectionId && x.DisconnectedDate == null).FirstOrDefault();
            if (tempUser == null)
                throw new Exception("User not found.");

            var message = new MessageDTO { Message = request.Message, ConnectionId = request.ConnectionId, Sender = tempUser.Name, CreatedDate = DateTime.Now };
            tempGroup.Messages.Add(message);
            await _baseGroupService.AppedMessage(new Chat_MessageDetail_Response
            {
                Date = message.CreatedDate,
                Message = message.Message,
                Sender = message.Sender
            }, request.GroupId);
        }


        public async Task<Chat_Load_Response> GetChat(Chat_Get_Request request)
        {
            var tempGroup = await _baseGroupService.GetGroupbyId(request.GroupId);
            var response = new Chat_Load_Response();
            if(tempGroup == null)
                throw new Exception("Group not found.");
            var userIsExist = tempGroup.Users.Where(x => x.ConnectionId == request.ConnectionId).Any();
            if (userIsExist)
            {
                foreach (var message in tempGroup.Messages)
                {
                    response.MessageDetailList.Add(new Chat_MessageDetail_Response
                    {
                        Sender = message.Sender,
                        Date = message.CreatedDate,
                        Message = message.Message
                    });
                }
                return response;
            }
            throw new Exception("User not found.");
        }
    }
}
