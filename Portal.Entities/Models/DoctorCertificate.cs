using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DoctorCertificate
    {
        public int Id { get; set; }
        public int? DoctorId { get; set; }
        public int? CertificateId { get; set; }

        public virtual Certificate Certificate { get; set; }
        public virtual Doctors Doctor { get; set; }
    }
}
