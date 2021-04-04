using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface IEmailService
    {
        Task<BaseResponseDto> SendEmail(SendEmailDto sendEmailDto);

    }
}
