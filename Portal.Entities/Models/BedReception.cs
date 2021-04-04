using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class BedReception
    {
        public int Id { get; set; }
        public long? ReceptionId { get; set; }
        public int? BedId { get; set; }
        public int? BedReserveStatusId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public DateTime? Createdate { get; set; }
        public int? FromBedId { get; set; }
        public bool? IsCurrent { get; set; }
        public int? FromId { get; set; }

        public virtual Bed Bed { get; set; }
        public virtual BedReserveStatus BedReserveStatus { get; set; }
        public virtual Receptions Reception { get; set; }
    }
}
