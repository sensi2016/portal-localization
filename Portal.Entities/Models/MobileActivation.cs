using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class MobileActivation
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public string VerifyCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
