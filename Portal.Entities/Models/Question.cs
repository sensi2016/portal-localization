using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Question
    {
        public Question()
        {
            Answer = new HashSet<Answer>();
            ReceptionAnswer = new HashSet<ReceptionAnswer>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
        public int? Arrange { get; set; }

        public virtual ICollection<Answer> Answer { get; set; }
        public virtual ICollection<ReceptionAnswer> ReceptionAnswer { get; set; }
    }
}
