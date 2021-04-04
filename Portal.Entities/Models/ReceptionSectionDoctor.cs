using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ReceptionSectionDoctor
    {
        public int Id { get; set; }
        public int? DoctorId { get; set; }
        public int? SectionId { get; set; }
        public long? ReceptionId { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsCurrent { get; set; }

        public virtual Doctors Doctor { get; set; }
        public virtual Receptions Reception { get; set; }
        public virtual Section Section { get; set; }
    }
}
