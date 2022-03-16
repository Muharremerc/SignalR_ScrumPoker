using Microsoft.AspNetCore.Mvc;
using ScrumPokerAPI.Model;
using ScrumPokerAPI.Restful.Request.Point;
using ScrumPokerAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Controllers
{
    public class PointController : APIControllerBase
    {
        private readonly IPointService _pointService;
        public PointController(IPointService pointService)
        {
            _pointService = pointService;
        }

        [HttpPost]
        [Route("Give")]
        public async Task<ActionResult<APIReturn<bool>>> Give([FromBody] Point_Give_Request request)
        {
            await _pointService.GivePoint(request);
            return Success("GivePoint", true);
        }
    }
}
