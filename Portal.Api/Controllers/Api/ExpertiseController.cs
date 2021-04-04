using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Infrastructure;
using Portal.Application.Interface.Base;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpertiseController : ControllerBase
    {
        private readonly IBasicService<Expertise> _basicService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="basicService"></param>
        public ExpertiseController(IBasicService<Expertise> basicService)
        {
            _basicService = basicService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CustomAuthorization()]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _basicService.Get(id);
            return new CustomActionResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [CustomAuthorization()]
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var result = await _basicService.GetAll();
            return new CustomActionResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseRequestPost"></param>
        /// <returns></returns>
        [CustomAuthorization()]
        [HttpPost("List")]
        public async Task<IActionResult> ListPaging(BaseRequestPost<int> baseRequestPost)
        {
            var result = await _basicService.GetListPaging(baseRequestPost);
            return new CustomActionResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestBaseDto"></param>
        /// <returns></returns>
        [CustomAuthorization()]
        [HttpPost]
        public async Task<IActionResult> Add(RequestBaseDto requestBaseDto)
        {
            var result = await _basicService.AddAsync(requestBaseDto);
            return new CustomActionResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestBaseDto"></param>
        /// <returns></returns>
        [CustomAuthorization()]
        [HttpPut]
        public async Task<IActionResult> Edit(RequestBaseDto requestBaseDto)
        {
            var result = await _basicService.EditAsync(requestBaseDto);
            return new CustomActionResult(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseRequestPost"></param>
        /// <returns></returns>
        [CustomAuthorization()]
        [HttpDelete]
        public async Task<IActionResult> Delete(BaseRequestPost<int> baseRequestPost)
        {
            var result = await _basicService.DeleteAsync(baseRequestPost);
            return new CustomActionResult(result);
        }
    }
}
