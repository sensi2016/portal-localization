using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ReceptionDrug
    {
        public long Id { get; set; }
        public long? ReceptionId { get; set; }
        public int? DrugId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public decimal? SectionDiscount { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? DoctorId { get; set; }
        public int? NurseId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? DoctorPercent { get; set; }
        public int? PriceMethodTypeId { get; set; }
        public string Note { get; set; }
        public decimal? PatientShare { get; set; }
        public decimal? Providershare { get; set; }
        public decimal? ExtraPayment { get; set; }
        public decimal? TechnicalShare { get; set; }
        public decimal? PrescriptionShare { get; set; }
        public long? PrescriptionId { get; set; }
        public long? RequestId { get; set; }
        public decimal? FinalPrice { get; set; }
        public int? SectionId { get; set; }
        public int? ConsumerSectionId { get; set; }
        public int? StatusId { get; set; }

        public virtual Section ConsumerSection { get; set; }
        public virtual Doctors Doctor { get; set; }
        public virtual Drugs Drug { get; set; }
        public virtual PriceTypeMethod PriceMethodType { get; set; }
        public virtual Receptions Reception { get; set; }
        public virtual Request Request { get; set; }
        public virtual Section Section { get; set; }
    }
}
