using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Message
{
    public class ReceptionIdDto
    {
      public long ReceptionId { get; set; }
    }

    public class VerifyReceptionIdDto
    {
        public long ReceptionId { get; set; }
        public string VerifyCode { get; set; }
    }
}
