using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class BloodStatusHistory
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BloodStatusId { get; set; }
        public int? BagBloodId { get; set; }
        public DateTime? StatusDate { get; set; }
        public string Note { get; set; }
        public bool? IsCurrent { get; set; }
        public int? RoleId { get; set; }

        public virtual BagBlood BagBlood { get; set; }
        public virtual BloodStatus BloodStatus { get; set; }
        public virtual Role Role { get; set; }
        public virtual Users User { get; set; }
    }
}
