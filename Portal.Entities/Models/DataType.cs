using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DataType
    {
        public DataType()
        {
            Services = new HashSet<Services>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string SqlType { get; set; }
        public string CsharpType { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }

        public virtual ICollection<Services> Services { get; set; }
    }
}
