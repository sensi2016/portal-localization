using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Kit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string Brand { get; set; }
        public string SerialNumber { get; set; }
        public string LotNumber { get; set; }
        public int? UnitId { get; set; }
        public int? NumberOfTest { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public virtual Unit2 Unit { get; set; }
    }
}
