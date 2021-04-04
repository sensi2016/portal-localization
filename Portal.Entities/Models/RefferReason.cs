using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class RefferReason
    {
        public RefferReason()
        {
            InverseParent = new HashSet<RefferReason>();
        }

        public int Id { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public int? ParentId { get; set; }
        public int? LevelId { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsActive { get; set; }
        public string Note { get; set; }
        public string NoteLang2 { get; set; }

        public virtual RefferReason Parent { get; set; }
        public virtual ICollection<RefferReason> InverseParent { get; set; }
    }
}
