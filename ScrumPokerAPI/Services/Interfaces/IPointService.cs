using ScrumPokerAPI.Restful.Request.Point;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Services.Interfaces
{
    public interface IPointService
    {
        public Task GivePoint(Point_Give_Request request);
    }
}
