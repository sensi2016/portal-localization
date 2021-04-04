using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Patient
    {
        public Patient()
        {
            AnswerService = new HashSet<AnswerService>();
            FileTag = new HashSet<FileTag>();
            PatientExtraInfo = new HashSet<PatientExtraInfo>();
            Receptions = new HashSet<Receptions>();
        }

        public int Id { get; set; }
        public int? PersonId { get; set; }
        public string Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? Hisno { get; set; }
        public string FileNo { get; set; }
        public int? InternalId { get; set; }
        public int? BloodGroupId { get; set; }

        public virtual BloodGroup BloodGroup { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<AnswerService> AnswerService { get; set; }
        public virtual ICollection<FileTag> FileTag { get; set; }
        public virtual ICollection<PatientExtraInfo> PatientExtraInfo { get; set; }
        public virtual ICollection<Receptions> Receptions { get; set; }
    }
}
