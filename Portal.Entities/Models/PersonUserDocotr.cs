using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PersonUserDocotr
    {
        public string DoctorId { get; set; }
        public int? PersonId { get; set; }
        public string UserId { get; set; }
    }
}
