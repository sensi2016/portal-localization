using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Permissions
    {
        public Permissions()
        {
            Menu = new HashSet<Menu>();
            PermissionSectionField = new HashSet<PermissionSectionField>();
            RolePermission = new HashSet<RolePermission>();
            UserPermission = new HashSet<UserPermission>();
            UserRolePermission = new HashSet<UserRolePermission>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public string NoteLang2 { get; set; }
        public string PageAddress { get; set; }
        public string ModuleName { get; set; }

        public virtual ICollection<Menu> Menu { get; set; }
        public virtual ICollection<PermissionSectionField> PermissionSectionField { get; set; }
        public virtual ICollection<RolePermission> RolePermission { get; set; }
        public virtual ICollection<UserPermission> UserPermission { get; set; }
        public virtual ICollection<UserRolePermission> UserRolePermission { get; set; }
    }
}
