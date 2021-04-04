using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Frequency
    {
        public Frequency()
        {
            PrescriptionDetailDrug = new HashSet<PrescriptionDetailDrug>();
            PrescriptionDetailService = new HashSet<PrescriptionDetailService>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Code { get; set; }
        public int? Period { get; set; }

        public virtual ICollection<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
        public virtual ICollection<PrescriptionDetailService> PrescriptionDetailService { get; set; }
    }
}
