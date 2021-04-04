

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Portal.DAL;
using Portal.DTO;
using Portal.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Infrastructure
{

    public class CustomAuthorization : ActionFilterAttribute
    {
        public string PermissionName { get; set; }
        public string PageName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
#if !DEBUG

            #region init

            var userManagerService = (IUserManagerService)context.HttpContext.RequestServices.GetRequiredService(typeof(IUserManagerService));
            var sharedLocalizer = (IStringLocalizer<SharedResource>)context.HttpContext.RequestServices.GetRequiredService(typeof(IStringLocalizer<SharedResource>));

            var token = context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var sectionId = context.HttpContext.Request.Headers["SectionId"].ToString();
            var lang = context.HttpContext.Request.Headers["Accept-Language"].ToString();
            //این برای استفاده داخلی است بیین میکرو سرویس هاست تا پرمیژن چک شود
            var per = context.HttpContext.Request.Headers["PermissionName"].ToString();

            if (string.IsNullOrEmpty(per)) PermissionName = per;

            if (string.IsNullOrEmpty(token))
            { throw new UnauthorizedException(HttpStatusCode.Unauthorized, "", "GlobalForm.Response.TokenNotSend"); }

            #endregion

            #region check Token and get User

            var result = Task.Run(() => userManagerService.CheckToken(Guid.Parse(token))).GetAwaiter().GetResult();

            if (result is null)
            {
                throw new UnauthorizedException(HttpStatusCode.Unauthorized, "", "GlobalForm.Response.TokenExpire");
            }

            #endregion

            #region set Language

            if (!string.IsNullOrEmpty(result.Language))
            {
                var cultureInfo = CultureInfo.GetCultureInfo(result.Language);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }

            #endregion

            #region get Access User

            if (!string.IsNullOrEmpty(PermissionName))
            {
                var userPermission = Task.Run(() => userManagerService.GetPermissionsAsync(result.UserId)).GetAwaiter().GetResult();
                //multi permission
                var arrPermission = PermissionName.Split(',', '&');
                //get all permission 
                var allPerms = new List<PermissionDto>();

                if (string.IsNullOrEmpty(PageName))
                {
                    allPerms = userPermission.Where(up => up.SectionId == result.CurrentSectionId && up.PageAdress == PageName && up.UserId == result.UserId && arrPermission.Contains(up.PermissionName)).ToList();
                }
                else
                {
                    allPerms = userPermission.Where(up => up.SectionId == result.CurrentSectionId && up.UserId == result.UserId && arrPermission.Contains(up.PermissionName)).ToList();
                }

                var isAuth = true;
                //time permission and check
                if (PermissionName.Contains("&"))
                {
                    var arr = PermissionName.Split(',');
                    if (!allPerms.Any(d => arr.Contains(d.PermissionName)))
                    {
                        foreach (var item in arr)
                        {
                            if (item.Contains("&"))
                            {
                                var sub = item.Split("&");

                                foreach (var subitem in sub)
                                {
                                    if (!allPerms.Any(d => d.PermissionName == subitem))
                                    {
                                        isAuth = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                }


                if (allPerms is null || allPerms.Count == 0 || isAuth == false)
                {
                    context.Result = new UnauthorizedObjectResult("Not Access To Action"); //new JsonResult(new { HttpStatusCode.Unauthorized });
                    return;
                }
            }

            #endregion

            #region set Context User Identity

            var userIdentity = new ClaimsIdentity("Identity");
            context.RouteData.Values.Add("UserLoginInfo", result);
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, result.UserId.ToString()));
            userIdentity.AddClaim(new Claim("Token", result.Token.ToString()));

            //set lang from database
            if (!string.IsNullOrEmpty(result.Language))
            {
                userIdentity.AddClaim(new Claim("Language", result.Language.ToString()));
                Utilities.Language.CurrentLanguage = result.Language.ToString();
            }

            userIdentity.AddClaim(new Claim("CurrentSectionId", result.CurrentSectionId.ToString()));
            userIdentity.AddClaim(new Claim("RoleId", result.RoleId.ToString()));

            var identity = new ClaimsPrincipal(userIdentity);
            context.HttpContext.User = identity;

            #endregion

#else

            var userIdentity = new ClaimsIdentity("Identity");
            userIdentity.AddClaim(new Claim(ClaimTypes.Name, 129.ToString()));
            userIdentity.AddClaim(new Claim("Token", "token"));

            //set lang from database
            userIdentity.AddClaim(new Claim("Language", "AR-Iq"));
            userIdentity.AddClaim(new Claim("CurrentSectionId", 1.ToString()));
            userIdentity.AddClaim(new Claim("RoleId", 51.ToString()));
            
            var identity = new ClaimsPrincipal(userIdentity);
            context.HttpContext.User = identity;
            
#endif

            base.OnActionExecuting(context);
        }
    }
}
