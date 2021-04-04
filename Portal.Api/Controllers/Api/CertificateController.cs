using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Portal.Api.Infrastructure;
using Portal.Application.Interface.Base;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;

namespace Portal.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly IBasicService<Certificate> _basicService;
        public CertificateController(IBasicService<Certificate> basicService)=> _basicService = basicService;
        
        [CustomAuthorization]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _basicService.Get(id);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            var result = await _basicService.GetAll();
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPost("List")]
        public async Task<IActionResult> ListPaging(BaseRequestPost<int> dto)
        {
            var result = await _basicService.GetListPaging(dto);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPost]
        public async Task<IActionResult> Add(RequestBaseDto requestBaseDto)
        {
            var result = await _basicService.AddAsync(requestBaseDto);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpPut]
        public async Task<IActionResult> Edit(RequestBaseDto requestBaseDto)
        {
            var result = await _basicService.EditAsync(requestBaseDto);
            return new CustomActionResult(result);
        }

        [CustomAuthorization]
        [HttpDelete]
        public async Task<IActionResult> Delete(BaseRequestPost<int> dto)
        {
            var result = await _basicService.DeleteAsync(dto);
            return new CustomActionResult(result);
        }
    }
}
