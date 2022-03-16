using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Restful.Response.Group
{
    public class Group_Create_Response
    {
        public string Name { get; set; }
        public string GroupId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
