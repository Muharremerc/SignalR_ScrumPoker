using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Data
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string ConnectionId { get; set; }
        public string Point { get; set; }
        public DateTime JoinedDate { get; set; } = DateTime.Now;
        public DateTime? DisconnectedDate { get; set; }
    }
}
