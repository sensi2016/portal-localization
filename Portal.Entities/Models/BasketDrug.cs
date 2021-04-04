using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class BasketDrug
    {
        public long Id { get; set; }
        public int? UserId { get; set; }
        public long? ReceptionId { get; set; }
        public long? PrescriptionDetailDrugId { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? DrugId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? Quantity { get; set; }
        public int? RoleId { get; set; }

        public virtual PrescriptionDetailDrug PrescriptionDetailDrug { get; set; }
        public virtual Receptions Reception { get; set; }
        public virtual Role Role { get; set; }
        public virtual Users User { get; set; }
    }
}
