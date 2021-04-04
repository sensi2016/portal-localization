using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface ISmsService
    {
        Task<BaseResponseDto> SendSms(SendSmsDto SendSmsDto);
    }
}
