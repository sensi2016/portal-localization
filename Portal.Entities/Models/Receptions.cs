using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Receptions
    {
        public Receptions()
        {
            BasketDrug = new HashSet<BasketDrug>();
            BedReception = new HashSet<BedReception>();
            DoctorAppointmentDetail = new HashSet<DoctorAppointmentDetail>();
            Payment = new HashSet<Payment>();
            Prescription = new HashSet<Prescription>();
            ReceptionAnswer = new HashSet<ReceptionAnswer>();
            ReceptionDetail = new HashSet<ReceptionDetail>();
            ReceptionDiagnosis = new HashSet<ReceptionDiagnosis>();
            ReceptionDrug = new HashSet<ReceptionDrug>();
            ReceptionHistory = new HashSet<ReceptionHistory>();
            ReceptionSectionDoctor = new HashSet<ReceptionSectionDoctor>();
            ReceptionService = new HashSet<ReceptionService>();
            ReceptionSign = new HashSet<ReceptionSign>();
            Request = new HashSet<Request>();
            SmsReception = new HashSet<SmsReception>();
            SpecialIllnessReception = new HashSet<SpecialIllnessReception>();
            VitalSigns = new HashSet<VitalSigns>();
        }

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

        public virtual Illness CurrentIllness { get; set; }
        public virtual Doctors Doctor { get; set; }
        public virtual GeneralStatus GeneralStatus { get; set; }
        public virtual Doctors HospitalizationDoctor { get; set; }
        public virtual Person Parent { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Presenter Presenter { get; set; }
        public virtual ReceptionType ReceptionType { get; set; }
        public virtual RefferFrom RefferFrom { get; set; }
        public virtual RelationShip Relation { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<BasketDrug> BasketDrug { get; set; }
        public virtual ICollection<BedReception> BedReception { get; set; }
        public virtual ICollection<DoctorAppointmentDetail> DoctorAppointmentDetail { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Prescription> Prescription { get; set; }
        public virtual ICollection<ReceptionAnswer> ReceptionAnswer { get; set; }
        public virtual ICollection<ReceptionDetail> ReceptionDetail { get; set; }
        public virtual ICollection<ReceptionDiagnosis> ReceptionDiagnosis { get; set; }
        public virtual ICollection<ReceptionDrug> ReceptionDrug { get; set; }
        public virtual ICollection<ReceptionHistory> ReceptionHistory { get; set; }
        public virtual ICollection<ReceptionSectionDoctor> ReceptionSectionDoctor { get; set; }
        public virtual ICollection<ReceptionService> ReceptionService { get; set; }
        public virtual ICollection<ReceptionSign> ReceptionSign { get; set; }
        public virtual ICollection<Request> Request { get; set; }
        public virtual ICollection<SmsReception> SmsReception { get; set; }
        public virtual ICollection<SpecialIllnessReception> SpecialIllnessReception { get; set; }
        public virtual ICollection<VitalSigns> VitalSigns { get; set; }
    }
}
