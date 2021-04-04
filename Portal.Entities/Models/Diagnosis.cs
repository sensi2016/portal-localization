using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Diagnosis
    {
        public Diagnosis()
        {
            ReceptionDiagnosis = new HashSet<ReceptionDiagnosis>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public int? IcdCodeId { get; set; }
        public string LocalCode { get; set; }
        public string Note { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<ReceptionDiagnosis> ReceptionDiagnosis { get; set; }
    }
}
