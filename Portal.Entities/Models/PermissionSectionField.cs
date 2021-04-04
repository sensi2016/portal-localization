using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PermissionSectionField
    {
        public int Id { get; set; }
        public int? SectionFieldId { get; set; }
        public int? PermissionId { get; set; }

        public virtual Permissions Permission { get; set; }
        public virtual SectionField SectionField { get; set; }
    }
}
