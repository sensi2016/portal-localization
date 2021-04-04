using Portal.DAL.Extensions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.DTO.User;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Service
{
    public class SmsService : ISmsService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ConfigSmsDto> _configSms;
        public SmsService(HttpClient httpClientFactory, IOptions<ConfigSmsDto> configSms)
        {
            _httpClient = httpClientFactory;
            _configSms = configSms;
        }

        public async Task<BaseResponseDto> SendSms(SendSmsDto SendSmsDto)
        {
            _httpClient.DefaultRequestHeaders.Add("ApiToken", _configSms.Value.ApiToken);

            var result = await _httpClient.PostAsJsonAsync(_configSms.Value.Url, SendSmsDto);

            var response = await result.Content.ReadAsStringAsync();


            if (!response.IsValidateJSON())
            {
                return new BaseResponseDto
                {
                    Status = ResponseStatus.Fail
                };
            }

            var basedto = JsonConvert.DeserializeObject<BaseResponseDto>(response);

            if ((int)basedto.Status == 3 || (int)basedto.Status == 401)
            {
                return new BaseResponseDto
                {
                    Status = ResponseStatus.Fail
                };
            }

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success
            };
        }
    }
}
