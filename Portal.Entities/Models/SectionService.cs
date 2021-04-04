using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class SectionService
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public int? SectionId { get; set; }

        public virtual Section Section { get; set; }
        public virtual Services Service { get; set; }
    }
}
