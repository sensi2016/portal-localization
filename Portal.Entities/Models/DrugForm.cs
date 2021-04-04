using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DrugForm
    {
        public DrugForm()
        {
            Drugs = new HashSet<Drugs>();
            PackagingType = new HashSet<PackagingType>();
            PrescriptionDetailDrug = new HashSet<PrescriptionDetailDrug>();
            RequestDetail = new HashSet<RequestDetail>();
            SectionEssentialsGenericDrug = new HashSet<SectionEssentialsGenericDrug>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public bool? IsAdmin { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Drugs> Drugs { get; set; }
        public virtual ICollection<PackagingType> PackagingType { get; set; }
        public virtual ICollection<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
        public virtual ICollection<RequestDetail> RequestDetail { get; set; }
        public virtual ICollection<SectionEssentialsGenericDrug> SectionEssentialsGenericDrug { get; set; }
    }
}
