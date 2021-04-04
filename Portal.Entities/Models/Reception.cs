using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Reception
    {
        public long ReceptionId { get; set; }
        public long? ReceptionCode { get; set; }
        public int? PatientId { get; set; }
        public int? PersonId { get; set; }
    }
}
