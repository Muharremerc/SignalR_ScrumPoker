using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScrumPokerAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Controllers
{
    
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(APIReturn<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(APIReturn<bool>), StatusCodes.Status404NotFound)]
    public class APIControllerBase : ControllerBase
    {
        [NonAction]
        protected ActionResult Success<T>(string message, T data)
        {
            return Success(new APIReturn<T>
            {
                Data = data,
                Message = message,
                HttpStatus = HttpStatusCode.OK
            });
        }


        [NonAction]
        protected ActionResult Success<T>(APIReturn<T> data)
        {
            return Ok(data);
        }

    }
}
