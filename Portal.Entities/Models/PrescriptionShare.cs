using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionShare
    {
        public long Id { get; set; }
        public long? PrescriptoinId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string FileName { get; set; }

        public virtual Prescription Prescriptoin { get; set; }
    }
}
