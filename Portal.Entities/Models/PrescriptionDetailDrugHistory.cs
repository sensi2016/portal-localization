using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionDetailDrugHistory
    {
        public long Id { get; set; }
        public int? SectionId { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? PrescriptionId { get; set; }
        public long? PrescriptionDetailDrugId { get; set; }
        public int? PrescriptionHistoryStatusId { get; set; }
        public DateTime? StatusDate { get; set; }
        public string Note { get; set; }
        public bool? IsCurrent { get; set; }
        public DateTime? StopDate { get; set; }

        public virtual Prescription Prescription { get; set; }
        public virtual PrescriptionDetailDrug PrescriptionDetailDrug { get; set; }
        public virtual PrescriptionHistoryStatus PrescriptionHistoryStatus { get; set; }
        public virtual Role Role { get; set; }
        public virtual Section Section { get; set; }
        public virtual Users User { get; set; }
    }
}
