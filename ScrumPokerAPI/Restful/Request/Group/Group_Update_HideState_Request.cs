using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Restful.Request.Group
{
    public class Group_Update_HideState_Request
    {
        public string GroupId { get; set; }
        public string ConnectionId { get; set; }
    }
}
