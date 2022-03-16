using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Hubs.Response.Group
{
    public class GroupHub_DisconnectUser_Response
    {
        public string UserName { get; set; }
        public string  GroupName { get; set; }
        public string GroupId { get; set; }
    }
}
