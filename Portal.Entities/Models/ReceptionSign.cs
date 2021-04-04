using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ReceptionSign
    {
        public long Id { get; set; }
        public int? SignId { get; set; }
        public long? ReceptionId { get; set; }

        public virtual Receptions Reception { get; set; }
        public virtual Sign Sign { get; set; }
    }
}
