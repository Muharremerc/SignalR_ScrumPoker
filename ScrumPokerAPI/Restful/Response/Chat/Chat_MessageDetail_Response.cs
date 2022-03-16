using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Restful.Response.Chat
{
    public class Chat_MessageDetail_Response
    {
        public DateTime Date { get; set; }
        public string Sender { get; set; }
        public string Message { get; set; }
    }
}
