using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Prescription
    {
        public Prescription()
        {
            PrescriptionAllergy = new HashSet<PrescriptionAllergy>();
            PrescriptionDetailDrug = new HashSet<PrescriptionDetailDrug>();
            PrescriptionDetailDrugHistory = new HashSet<PrescriptionDetailDrugHistory>();
            PrescriptionDetailPharmacistNote = new HashSet<PrescriptionDetailPharmacistNote>();
            PrescriptionDetailService = new HashSet<PrescriptionDetailService>();
            PrescriptionDetailServiceHistory = new HashSet<PrescriptionDetailServiceHistory>();
            PrescriptionDiet = new HashSet<PrescriptionDiet>();
            PrescriptionPanel = new HashSet<PrescriptionPanel>();
            PrescriptionServiceResult = new HashSet<PrescriptionServiceResult>();
            PrescriptionShare = new HashSet<PrescriptionShare>();
            PrescriptionSign = new HashSet<PrescriptionSign>();
            ReceptionDiagnosis = new HashSet<ReceptionDiagnosis>();
            ReceptionService = new HashSet<ReceptionService>();
            Request = new HashSet<Request>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? ReceptionId { get; set; }
        public int? SectionId { get; set; }
        public int? DoctorId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? PrescriptionDate { get; set; }
        public string PostMedicalHistory { get; set; }
        public string Biography { get; set; }
        public string Diet { get; set; }
        public string InteractionsNote { get; set; }
        public string PharmacistNoteForNursing { get; set; }
        public string MainDoctorNote { get; set; }
        public string Note { get; set; }
        public int? VisitTypeId { get; set; }

        public virtual Doctors Doctor { get; set; }
        public virtual Receptions Reception { get; set; }
        public virtual Role Role { get; set; }
        public virtual Section Section { get; set; }
        public virtual Users User { get; set; }
        public virtual VisitType VisitType { get; set; }
        public virtual ICollection<PrescriptionAllergy> PrescriptionAllergy { get; set; }
        public virtual ICollection<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
        public virtual ICollection<PrescriptionDetailDrugHistory> PrescriptionDetailDrugHistory { get; set; }
        public virtual ICollection<PrescriptionDetailPharmacistNote> PrescriptionDetailPharmacistNote { get; set; }
        public virtual ICollection<PrescriptionDetailService> PrescriptionDetailService { get; set; }
        public virtual ICollection<PrescriptionDetailServiceHistory> PrescriptionDetailServiceHistory { get; set; }
        public virtual ICollection<PrescriptionDiet> PrescriptionDiet { get; set; }
        public virtual ICollection<PrescriptionPanel> PrescriptionPanel { get; set; }
        public virtual ICollection<PrescriptionServiceResult> PrescriptionServiceResult { get; set; }
        public virtual ICollection<PrescriptionShare> PrescriptionShare { get; set; }
        public virtual ICollection<PrescriptionSign> PrescriptionSign { get; set; }
        public virtual ICollection<ReceptionDiagnosis> ReceptionDiagnosis { get; set; }
        public virtual ICollection<ReceptionService> ReceptionService { get; set; }
        public virtual ICollection<Request> Request { get; set; }
    }
}
