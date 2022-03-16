using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Data
{
    public class GroupDTO
    {
        public string Name { get; set; }
        public string GroupId { get; set; }
        public bool HidePoints { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public List<UserDTO> Users { get; } = new List<UserDTO>();
        public List<MessageDTO> Messages { get; set; } = new List<MessageDTO>();
    }
}
