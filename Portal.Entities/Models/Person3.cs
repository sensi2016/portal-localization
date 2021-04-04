using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Person3
    {
        public int? PersonId { get; set; }
        public string UserId { get; set; }
        public Guid? DoctorId { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
