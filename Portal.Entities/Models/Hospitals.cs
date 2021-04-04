using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Hospitals
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string DescriptionEn { get; set; }
        public string AddressAr { get; set; }
        public string HospitalNameAr { get; set; }
    }
}
