using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DoctorService
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public int? DoctorId { get; set; }
        public int? DoctorPercent { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
    }
}
