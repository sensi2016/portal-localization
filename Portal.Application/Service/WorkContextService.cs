using Microsoft.AspNetCore.Http;
using Portal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace His.Reception.Application.Service
{
    public class WorkContextService : IWorkContextService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public WorkContextService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string Token => _contextAccessor.HttpContext.User.Claims.Where(c => c.Type == "Token").FirstOrDefault().Value;

        public string UserName => _contextAccessor.HttpContext.User.Identity.Name;
        public int? SectionId
        {
            get
            {
                var sectionId = _contextAccessor.HttpContext.User.Claims.Where(d => d.Type == "CurrentSectionId").FirstOrDefault().Value;

                if (int.TryParse(sectionId, out var resul))
                {
                    return resul;
                }

                return null;
            }
        }

        public int? UserId
        {
            get
            {
                var userId = _contextAccessor.HttpContext.User.Identity.Name;

                if (int.TryParse(userId, out var resul))
                {
                    return resul;
                }

                return null;
            }
        }

        public int? RoleId
        {
            get
            {
                var RoleId = _contextAccessor.HttpContext.User.Claims.Where(d => d.Type == "RoleId").FirstOrDefault().Value;

                if (int.TryParse(RoleId, out var resul))
                {
                    return resul;
                }

                return null;
            }
        }

        public string Language => _contextAccessor.HttpContext.User.Claims.Where(c => c.Type == "Language").FirstOrDefault().Value;
    }
}
