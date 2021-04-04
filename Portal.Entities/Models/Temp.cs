using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Temp
    {
        public long Id { get; set; }
        public int? PatientId { get; set; }
        public long? ReceptionId { get; set; }
        public DateTime? ReceptionDate { get; set; }
        public int? SectionId { get; set; }
        public int? ReceptionTypeId { get; set; }
        public int? HospitalizationDoctorId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? RefferDate { get; set; }
        public int? RefferReasonId { get; set; }
        public int? CurrentIllnessId { get; set; }
        public int? GeneralStatusId { get; set; }
        public string ChiefComplaints { get; set; }
        public string Advice { get; set; }
        public int? RefferFromId { get; set; }
        public string Note { get; set; }
        public int? PresenterId { get; set; }
        public int? RelationId { get; set; }
        public string ConsumeDrug { get; set; }
        public bool? IsHaveSign { get; set; }
        public DateTime? DateOfSign { get; set; }
        public DateTime? HospitalEnteryDate { get; set; }
        public DateTime? RecoveryDate { get; set; }
        public bool? IsResult { get; set; }
        public string ResultNote { get; set; }
        public int? ParentId { get; set; }
        public bool? IsVerify { get; set; }
        public DateTime? AnswerDate { get; set; }
    }
}
