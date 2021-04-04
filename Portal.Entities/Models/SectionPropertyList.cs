using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class SectionPropertyList
    {
        public int Id { get; set; }
        public int? SectionId { get; set; }
        public int? SectionPropertyId { get; set; }
        public bool? IsActive { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string NoteLang2 { get; set; }

        public virtual Section Section { get; set; }
        public virtual SectionProperty SectionProperty { get; set; }
    }
}
