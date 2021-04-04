using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DoctorVisitType
    {
        public int Id { get; set; }
        public int? VisitTypeId { get; set; }
        public int? DoctorId { get; set; }

        public virtual Doctors Doctor { get; set; }
        public virtual VisitType VisitType { get; set; }
    }
}
