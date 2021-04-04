using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class FilterPatinetAnswerDto:IPaging
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
