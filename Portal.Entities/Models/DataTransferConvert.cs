using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DataTransferConvert
    {
        public long Id { get; set; }
        public string TableId { get; set; }
        public string TargetTableId { get; set; }
        public string TableName { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UserId { get; set; }
    }
}
