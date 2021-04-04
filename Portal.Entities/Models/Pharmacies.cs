using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Pharmacies
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
