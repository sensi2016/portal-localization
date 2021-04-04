using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DurationType
    {
        public DurationType()
        {
            DoctorAppointment = new HashSet<DoctorAppointment>();
            PrescriptionDetailDrug = new HashSet<PrescriptionDetailDrug>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public int? DurationMinute { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsByQuantity { get; set; }
        public string Code { get; set; }

        public virtual ICollection<DoctorAppointment> DoctorAppointment { get; set; }
        public virtual ICollection<PrescriptionDetailDrug> PrescriptionDetailDrug { get; set; }
    }
}
