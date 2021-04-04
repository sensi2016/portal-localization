using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class UserRolePermission
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public int? SectionId { get; set; }
        public int? PermissionId { get; set; }

        public virtual Permissions Permission { get; set; }
        public virtual Role Role { get; set; }
        public virtual Section Section { get; set; }
        public virtual Users User { get; set; }
    }
}
