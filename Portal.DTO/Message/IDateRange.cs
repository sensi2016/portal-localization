using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Message
{
    public interface IDateRange
    {
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
    }
}
