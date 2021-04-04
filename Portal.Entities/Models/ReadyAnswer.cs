using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ReadyAnswer
    {
        public ReadyAnswer()
        {
            GroupReadyAnswer = new HashSet<GroupReadyAnswer>();
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
        public bool? IsCommon { get; set; }

        public virtual ICollection<GroupReadyAnswer> GroupReadyAnswer { get; set; }
    }
}
