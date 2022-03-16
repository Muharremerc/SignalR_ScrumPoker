using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumPokerAPI.Model;
using ScrumPokerAPI.Restful.Request.Chat;
using ScrumPokerAPI.Restful.Response.Chat;
using ScrumPokerAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Controllers
{
    public class ChatController : APIControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult<APIReturn<bool>>> Add([FromBody] Chat_Message_Add_Request request)
        {
            await _chatService.AddMessage(request);
            return Success("Add", true);
        }

        [HttpGet("{groupId}/{connectionId}")]
        public async Task<ActionResult<APIReturn<Chat_Load_Response>>> Get(string connectionId , string groupId)
        {
            var response = await _chatService.GetChat(new Chat_Get_Request { ConnectionId = connectionId  ,GroupId = groupId });
            return Success("Get", response);
        }
    }
}
