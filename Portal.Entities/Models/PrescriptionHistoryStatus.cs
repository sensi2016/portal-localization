using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionHistoryStatus
    {
        public PrescriptionHistoryStatus()
        {
            PrescriptionDetailDrugHistory = new HashSet<PrescriptionDetailDrugHistory>();
            PrescriptionDetailServiceHistory = new HashSet<PrescriptionDetailServiceHistory>();
        }

        public int Id { get; set; }
        public string StatusTitle { get; set; }

        public virtual ICollection<PrescriptionDetailDrugHistory> PrescriptionDetailDrugHistory { get; set; }
        public virtual ICollection<PrescriptionDetailServiceHistory> PrescriptionDetailServiceHistory { get; set; }
    }
}
