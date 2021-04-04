using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ReceptionHistory
    {
        public long Id { get; set; }
        public long? ReceptionId { get; set; }
        public int? PatientStatusId { get; set; }
        public int? SectionStatusId { get; set; }
        public int? ClearanceTypeId { get; set; }
        public int? UserId { get; set; }
        public bool? IsSectionStatus { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? SectionId { get; set; }
        public int? DoctorId { get; set; }
        public bool? IsCurrent { get; set; }
        public string Note { get; set; }
        public int? RoleId { get; set; }

        public virtual ClearanceType ClearanceType { get; set; }
        public virtual Doctors Doctor { get; set; }
        public virtual GeneralStatus PatientStatus { get; set; }
        public virtual Receptions Reception { get; set; }
        public virtual Role Role { get; set; }
        public virtual Section Section { get; set; }
        public virtual SectionStatus SectionStatus { get; set; }
        public virtual Users User { get; set; }
    }
}
