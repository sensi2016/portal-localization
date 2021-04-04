using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class SmsReception
    {
        public long Id { get; set; }
        public long? ReceptionId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? SendDate { get; set; }
        public string Mobile { get; set; }
        public int? SendsmsStatusId { get; set; }
        public string SmsContent { get; set; }
        public string FileName { get; set; }

        public virtual Receptions Reception { get; set; }
        public virtual SendSmsStatus SendsmsStatus { get; set; }
    }
}
