using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class WayOfPrescription
    {
        public WayOfPrescription()
        {
            PrescriptionDetailDrug = new HashSet<PrescriptionDetailDrug>();
            PrescriptionDrugRoutine = new HashSet<PrescriptionDrugRoutine>();
            RequestDetail = new HashSet<RequestDetail>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
        public virtual ICollection<PrescriptionDrugRoutine> PrescriptionDrugRoutine { get; set; }
        public virtual ICollection<RequestDetail> RequestDetail { get; set; }
    }
}
