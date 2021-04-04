using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            RequestDetail = new HashSet<RequestDetail>();
            RequestHistory = new HashSet<RequestHistory>();
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

        public virtual ICollection<RequestDetail> RequestDetail { get; set; }
        public virtual ICollection<RequestHistory> RequestHistory { get; set; }
    }
}
