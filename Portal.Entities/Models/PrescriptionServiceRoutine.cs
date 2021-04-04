using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionServiceRoutine
    {
        public long Id { get; set; }
        public int? ServiceId { get; set; }
        public int? Number { get; set; }
        public long? PrescriptionRoutineId { get; set; }
        public int? Period { get; set; }
        public string Note { get; set; }

        public virtual PrescriptionRoutine PrescriptionRoutine { get; set; }
        public virtual Services Service { get; set; }
    }
}
