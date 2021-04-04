using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionAllergy
    {
        public long Id { get; set; }
        public int? AllergyId { get; set; }
        public long? PrescriptionId { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Allergy Allergy { get; set; }
        public virtual Prescription Prescription { get; set; }
    }
}
