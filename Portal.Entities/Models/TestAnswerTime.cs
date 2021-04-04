using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class TestAnswerTime
    {
        public int Id { get; set; }
        public int? ServiceId { get; set; }
        public bool? IsEveryDay { get; set; }
        public int? DayOfWeek { get; set; }
        public int? DayOfMonth { get; set; }

        public virtual Services Service { get; set; }
    }
}
