using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class PermissionDto
    {
        public string PermissionName { get; set; }
        public int? PermissionId { get; set; }
        public string SectionName { get; set; }
        public string PageAdress { get; set; }
        public int? SectionId { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }

    }

    public class ResponsePermissionDto
    {
        public string Title { get; set; }
        public int? Id { get; set; }
    }

    public class PageNameDto
    {
        public string PageName { get; set; }
        public IEnumerable<string> ModuleName { get; set; } 

    }

    

    public class LoginUserInfoDto
    {
        public List<ListSectionDto> Sections { get; set; } 
    }
}
