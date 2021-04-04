using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Group
    {
        public Group()
        {
            DrugInteractionsFirstGroup = new HashSet<DrugInteractions>();
            DrugInteractionsSecondGroup = new HashSet<DrugInteractions>();
            GenericDrug = new HashSet<GenericDrug>();
            InteractionFirstGroup = new HashSet<Interaction>();
            InteractionSecondGroup = new HashSet<Interaction>();
            ProhibitedUsage = new HashSet<ProhibitedUsage>();
            SideEffects = new HashSet<SideEffects>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public int? ParentId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<DrugInteractions> DrugInteractionsFirstGroup { get; set; }
        public virtual ICollection<DrugInteractions> DrugInteractionsSecondGroup { get; set; }
        public virtual ICollection<GenericDrug> GenericDrug { get; set; }
        public virtual ICollection<Interaction> InteractionFirstGroup { get; set; }
        public virtual ICollection<Interaction> InteractionSecondGroup { get; set; }
        public virtual ICollection<ProhibitedUsage> ProhibitedUsage { get; set; }
        public virtual ICollection<SideEffects> SideEffects { get; set; }
    }
}
