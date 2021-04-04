using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Setting1
    {
        public int Id { get; set; }
        public string KeyName { get; set; }
        public string KeyValue { get; set; }
        public bool? IsActive { get; set; }
        public int? CategorySettingId { get; set; }
    }
}
