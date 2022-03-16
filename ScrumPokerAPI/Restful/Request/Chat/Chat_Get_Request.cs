using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Restful.Request.Chat
{
    public class Chat_Get_Request
    {
        public string GroupId { get; set; }
        public string ConnectionId { get; set; }
    }
}
