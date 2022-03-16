using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using ScrumPokerAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ScrumPokerAPI.Middleware
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {

                    if ((context.Features.Get<IExceptionHandlerFeature>().Error is Exception))
                    {
                        var error = context.Features.Get<IExceptionHandlerFeature>().Error as Exception;
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = 400;
                        var apiResponse = Newtonsoft.Json.JsonConvert.SerializeObject(new
                        APIReturn<bool>
                        {
                            HttpStatus = HttpStatusCode.BadRequest,
                            Message = error.Message,
                            Data = false
                        });

                        await context.Response.WriteAsync(apiResponse);
                    }
                });
            });
        }
    }
}
