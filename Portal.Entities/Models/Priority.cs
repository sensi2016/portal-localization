using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Priority
    {
        public Priority()
        {
            PrescriptionDetailDrug = new HashSet<PrescriptionDetailDrug>();
            PrescriptionDetailService = new HashSet<PrescriptionDetailService>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public int? Arrange { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsActive { get; set; }
        public string NoteLang2 { get; set; }

        public virtual ICollection<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
        public virtual ICollection<PrescriptionDetailService> PrescriptionDetailService { get; set; }
    }
}
