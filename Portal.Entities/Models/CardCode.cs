using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class CardCode
    {
        public CardCode()
        {
            UserCardCode = new HashSet<UserCardCode>();
            Users = new HashSet<Users>();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HealthNumber { get; set; }
        public string Token { get; set; }
        public string Serial { get; set; }
        public bool IsUsed { get; set; }

        public virtual ICollection<UserCardCode> UserCardCode { get; set; }
        public virtual ICollection<Users> Users { get; set; }
    }
}
