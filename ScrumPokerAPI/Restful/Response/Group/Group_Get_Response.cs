using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScrumPokerAPI.Data;

namespace ScrumPokerAPI.Restful.Response.Group
{
    public class Group_Get_Response
    {
        public string Name { get; set; }
        public string GroupId { get; set; }
        public int MemberCount { get; set; }
    }
}
