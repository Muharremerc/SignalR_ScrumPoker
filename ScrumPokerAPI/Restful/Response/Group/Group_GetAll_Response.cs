using ScrumPokerAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Restful.Response.Group
{
    public class Group_GetAll_Response
    {
        public List<Group_Get_Response> Groups { get; set; } = new List<Group_Get_Response>();
    }
}
