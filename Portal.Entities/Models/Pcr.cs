using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Pcr
    {
        public long Id { get; set; }
        public int? SectionId { get; set; }
        public short? Age { get; set; }
        public DateTime? AnswerDate { get; set; }
        public string Result { get; set; }
    }
}
