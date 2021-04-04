using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class SectionEssentialsDrug
    {
        public int Id { get; set; }
        public int? DrugId { get; set; }
        public int? OrderPoint { get; set; }
        public int? SectionId { get; set; }

        public virtual Drugs Drug { get; set; }
        public virtual Section Section { get; set; }
    }
}
