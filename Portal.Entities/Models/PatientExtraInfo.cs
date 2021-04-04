using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PatientExtraInfo
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public string Note { get; set; }
        public int? IssuePlaceId { get; set; }
        public DateTime? IssueDate { get; set; }
        public int? NationalityId { get; set; }
        public int? ReligionId { get; set; }
        public bool? IsSmoking { get; set; }
        public bool? IsDrinking { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public int? BloodGroupId { get; set; }
        public int? AllergyId { get; set; }
        public int? ChronicIllnessId { get; set; }
        public int? JobId { get; set; }
        public int? EducationId { get; set; }
        public string WorkPhone { get; set; }
        public int? RhId { get; set; }
        public string WorkAddress { get; set; }
        public int? JobTypeId { get; set; }
        public bool? IsPregnant { get; set; }
        public int? PregnancySeasonId { get; set; }
        public string Word { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportIssueDate { get; set; }

        public virtual Allergy Allergy { get; set; }
        public virtual Illness ChronicIllness { get; set; }
        public virtual Education Education { get; set; }
        public virtual Job Job { get; set; }
        public virtual JobType JobType { get; set; }
        public virtual Nationality Nationality { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual PregnancySeason PregnancySeason { get; set; }
        public virtual Religion Religion { get; set; }
        public virtual Rh Rh { get; set; }
    }
}
