using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionChartCancelationReason
    {
        public PrescriptionChartCancelationReason()
        {
            PrescriptionDrugChart = new HashSet<PrescriptionDrugChart>();
            PrescriptionServiceChart = new HashSet<PrescriptionServiceChart>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public bool? IsAdmin { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<PrescriptionDrugChart> PrescriptionDrugChart { get; set; }
        public virtual ICollection<PrescriptionServiceChart> PrescriptionServiceChart { get; set; }
    }
}
