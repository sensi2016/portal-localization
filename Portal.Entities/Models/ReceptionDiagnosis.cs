using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ReceptionDiagnosis
    {
        public ReceptionDiagnosis()
        {
            ReceptionDiagnosisHistory = new HashSet<ReceptionDiagnosisHistory>();
        }

        public long Id { get; set; }
        public int? DiagnosisId { get; set; }
        public long? ReceptionId { get; set; }
        public long? PrescriptionId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Note { get; set; }

        public virtual Diagnosis Diagnosis { get; set; }
        public virtual Prescription Prescription { get; set; }
        public virtual Receptions Reception { get; set; }
        public virtual ICollection<ReceptionDiagnosisHistory> ReceptionDiagnosisHistory { get; set; }
    }
}
