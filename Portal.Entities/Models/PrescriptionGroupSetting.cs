using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionGroupSetting
    {
        public int Id { get; set; }
        public int? GroupServiceId { get; set; }
        public int? PrescriptionGroupItemId { get; set; }
        public int? VisitTypeId { get; set; }
        public int? SectionId { get; set; }
        public bool? IsCanView { get; set; }
        public bool? IsCanInsert { get; set; }
        public int? Arrange { get; set; }

        public virtual Services GroupService { get; set; }
        public virtual PrescriptionGroupItem PrescriptionGroupItem { get; set; }
        public virtual VisitType VisitType { get; set; }
    }
}
