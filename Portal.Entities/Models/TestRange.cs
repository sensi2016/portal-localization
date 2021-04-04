using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class TestRange
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public int? SexId { get; set; }
        public bool? IsPregnant { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public int? LowFrom { get; set; }
        public int? LowTo { get; set; }
        public string LowComment { get; set; }
        public int? NormalFrom { get; set; }
        public int? NormalTo { get; set; }
        public string NormalComment { get; set; }
        public int? CriticalFrom { get; set; }
        public int? CriticalTo { get; set; }
        public string CriticalLowComment { get; set; }
        public string CriticalHighComment { get; set; }
        public int? OutOfRangeMin { get; set; }
        public int? OutOfRangeMax { get; set; }
        public string OutOfRangeComment { get; set; }
        public int? HighFrom { get; set; }
        public int? HighTo { get; set; }

        public virtual Services Service { get; set; }
        public virtual Sex Sex { get; set; }
    }
}
