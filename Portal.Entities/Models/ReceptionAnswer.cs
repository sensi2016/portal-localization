using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ReceptionAnswer
    {
        public long Id { get; set; }
        public int? AnswerId { get; set; }
        public int? QuestionId { get; set; }
        public long? ReceptionId { get; set; }
        public DateTime? InfoDate { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual Question Question { get; set; }
        public virtual Receptions Reception { get; set; }
    }
}
