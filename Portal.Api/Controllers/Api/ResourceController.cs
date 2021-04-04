using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Portal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IOptions<RequestLocalizationOptions> _locOption;
        private readonly IPortalResourceService _portalResourceService;

        public ResourceController(IStringLocalizer<SharedResource> stringLocalizer, IOptions<RequestLocalizationOptions> locOption,
            IPortalResourceService portalResourceService)
        {
            _sharedLocalizer = stringLocalizer;
            _locOption = locOption;
            _portalResourceService = portalResourceService;
        }

        /// <summary>
        /// Get All Languages
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllLanguage()
        {
            var cultures = _locOption.Value.SupportedCultures.Select(g => g.Name).ToList();

            return Ok(new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = cultures
            });
        }

        /// <summary>
        /// Get Resource By Language
        /// </summary>
        [HttpPost()]
        public async Task<IActionResult> GetResource([FromBody] ResourceDto resourceDto)
        {
            var cultures = _locOption.Value.SupportedCultures.ToList();

            if (!cultures.Any(d => d.Name == resourceDto.Language))
            {
                return BadRequest(new BaseResponseDto
                {
                    Status = ResponseStatus.NotFound,
                    Message = "Culture Not Found"
                });
            }

            var query = _sharedLocalizer.WithCulture(new CultureInfo(resourceDto.Language)).GetAllStrings().AsQueryable();
            var predicate = PredicateBuilder.New<LocalizedString>();

            foreach (var item in resourceDto.Actions)
            {
                predicate = predicate.Or(d => d.Name.Contains(item));
            }

            var lstDic = new object();
            var dictionary = new Dictionary<string, object>();

            try
            {
                if (resourceDto.IsKeyValue)
                {
                    lstDic = query.Where(predicate).Select(g => DotNotationToDictionary(g, dictionary)).ToList();
                    lstDic = dictionary;
                }
                else
                {
                    lstDic = query.Where(predicate).Select(x => new {Name = x.Name, Value = x.Value}).ToList();
                }
            }
            catch
            {
                return BadRequest(new BaseResponseDto
                {
                    Status = ResponseStatus.NotFound,
                    Message = "Culture Not Found"
                });
            }

            return Ok(new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lstDic
            });
        }

        private Dictionary<string, object> DotNotationToDictionary(LocalizedString dotNotation, Dictionary<string, object> dictionary)
        {
            //Dictionary<string, object> dictionary = new Dictionary<string, object>();

            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add(dotNotation.Name, dotNotation.Value);

            foreach (var dotObject in dic)
            {
                var hierarcy = dotObject.Key.Split('.');

                Dictionary<string, object> bottom = dictionary;

                for (int i = 0; i < hierarcy.Length; i++)
                {
                    var key = hierarcy[i];

                    if (i == hierarcy.Length - 1) // Last key
                    {
                        bottom.Add(key, dotObject.Value);
                    }
                    else
                    {
                        if (!bottom.ContainsKey(key))
                            bottom.Add(key, new Dictionary<string, object>());

                        bottom = (Dictionary<string, object>)bottom[key];
                    }
                }
            }

            return dictionary;
        }

        /// <summary>
        /// New Translate (Add and Edit)
        /// </summary>
        [HttpPost("New")]
        public async Task<IActionResult> New(NewResourceDto newResource)
        {
            var result = await _portalResourceService.New(newResource);
            return new CustomActionResult(result);
        }
    }
}