using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionDetails
    {
        public long? ReceptionId { get; set; }
        public long Prescription { get; set; }
        public long? DetailDrug { get; set; }
        public long? DetailService { get; set; }
    }
}
