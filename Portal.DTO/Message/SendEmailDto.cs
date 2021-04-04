using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class SendEmailDto
    {
        public string Email { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
