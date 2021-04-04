using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Message
{
    public interface IReportParameters
    {
        long ReceptionId { get; set; }
        bool IsPrint { get; set; }
    }
}
