using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DoctorDegree
    {
        public DoctorDegree()
        {
            Doctors = new HashSet<Doctors>();
        }

        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int DegreeId { get; set; }
        public DateTime? DateOfIssue { get; set; }

        public virtual Degree Degree { get; set; }
        public virtual Doctors Doctor { get; set; }
        public virtual ICollection<Doctors> Doctors { get; set; }
    }
}
