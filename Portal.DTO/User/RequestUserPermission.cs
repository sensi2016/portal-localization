using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class RequestUserPermissionDto
    {
        public int UserId { get; set; } 
        public int? SectionId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class AddOrDeleteUserPermissionDto
    {
        public int UserId { get; set; }
        public int SectionId { get; set; }
        public List<int> PermissionIds { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class AddOrDeleteUserRoleDto
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class RequestUserRoleDto
    {
        public int UserId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
