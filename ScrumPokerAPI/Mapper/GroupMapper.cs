using AutoMapper;
using ScrumPokerAPI.Data;
using ScrumPokerAPI.Restful.Response.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Mapper
{
    public class GroupMapper : Profile
    {
        public GroupMapper()
        {
            CreateMap<GroupDTO, Group_Create_Response>();
            CreateMap<Group_Create_Response, GroupDTO>();
        }
    }
}
