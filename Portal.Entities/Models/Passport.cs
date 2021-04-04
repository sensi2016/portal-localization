using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Passport
    {
        public long Id { get; set; }
        public int? PersonId { get; set; }
        public string Code { get; set; }
        public string FlightNumber { get; set; }
        public DateTime? FlightDate { get; set; }
        public string Note { get; set; }

        public virtual Person Person { get; set; }
    }
}
