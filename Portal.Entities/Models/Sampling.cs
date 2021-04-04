using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Sampling
    {
        public Sampling()
        {
            SamplingHistory = new HashSet<SamplingHistory>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public long? LabReceptionId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? SamplerTypeId { get; set; }
        public int? SamplingPersonId { get; set; }
        public int? SampleTypeId { get; set; }
        public string OtherSampler { get; set; }
        public decimal? SampleSizeTaken { get; set; }
        public bool? IsPatientIdentificationByQuestion { get; set; }
        public bool? IsPatientIdentificationByWrist { get; set; }
        public int? PrintCount { get; set; }

        public virtual ReceptionDetail LabReception { get; set; }
        public virtual SampleType SampleType { get; set; }
        public virtual SamplerType SamplerType { get; set; }
        public virtual Users SamplingPerson { get; set; }
        public virtual ICollection<SamplingHistory> SamplingHistory { get; set; }
    }
}
