using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class SamplerType
    {
        public SamplerType()
        {
            Sampling = new HashSet<Sampling>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string LocalCode { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string NoteLang2 { get; set; }
        public int? IcdCodeId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Sampling> Sampling { get; set; }
    }
}
