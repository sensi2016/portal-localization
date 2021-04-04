using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class CenterWorkItem
    {
        public int Id { get; set; }
        public int? CenterId { get; set; }
        public int? WorkItemId { get; set; }

        public virtual Center Center { get; set; }
        public virtual WorkTimeType WorkItem { get; set; }
    }
}
