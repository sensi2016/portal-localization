using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.DTO;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public EmailService(IConfiguration configuration, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _configuration = configuration;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<BaseResponseDto> SendEmail(SendEmailDto sendEmailDto)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(_configuration["EmailConfig: Email"]);
            msg.To.Add(sendEmailDto.Email);
            msg.Subject = sendEmailDto.Subject;
            msg.Body = sendEmailDto.Body;
            //msg.Priority = MailPriority.

            using (SmtpClient smtp = new SmtpClient())
            {

                smtp.Host = _configuration["EmailConfig:Host"];
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(_configuration["EmailConfig:Email"], _configuration["EmailConfig:Password"]);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = Convert.ToInt32(_configuration["EmailConfig:Port"]);
                smtp.Send(msg);
            }

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success
            };
        }
    }
}
