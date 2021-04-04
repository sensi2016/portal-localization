using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionSetting
    {
        public long Id { get; set; }
        public int? VisitTypeId { get; set; }
        public int? ServiceId { get; set; }
        public int? ServiceGroupId { get; set; }
        public int? SectionId { get; set; }

        public virtual Section Section { get; set; }
        public virtual Services Service { get; set; }
        public virtual Services ServiceGroup { get; set; }
        public virtual VisitType VisitType { get; set; }
    }
}
