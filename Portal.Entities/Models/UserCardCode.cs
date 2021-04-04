using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class UserCardCode
    {
        public long Id { get; set; }
        public int? UserId { get; set; }
        public long? CardCodeId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool? IsCurrent { get; set; }

        public virtual CardCode CardCode { get; set; }
        public virtual Users User { get; set; }
    }
}
