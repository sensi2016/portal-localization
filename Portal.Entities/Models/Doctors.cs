using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Doctors
    {
        public Doctors()
        {
            DoctorAppointment = new HashSet<DoctorAppointment>();
            DoctorCertificate = new HashSet<DoctorCertificate>();
            DoctorDegree = new HashSet<DoctorDegree>();
            DoctorVisitType = new HashSet<DoctorVisitType>();
            Prescription = new HashSet<Prescription>();
            ReceptionDetailPatoDoctor = new HashSet<ReceptionDetail>();
            ReceptionDetailPrescriptionDoctor = new HashSet<ReceptionDetail>();
            ReceptionDrug = new HashSet<ReceptionDrug>();
            ReceptionHistory = new HashSet<ReceptionHistory>();
            ReceptionSectionDoctor = new HashSet<ReceptionSectionDoctor>();
            ReceptionService = new HashSet<ReceptionService>();
            ReceptionsDoctor = new HashSet<Receptions>();
            ReceptionsHospitalizationDoctor = new HashSet<Receptions>();
            Request = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string MedicalSystemNo { get; set; }
        public int? CenterId { get; set; }
        public int? PersonId { get; set; }
        public int? ExpertiseId { get; set; }
        public int? ScientificlevelId { get; set; }
        public DateTime? CreditDate { get; set; }
        public DateTime? CooperationDate { get; set; }
        public string PhoneClinic { get; set; }
        public string AddressClinic { get; set; }
        public bool? IsEmergency { get; set; }
        public bool? IsOutpatient { get; set; }
        public bool? IsHospitalization { get; set; }
        public bool? IsSurgeryRoom { get; set; }
        public bool? IsActive { get; set; }
        public decimal? CostVisit { get; set; }
        public string Note { get; set; }
        public int? DoctorDegreeId { get; set; }

        public virtual Center Center { get; set; }
        public virtual DoctorDegree DoctorDegreeNavigation { get; set; }
        public virtual Expertise Expertise { get; set; }
        public virtual Person Person { get; set; }
        public virtual Scientificlevel Scientificlevel { get; set; }
        public virtual ICollection<DoctorAppointment> DoctorAppointment { get; set; }
        public virtual ICollection<DoctorCertificate> DoctorCertificate { get; set; }
        public virtual ICollection<DoctorDegree> DoctorDegree { get; set; }
        public virtual ICollection<DoctorVisitType> DoctorVisitType { get; set; }
        public virtual ICollection<Prescription> Prescription { get; set; }
        public virtual ICollection<ReceptionDetail> ReceptionDetailPatoDoctor { get; set; }
        public virtual ICollection<ReceptionDetail> ReceptionDetailPrescriptionDoctor { get; set; }
        public virtual ICollection<ReceptionDrug> ReceptionDrug { get; set; }
        public virtual ICollection<ReceptionHistory> ReceptionHistory { get; set; }
        public virtual ICollection<ReceptionSectionDoctor> ReceptionSectionDoctor { get; set; }
        public virtual ICollection<ReceptionService> ReceptionService { get; set; }
        public virtual ICollection<Receptions> ReceptionsDoctor { get; set; }
        public virtual ICollection<Receptions> ReceptionsHospitalizationDoctor { get; set; }
        public virtual ICollection<Request> Request { get; set; }
    }
}
