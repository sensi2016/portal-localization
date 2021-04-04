using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Covid
{
    public class RequestReceptionAnswerDto
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public string InfoDate  { get; set; }
    }
}
