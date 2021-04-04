using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Sign
    {
        public Sign()
        {
            PrescriptionSign = new HashSet<PrescriptionSign>();
            ReceptionSign = new HashSet<ReceptionSign>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string LocalCode { get; set; }
        public string Note { get; set; }
        public string NoteLange2 { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<PrescriptionSign> PrescriptionSign { get; set; }
        public virtual ICollection<ReceptionSign> ReceptionSign { get; set; }
    }
}
