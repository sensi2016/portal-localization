using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class TemperatureType
    {
        public TemperatureType()
        {
            Section = new HashSet<Section>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Range { get; set; }
        public string Note { get; set; }

        public virtual ICollection<Section> Section { get; set; }
    }
}
