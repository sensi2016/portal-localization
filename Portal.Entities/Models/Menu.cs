using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string MenuName { get; set; }
        public int? PermissionId { get; set; }
        public int? ParentId { get; set; }
        public string MenuType { get; set; }
        public bool? IsActive { get; set; }
        public double? ArrangeId { get; set; }
        public string IconName { get; set; }
        public string Link { get; set; }

        public virtual Permissions Permission { get; set; }
    }
}
