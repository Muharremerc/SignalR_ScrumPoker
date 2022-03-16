using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Restful.Request.Group
{
    public class Group_Leave_Request
    {
        public string ConnectionId { get; set; }
        public string GroupId { get; set; }
    }
}
