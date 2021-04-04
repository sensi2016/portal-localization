using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Login
    {
        public long Id { get; set; }
        public int? UserId { get; set; }
        public string Ip { get; set; }
        public string Browser { get; set; }
        public Guid? Token { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string Language { get; set; }
        public int? CurrentSectionId { get; set; }
        public int? RoleId { get; set; }
    }
}
