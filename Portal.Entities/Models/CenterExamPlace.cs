using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class CenterExamPlace
    {
        public int Id { get; set; }
        public int? CenterId { get; set; }
        public int? ExamplaceId { get; set; }

        public virtual Center Center { get; set; }
        public virtual Examplace Examplace { get; set; }
    }
}
