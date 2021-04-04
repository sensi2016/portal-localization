using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Country
    {
        public Country()
        {
            Center = new HashSet<Center>();
            Company = new HashSet<Company>();
            Person = new HashSet<Person>();
            Province = new HashSet<Province>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public bool? IsAdmin { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Center> Center { get; set; }
        public virtual ICollection<Company> Company { get; set; }
        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<Province> Province { get; set; }
    }
}
