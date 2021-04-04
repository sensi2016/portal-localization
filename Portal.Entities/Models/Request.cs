using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Request
    {
        public Request()
        {
            ReceptionDrug = new HashSet<ReceptionDrug>();
            ReceptionService = new HashSet<ReceptionService>();
            RequestHistory = new HashSet<RequestHistory>();
        }

        public long Id { get; set; }
        public long? ReceptionId { get; set; }
        public long? PrescriptionId { get; set; }
        public long? RequestCode { get; set; }
        public int? RequestTypeId { get; set; }
        public int? SourceSectionId { get; set; }
        public int? TargetSectionId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UserId { get; set; }
        public int? DoctorId { get; set; }
        public int? PriorityId { get; set; }
        public DateTime? PrescriptionDate { get; set; }
        public DateTime? DoneDate { get; set; }
        public string Note { get; set; }
        public int? RoleId { get; set; }

        public virtual Doctors Doctor { get; set; }
        public virtual Prescription Prescription { get; set; }
        public virtual Receptions Reception { get; set; }
        public virtual RequestType RequestType { get; set; }
        public virtual Role Role { get; set; }
        public virtual Section SourceSection { get; set; }
        public virtual Section TargetSection { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<ReceptionDrug> ReceptionDrug { get; set; }
        public virtual ICollection<ReceptionService> ReceptionService { get; set; }
        public virtual ICollection<RequestHistory> RequestHistory { get; set; }
    }
}
