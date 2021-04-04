using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Drugs
    {
        public Drugs()
        {
            DrugStoregCondition = new HashSet<DrugStoregCondition>();
            InteractionFirstDrug = new HashSet<Interaction>();
            InteractionSecondDrug = new HashSet<Interaction>();
            PrescriptionDetailDrug = new HashSet<PrescriptionDetailDrug>();
            PrescriptionDrugChart = new HashSet<PrescriptionDrugChart>();
            ProhibitedUsage = new HashSet<ProhibitedUsage>();
            PurchaseInvoiceDetails = new HashSet<PurchaseInvoiceDetails>();
            ReceptionDrug = new HashSet<ReceptionDrug>();
            RequestDetail = new HashSet<RequestDetail>();
            SectionEssentialsDrug = new HashSet<SectionEssentialsDrug>();
            SideEffects = new HashSet<SideEffects>();
            StoreDrug = new HashSet<StoreDrug>();
            StoreEntryDetails = new HashSet<StoreEntryDetails>();
            StoreTransferDetails = new HashSet<StoreTransferDetails>();
        }

        public int Id { get; set; }
        public int? GenericId { get; set; }
        public string BrandTitle { get; set; }
        public string LocalCode { get; set; }
        public decimal? Price { get; set; }
        public int? DrugCode { get; set; }
        public int? HisCode { get; set; }
        public int? CompanyId { get; set; }
        public int? StoregConditonId { get; set; }
        public int? DrugFormId { get; set; }
        public int? UnitId { get; set; }
        public int? BrokenConsumptionId { get; set; }
        public int? BrokenConsumptionBase { get; set; }
        public string ContentsOf { get; set; }
        public int? StopConsumptionDay { get; set; }
        public bool? IsHighRisk { get; set; }
        public bool? IsNarcotic { get; set; }
        public bool? IsSaleWithoutPrescription { get; set; }
        public bool? IsOtcRight { get; set; }
        public bool? IsTechnicalRight { get; set; }
        public string Note { get; set; }

        public virtual Unit1 BrokenConsumption { get; set; }
        public virtual Company Company { get; set; }
        public virtual DrugForm DrugForm { get; set; }
        public virtual GenericDrug Generic { get; set; }
        public virtual StoregCondition StoregConditon { get; set; }
        public virtual Unit1 Unit { get; set; }
        public virtual ICollection<DrugStoregCondition> DrugStoregCondition { get; set; }
        public virtual ICollection<Interaction> InteractionFirstDrug { get; set; }
        public virtual ICollection<Interaction> InteractionSecondDrug { get; set; }
        public virtual ICollection<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
        public virtual ICollection<PrescriptionDrugChart> PrescriptionDrugChart { get; set; }
        public virtual ICollection<ProhibitedUsage> ProhibitedUsage { get; set; }
        public virtual ICollection<PurchaseInvoiceDetails> PurchaseInvoiceDetails { get; set; }
        public virtual ICollection<ReceptionDrug> ReceptionDrug { get; set; }
        public virtual ICollection<RequestDetail> RequestDetail { get; set; }
        public virtual ICollection<SectionEssentialsDrug> SectionEssentialsDrug { get; set; }
        public virtual ICollection<SideEffects> SideEffects { get; set; }
        public virtual ICollection<StoreDrug> StoreDrug { get; set; }
        public virtual ICollection<StoreEntryDetails> StoreEntryDetails { get; set; }
        public virtual ICollection<StoreTransferDetails> StoreTransferDetails { get; set; }
    }
}
