using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ServiceCoding
    {
        public int Id { get; set; }
        public int? CodingId { get; set; }
        public int? CodingTypeId { get; set; }
        public string Code { get; set; }
        public int? ServiceId { get; set; }

        public virtual Services Service { get; set; }
    }
}
