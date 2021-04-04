using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Payment
    {
        public long Id { get; set; }
        public decimal? Amount { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string PaymentNo { get; set; }
        public int? PaymentTypeId { get; set; }
        public long? ReceptionId { get; set; }
        public bool? IsApply { get; set; }
        public int? RoleId { get; set; }

        public virtual PaymentType PaymentType { get; set; }
        public virtual Receptions Reception { get; set; }
        public virtual Role Role { get; set; }
        public virtual Users User { get; set; }
    }
}
