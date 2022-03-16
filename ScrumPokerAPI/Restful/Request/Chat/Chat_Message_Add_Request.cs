using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Restful.Request.Chat
{
    public class Chat_Message_Add_Request
    {
        public string GroupId { get; set; }
        public string ConnectionId { get; set; }
        public string Message { get; set; }
    }
}
