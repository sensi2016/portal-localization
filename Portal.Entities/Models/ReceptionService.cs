using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ReceptionService
    {
        public ReceptionService()
        {
            Answer1 = new HashSet<Answer1>();
        }

        public long Id { get; set; }
        public long? ReceptionId { get; set; }
        public long? DetailReceptionId { get; set; }
        public int? ServiceId { get; set; }
        public long? PrescriptionId { get; set; }
        public long? RequestId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int? ServiceChildId { get; set; }
        public decimal? SectionDiscount { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? DoctorId { get; set; }
        public int? NurseId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? DoctorPercent { get; set; }
        public int? PriceMethodTypeId { get; set; }
        public string Note { get; set; }
        public decimal? PatientShare { get; set; }
        public decimal? TechnicalShare { get; set; }
        public decimal? PrescriptionShare { get; set; }
        public decimal? Providershare { get; set; }
        public decimal? ExtraPayment { get; set; }
        public decimal? DoctorDiscount { get; set; }
        public decimal? FinalPrice { get; set; }
        public int? SectionId { get; set; }
        public int? ServiceTypeId { get; set; }
        public int? StatusId { get; set; }
        public long? ParentReceptionServiceId { get; set; }
        public long? PrescriptionDetailServiceId { get; set; }
        public int? AnswerUserId { get; set; }

        public virtual Users AnswerUser { get; set; }
        public virtual Doctors Doctor { get; set; }
        public virtual Prescription Prescription { get; set; }
        public virtual PrescriptionDetailService PrescriptionDetailService { get; set; }
        public virtual PriceTypeMethod PriceMethodType { get; set; }
        public virtual Receptions Reception { get; set; }
        public virtual Request Request { get; set; }
        public virtual Section Section { get; set; }
        public virtual Services Service { get; set; }
        public virtual Services ServiceChild { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public virtual ReceptionServiceStatus Status { get; set; }
        public virtual ICollection<Answer1> Answer1 { get; set; }
    }
}
