using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Covid
{
    public class ReceiptDto
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string FullName { get; set; }
        public string Date { get; set; }
        public string AnswerDate { get; set; }
    }
}
