using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Province
    {
        public Province()
        {
            Center = new HashSet<Center>();
            City = new HashSet<City>();
            Person = new HashSet<Person>();
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
        public int? CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Center> Center { get; set; }
        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}
