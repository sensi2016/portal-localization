using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionDetailDrug
    {
        public PrescriptionDetailDrug()
        {
            BasketDrug = new HashSet<BasketDrug>();
            PrescriptionDetailDrugHistory = new HashSet<PrescriptionDetailDrugHistory>();
            PrescriptionDetailPharmacistNote = new HashSet<PrescriptionDetailPharmacistNote>();
            PrescriptionDrugChart = new HashSet<PrescriptionDrugChart>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public long? PrescriptionId { get; set; }
        public int? PrescriberNurseId { get; set; }
        public int? GenericDrugId { get; set; }
        public int? PrescriptionTypeId { get; set; }
        public int? PriorityId { get; set; }
        public int? DrugFormId { get; set; }
        public int? Quantity { get; set; }
        public int? RequestFromSectionId { get; set; }
        public int? PrescriptionInstructionId { get; set; }
        public int? WayOfPrescriptionId { get; set; }
        public DateTime? ActionDate { get; set; }
        public int? Period { get; set; }
        public long? PanelId { get; set; }
        public DateTime? StartDate { get; set; }
        public bool? IsJustOnTime { get; set; }
        public string Note { get; set; }
        public int? FrequencyId { get; set; }
        public int? DrugId { get; set; }
        public int? DurationNumber { get; set; }
        public int? DurationTypeId { get; set; }

        public virtual Drugs Drug { get; set; }
        public virtual DrugForm DrugForm { get; set; }
        public virtual DurationType DurationType { get; set; }
        public virtual Frequency Frequency { get; set; }
        public virtual GenericDrug GenericDrug { get; set; }
        public virtual PrescriptionPanel Panel { get; set; }
        public virtual Users PrescriberNurse { get; set; }
        public virtual Prescription Prescription { get; set; }
        public virtual PrescriptionInstruction PrescriptionInstruction { get; set; }
        public virtual PrescriptionType PrescriptionType { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Section RequestFromSection { get; set; }
        public virtual WayOfPrescription WayOfPrescription { get; set; }
        public virtual ICollection<BasketDrug> BasketDrug { get; set; }
        public virtual ICollection<PrescriptionDetailDrugHistory> PrescriptionDetailDrugHistory { get; set; }
        public virtual ICollection<PrescriptionDetailPharmacistNote> PrescriptionDetailPharmacistNote { get; set; }
        public virtual ICollection<PrescriptionDrugChart> PrescriptionDrugChart { get; set; }
    }
}
