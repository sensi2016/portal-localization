using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Instruction
    {
        public int Id { get; set; }
        public int? SexId { get; set; }
        public int? ServiceId { get; set; }
        public int? FromAge { get; set; }
        public int? ToAge { get; set; }
        public bool? IsPregnant { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }

        public virtual Services Service { get; set; }
        public virtual Sex Sex { get; set; }
    }
}
