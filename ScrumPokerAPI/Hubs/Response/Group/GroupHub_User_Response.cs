using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Hubs.Response.Group
{
    public class GroupHub_User_Response
    {
        public string ConnectionId { get; set; }
        public string Point { get; set; }
        public string Name { get; set; }
        public bool IsDisconnected { get; set; }

    }
}
