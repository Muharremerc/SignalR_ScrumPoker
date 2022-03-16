using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Model
{
    public class APIReturn<T>
    {
        public HttpStatusCode  HttpStatus{ get; set; }
        public T Data { get; set; }
        public string Message { get; set; }     
    }
}
