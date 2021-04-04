using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class CenterServices
    {
        public int Id { get; set; }
        public int? CenterId { get; set; }
        public int? ServiceId { get; set; }

        public virtual Center Center { get; set; }
        public virtual Services Service { get; set; }
    }
}
