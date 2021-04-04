using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionDrugRoutine
    {
        public long Id { get; set; }
        public int WayOfPrescriptionId { get; set; }
        public int? GenericDrugId { get; set; }
        public long? PrescriptionRoutineId { get; set; }
        public int? Number { get; set; }
        public int? Period { get; set; }
        public string Note { get; set; }

        public virtual GenericDrug GenericDrug { get; set; }
        public virtual PrescriptionRoutine PrescriptionRoutine { get; set; }
        public virtual WayOfPrescription WayOfPrescription { get; set; }
    }
}
