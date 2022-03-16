using ScrumPokerAPI.Restful.Request.Chat;
using ScrumPokerAPI.Restful.Response.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Services.Interfaces
{
    public interface IChatService
    {
        Task AddMessage(Chat_Message_Add_Request request);
        Task<Chat_Load_Response> GetChat(Chat_Get_Request request);

    }
}
