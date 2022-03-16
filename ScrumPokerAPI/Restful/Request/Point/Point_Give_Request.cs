using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Restful.Request.Point
{
    public class Point_Give_Request
    {
        public string GroupId { get; set; }
        public string ConnectionId { get; set; }
        public string Point { get; set; }
    }
}
