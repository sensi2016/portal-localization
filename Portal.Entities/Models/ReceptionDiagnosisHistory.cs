using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ReceptionDiagnosisHistory
    {
        public long Id { get; set; }
        public int? UserId { get; set; }
        public long? ReceptionDiagnosisId { get; set; }
        public int? DiagnosisStatusId { get; set; }
        public DateTime? Date { get; set; }
        public string Note { get; set; }
        public bool? IsCurrent { get; set; }
        public int? RoleId { get; set; }

        public virtual DiagnosisStatus DiagnosisStatus { get; set; }
        public virtual ReceptionDiagnosis ReceptionDiagnosis { get; set; }
        public virtual Role Role { get; set; }
        public virtual Users User { get; set; }
    }
}
