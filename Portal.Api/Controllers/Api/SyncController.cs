using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portal.Api.Infrastructure;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.DTO.User;
using Portal.Infrastructure;
using Portal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Api.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SyncController : ControllerBase
    {
        private readonly ISyncService _syncService;
        private readonly ILogger _logger;

        public SyncController(ISyncService syncService,
            ILogger<SyncController> logger)
        {
            _syncService = syncService;
            _logger = logger;
        }

        [HttpPost("SyncDoctor")]
        public async Task<IActionResult> RegisterDoctorAsync([FromBody] SyncDoctorDto doctorDto)
        {
            _logger.LogInformation("SyncDoctor", Newtonsoft.Json.JsonConvert.SerializeObject(doctorDto));

            try
            {
                return new CustomActionResult(await _syncService.RegisterDoctorAsync(doctorDto));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"SyncDoctor_ex_{ex.Message}", Newtonsoft.Json.JsonConvert.SerializeObject(doctorDto));
                return new CustomActionResult(new LoginResponseDto
                {
                    Status = ResponseStatus.Fail,
                    Errors = ex.Message
                });
            }
        }

        [HttpPost("SyncData")]
        [CustomAuthorization]
        public async Task<IActionResult> SyncDataAsync([FromBody] SyncDto syncDto)
        {
            var message = "";
            try
            {
                message = await _syncService.SyncData(syncDto);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                _logger.LogInformation($"SyncData_{syncDto.Data.Table}_{syncDto.Data.ServerId}_{message}", Newtonsoft.Json.JsonConvert.SerializeObject(syncDto));
            }
            
            return new CustomActionResult(new LoginResponseDto
            {
                Status = message.Contains("Success") ? ResponseStatus.Success : ResponseStatus.Fail,
                Message = message
            });
        }
    }
}
