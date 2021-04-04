using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionRoutine
    {
        public PrescriptionRoutine()
        {
            PrescriptionDrugRoutine = new HashSet<PrescriptionDrugRoutine>();
            PrescriptionServiceRoutine = new HashSet<PrescriptionServiceRoutine>();
        }

        public long Id { get; set; }
        public int? SectionId { get; set; }
        public string Title { get; set; }
        public int? PanelTypeId { get; set; }
        public bool? IsDrugType { get; set; }

        public virtual PanelType PanelType { get; set; }
        public virtual ICollection<PrescriptionDrugRoutine> PrescriptionDrugRoutine { get; set; }
        public virtual ICollection<PrescriptionServiceRoutine> PrescriptionServiceRoutine { get; set; }
    }
}
