using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Restful.Response.Chat
{
    public class Chat_Load_Response
    {
        public List<Chat_MessageDetail_Response> MessageDetailList { get; set; } = new List<Chat_MessageDetail_Response>();
    }
}
