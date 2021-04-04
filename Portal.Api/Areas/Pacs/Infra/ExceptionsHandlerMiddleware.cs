using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace His.Reception.Api
{
    public static class UseExceptionsHandlerExtensions
    {
        public static IApplicationBuilder UseExceptionsHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionsHandlerMiddleware>();
        }

    }

    public class ExceptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionsHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.ToString().Contains("/api/Pacs"))
            {
                await _next(context);
            }
            else
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex )
                {
                    var data = JsonConvert.SerializeObject(new ApiResult(false, ex.Message));

                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "appliction/json";
                    await context.Response.WriteAsync(data);
                }
            }


        }

    }
}
