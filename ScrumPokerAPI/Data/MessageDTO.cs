using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Data
{
    public class MessageDTO
    {
        public string ConnectionId { get; set; }
        public string Sender { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Message { get; set; }
    }
}
