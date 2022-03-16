using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Hubs.Response.Group
{
    public class GroupHub_GroupUserList_Response
    {
        public List<GroupHub_User_Response> UserList { get; set; } = new List<GroupHub_User_Response>();
    }
}
