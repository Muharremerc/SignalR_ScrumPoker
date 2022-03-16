using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumPokerAPI.Model;
using ScrumPokerAPI.Restful.Request.Group;
using ScrumPokerAPI.Restful.Response.Group;
using ScrumPokerAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Controllers
{

    public class GroupController : APIControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IBaseGroupService _baseGroupService;
        public GroupController(IGroupService groupService, IBaseGroupService baseGroupService)
        {
            _groupService = groupService;
            _baseGroupService = baseGroupService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<APIReturn<Group_Create_Response>>> Create([FromBody] Group_Create_Request request)
        {
            return Success("Created", await _groupService.Create(request));
        }

        [HttpPost]
        [Route("Join")]
        public async Task<ActionResult<APIReturn<bool>>> Join([FromBody] Group_Join_Request request)
        {
            await _groupService.Join(request);
            return Success("Joinned", true);
        }

        [HttpPost]
        [Route("Leave")]
        public async Task<ActionResult<APIReturn<bool>>> Leave([FromBody] Group_Leave_Request request)
        {
            await _groupService.Leave(request);
            return Success("Leaved", true);
        }

        [HttpPut]
        [Route("Clear")]
        public async Task<ActionResult<APIReturn<bool>>> Clear([FromBody] Group_Clear_Request request)
        {
            await _groupService.Clear(request);
            return Success("Cleared", true);
        }

        [HttpGet]
        [ProducesResponseType(typeof(APIReturn<Group_GetAll_Response>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIReturn<Group_GetAll_Response>>> GetAll()
        {
            return Success("UserList", await _baseGroupService.GetAll());
        }

        [HttpPut]
        [Route("Hide")]
        [ProducesResponseType(typeof(APIReturn<bool>), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIReturn<bool>>> Hide(Group_Update_HideState_Request request)
        {
            return Success("HidePoints", await _groupService.UpdateHideState(request));
        }
    }
}
