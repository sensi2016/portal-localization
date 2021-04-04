using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Illness
    {
        public Illness()
        {
            PatientExtraInfo = new HashSet<PatientExtraInfo>();
            Receptions = new HashSet<Receptions>();
            SpecialIllnessReception = new HashSet<SpecialIllnessReception>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public bool? IsSpecial { get; set; }
        public string LocalCode { get; set; }
        public int? IcdCodeId { get; set; }
        public string Note { get; set; }
        public string NoteLange2 { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<PatientExtraInfo> PatientExtraInfo { get; set; }
        public virtual ICollection<Receptions> Receptions { get; set; }
        public virtual ICollection<SpecialIllnessReception> SpecialIllnessReception { get; set; }
    }
}
