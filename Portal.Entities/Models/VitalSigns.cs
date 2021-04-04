using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class VitalSigns
    {
        public long Id { get; set; }
        public int? BloodPressure { get; set; }
        public int? Systolic { get; set; }
        public int? Diastolic { get; set; }
        public int? Bpmean { get; set; }
        public int? Weight { get; set; }
        public int? Height { get; set; }
        public int? Bmi { get; set; }
        public int? PulseRate { get; set; }
        public int? Breathing { get; set; }
        public int? Temperature { get; set; }
        public int? Spo2 { get; set; }
        public int? BodyMassIndexes { get; set; }
        public int? SkinRace { get; set; }
        public int? Creatinine { get; set; }
        public int? Gfr { get; set; }
        public bool? PsychologicalStress { get; set; }
        public bool? PhysicalStress { get; set; }
        public bool? HighRiskStatus { get; set; }
        public bool? Confusion { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UserId { get; set; }
        public string Note { get; set; }
        public long? ReceptionId { get; set; }
        public int? RoleId { get; set; }

        public virtual Receptions Reception { get; set; }
        public virtual Role Role { get; set; }
        public virtual Users User { get; set; }
    }
}
