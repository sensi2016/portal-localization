using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class StoreDrug
    {
        public long Id { get; set; }
        public int? DrugId { get; set; }
        public int? SectionId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? Quantity { get; set; }

        public virtual Drugs Drug { get; set; }
        public virtual Section Section { get; set; }
    }
}
