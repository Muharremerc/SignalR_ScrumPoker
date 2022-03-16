using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Restful.Request.Group
{
    public class Group_Create_Request
    {
        public string ConnectionId { get; set; }
        public string GroupName { get; set; }
        public string UserName { get; set; }
    }
}
