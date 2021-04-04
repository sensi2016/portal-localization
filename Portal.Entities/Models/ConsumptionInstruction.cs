using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ConsumptionInstruction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Code { get; set; }
        public int? Period { get; set; }
    }
}
