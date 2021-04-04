using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Answer
{
    public class FilterAnswerDto : IPaging
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int? Type { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
