using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionChartActionType
    {
        public PrescriptionChartActionType()
        {
            PrescriptionDrugChart = new HashSet<PrescriptionDrugChart>();
            PrescriptionServiceChart = new HashSet<PrescriptionServiceChart>();
        }

        public int Id { get; set; }
        public string ActionTitle { get; set; }

        public virtual ICollection<PrescriptionDrugChart> PrescriptionDrugChart { get; set; }
        public virtual ICollection<PrescriptionServiceChart> PrescriptionServiceChart { get; set; }
    }
}
