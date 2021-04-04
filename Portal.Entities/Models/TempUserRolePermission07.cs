using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class TempUserRolePermission07
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public int? SectionId { get; set; }
        public int? PermissionId { get; set; }
    }
}
