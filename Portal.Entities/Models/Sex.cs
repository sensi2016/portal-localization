using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Sex
    {
        public Sex()
        {
            Instruction = new HashSet<Instruction>();
            Person = new HashSet<Person>();
            TestRange = new HashSet<TestRange>();
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

        public virtual ICollection<Instruction> Instruction { get; set; }
        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<TestRange> TestRange { get; set; }
    }
}
