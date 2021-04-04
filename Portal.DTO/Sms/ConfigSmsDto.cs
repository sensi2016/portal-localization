using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class ConfigSmsDto
    {
        public string ApiToken { get; set; }
        public string Url { get; set; }
        public int LimitedSendSmsOnDay { get; set; }
        public int LimitedSendSmsTotal { get; set; }
    }
}
