using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionPanel
    {
        public PrescriptionPanel()
        {
            PrescriptionDetailDrug = new HashSet<PrescriptionDetailDrug>();
            PrescriptionDetailService = new HashSet<PrescriptionDetailService>();
        }

        public long Id { get; set; }
        public long? PrescriptionId { get; set; }
        public string Title { get; set; }
        public int? Period { get; set; }
        public DateTime? StartDate { get; set; }
        public bool? IsJustOnTime { get; set; }
        public bool? IsDrugType { get; set; }

        public virtual Prescription Prescription { get; set; }
        public virtual ICollection<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
        public virtual ICollection<PrescriptionDetailService> PrescriptionDetailService { get; set; }
    }
}
