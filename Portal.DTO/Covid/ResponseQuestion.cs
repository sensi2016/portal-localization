using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Covid
{
    public class ResponseQuestion
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int? Arrange { get; set; }
        public object Answers { get; set; } 
    }
}
