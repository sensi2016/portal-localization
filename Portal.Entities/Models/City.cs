using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class City
    {
        public City()
        {
            Center = new HashSet<Center>();
            Person = new HashSet<Person>();
            Zone = new HashSet<Zone>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }
        public int? ProvinceId { get; set; }

        public virtual Province Province { get; set; }
        public virtual ICollection<Center> Center { get; set; }
        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<Zone> Zone { get; set; }
    }
}
