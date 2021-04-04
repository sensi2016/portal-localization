using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class GenericDrug
    {
        public GenericDrug()
        {
            DrugInteractionsFirstGenericDrug = new HashSet<DrugInteractions>();
            DrugInteractionsSecondGenericDrug = new HashSet<DrugInteractions>();
            Drugs = new HashSet<Drugs>();
            InteractionFirstGenericDrug = new HashSet<Interaction>();
            InteractionSecondGenericDrug = new HashSet<Interaction>();
            PrescriptionDetailDrug = new HashSet<PrescriptionDetailDrug>();
            PrescriptionDrugRoutine = new HashSet<PrescriptionDrugRoutine>();
            ProhibitedUsage = new HashSet<ProhibitedUsage>();
            RequestDetail = new HashSet<RequestDetail>();
            SectionEssentialsGenericDrug = new HashSet<SectionEssentialsGenericDrug>();
            SideEffects = new HashSet<SideEffects>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public int? GroupId { get; set; }
        public bool? IsAdmin { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<DrugInteractions> DrugInteractionsFirstGenericDrug { get; set; }
        public virtual ICollection<DrugInteractions> DrugInteractionsSecondGenericDrug { get; set; }
        public virtual ICollection<Drugs> Drugs { get; set; }
        public virtual ICollection<Interaction> InteractionFirstGenericDrug { get; set; }
        public virtual ICollection<Interaction> InteractionSecondGenericDrug { get; set; }
        public virtual ICollection<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
        public virtual ICollection<PrescriptionDrugRoutine> PrescriptionDrugRoutine { get; set; }
        public virtual ICollection<ProhibitedUsage> ProhibitedUsage { get; set; }
        public virtual ICollection<RequestDetail> RequestDetail { get; set; }
        public virtual ICollection<SectionEssentialsGenericDrug> SectionEssentialsGenericDrug { get; set; }
        public virtual ICollection<SideEffects> SideEffects { get; set; }
    }
}
