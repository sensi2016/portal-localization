using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionDetailPharmacistNote
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? PrescriptionId { get; set; }
        public long? PrescriptionDetailDrugId { get; set; }
        public string NoteForNurse { get; set; }
        public string NoteForTrainingPatient { get; set; }
        public string Note { get; set; }

        public virtual Prescription Prescription { get; set; }
        public virtual PrescriptionDetailDrug PrescriptionDetailDrug { get; set; }
    }
}
