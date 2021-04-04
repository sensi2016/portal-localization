using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class RichLongAnswer
    {
        public RichLongAnswer()
        {
            Services = new HashSet<Services>();
        }

        public int Id { get; set; }
        public string Answer { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int? ServiceId { get; set; }

        public virtual Services Service { get; set; }
        public virtual ICollection<Services> Services { get; set; }
    }
}
