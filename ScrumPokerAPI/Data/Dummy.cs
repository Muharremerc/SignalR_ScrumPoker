using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Data
{
    public static class Dummy
    {
        public static List<GroupDTO> Groups { get; } = new List<GroupDTO>();
        public static List<string> ConnectionIdList { get; set; } = new List<string>();


        public static async Task<ReadOnlyCollection<string>> GetConnectionIdList()
        {
            return await Task.Run(() =>
            {
                return ConnectionIdList.AsReadOnly();
            });
        }
    }

}
