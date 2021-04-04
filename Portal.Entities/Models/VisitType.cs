using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class VisitType
    {
        public VisitType()
        {
            DoctorVisitType = new HashSet<DoctorVisitType>();
            Prescription = new HashSet<Prescription>();
            PrescriptionGroupSetting = new HashSet<PrescriptionGroupSetting>();
            PrescriptionSetting = new HashSet<PrescriptionSetting>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<DoctorVisitType> DoctorVisitType { get; set; }
        public virtual ICollection<Prescription> Prescription { get; set; }
        public virtual ICollection<PrescriptionGroupSetting> PrescriptionGroupSetting { get; set; }
        public virtual ICollection<PrescriptionSetting> PrescriptionSetting { get; set; }
    }
}
