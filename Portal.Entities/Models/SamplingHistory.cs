using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class SamplingHistory
    {
        public long Id { get; set; }
        public int? UserId { get; set; }
        public long? SamplingId { get; set; }
        public int? SamplingStatusId { get; set; }
        public DateTime? StatusDate { get; set; }
        public string Note { get; set; }
        public bool? IsCurrent { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Sampling Sampling { get; set; }
        public virtual SamplingStatus SamplingStatus { get; set; }
        public virtual Users User { get; set; }
    }
}
