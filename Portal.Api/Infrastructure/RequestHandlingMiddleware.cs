
using FluentValidation;
using Portal.DAL.Extensions;
using His.Reception.DTO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Portal.DAL;
using Portal.DTO;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using Microsoft.Extensions.Options;

namespace Portal.Api.Infrastructure
{
    public class RequestHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ILogger<RequestHandlingMiddleware> _logger;
        private readonly IOptions<RequestLocalizationOptions> _locOption;

        public RequestHandlingMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor, ILogger<RequestHandlingMiddleware> logger, IOptions<RequestLocalizationOptions> locOption)
        {
            _next = next;
            _contextAccessor = contextAccessor;
            _logger = logger;
            _locOption = locOption;
        }

        public async Task InvokeAsync(HttpContext context, IStringLocalizer<SharedResource> stringLocalizer)
        {
            context.Request.EnableBuffering();
            HttpRequest httpRequest = context.Request;
            var initialBody = context.Request.Body;
            string body = string.Empty;
            Stream stream = context.Response.Body;
            MemoryStream responseBuffer = new MemoryStream();
            try
            {
                var bodyReader = new StreamReader(context.Request.Body);

                body = await bodyReader.ReadToEndAsync();
                // context.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(body));
                context.Request.Body.Position = 0;


                #region init response body 

                //stream = context.Response.Body;
                responseBuffer = new MemoryStream();
                context.Response.Body = responseBuffer;

                #endregion

                SetDefaultCulture(context);

                await _next(context);

                //set response stream 
                responseBuffer.Seek(0, SeekOrigin.Begin);
                var responseBody = new StreamReader(responseBuffer).ReadToEnd();

                await LogResponse(responseBody, context, _logger);

            }
            catch (Exception ex)
            {
                _logger.LogError($"{ ex.Message} {ex.StackTrace}");

                await HandleExceptionAsync(context, ex, stringLocalizer);


            }
            finally
            {

                responseBuffer.Seek(0, SeekOrigin.Begin);
                await responseBuffer.CopyToAsync(stream);
            }
        }


        public static async Task LogResponse(string responseBody, HttpContext httpContext, ILogger logger)
        {
            if (httpContext.Response.StatusCode != 200 && responseBody.IsValidateJSON())
            {
                var result = JsonConvert.DeserializeObject<BaseResponseDto>(responseBody);

                if (result.Status != ResponseStatus.Success)
                {
                    logger.LogError(responseBody);
                }
            }
        }




        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IStringLocalizer<SharedResource> stringLocalizer)
        {
            string result = string.Empty;
            string error = string.Empty;
            string internalError = string.Empty;

            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            internalError = $"{ ex.Message}  {ex.StackTrace}";

            var dicErrorMain = new Dictionary<string, string>()
                        {
                            { "InternalServerError", stringLocalizer["GlobalForm.Response.InternalServerError"] }
                        };

            error = Utilities.CreateErrorMessage("InternalServerError", dicErrorMain);

            ResponseStatus responseStatus = ResponseStatus.Fail;

            if (ex is CanNotDeleteException)
            {
                code = HttpStatusCode.BadRequest;

                var dicError = new Dictionary<string, string>()
                        {
                            { "CanNotDelete",stringLocalizer["GlobalForm.Response.CanNotDelete"] }
                        };

                error = Utilities.CreateErrorMessage("CanNotDelete", dicError);
            }
            else if (ex is UnauthorizedException)
            {
                code = HttpStatusCode.Unauthorized;
                responseStatus = ResponseStatus.Fail;

                var dicError = new Dictionary<string, string>()
                        {
                            { "Unauthorized", stringLocalizer[((UnauthorizedException)ex).ResourceKey] }
                        };

                error = Utilities.CreateErrorMessage("Unauthorized", dicError);
            }


            result = JsonConvert.SerializeObject(new BaseResponseDto { Status = responseStatus, Message = internalError, Errors = error }, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private void SetDefaultCulture(HttpContext context)
        {
            var lang = context.Request.Headers["Accept-Language"].ToString();
            if (string.IsNullOrEmpty(lang))
            {
                var cRes = _locOption.Value.DefaultRequestCulture.Culture.Name;
                var cultureInfo = CultureInfo.GetCultureInfo(cRes);

                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
        }
    }

    public static class RequestHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestHandlingMiddleware>();
        }
    }



}
