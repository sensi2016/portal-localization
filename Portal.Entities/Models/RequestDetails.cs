using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class RequestDetails
    {
        public long Id { get; set; }
        public long? RequestId { get; set; }
        public long? LabReceptionId { get; set; }
        public int? ServiceId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Duration { get; set; }
        public bool? IsNoSample { get; set; }
        public long? PrescriptionDetailServiceId { get; set; }
        public string Note { get; set; }
    }
}
