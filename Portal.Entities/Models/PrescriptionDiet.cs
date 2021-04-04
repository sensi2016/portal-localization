using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionDiet
    {
        public long Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? PrescriptionId { get; set; }
        public int? DietId { get; set; }
        public string Note { get; set; }

        public virtual Diet Diet { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
