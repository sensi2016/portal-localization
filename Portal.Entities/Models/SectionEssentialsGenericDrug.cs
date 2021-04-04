using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class SectionEssentialsGenericDrug
    {
        public int Id { get; set; }
        public int? SectionId { get; set; }
        public int? GenericDrugId { get; set; }
        public int? DrugFormId { get; set; }
        public int? OrderPoint { get; set; }

        public virtual DrugForm DrugForm { get; set; }
        public virtual GenericDrug GenericDrug { get; set; }
        public virtual Section Section { get; set; }
    }
}
