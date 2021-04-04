using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class BackupT
    {
        public long BackupId { get; set; }
        public string FirstFileOnLocal { get; set; }
        public string SecondFileOnNetwork { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool? IsCompression { get; set; }
        public bool? Finished { get; set; }
        public string Notice { get; set; }
        public string Errors { get; set; }
    }
}
