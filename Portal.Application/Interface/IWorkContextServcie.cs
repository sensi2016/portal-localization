using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Interface
{
    public interface IWorkContextService
    {
         string Token { get; }
         string UserName { get; }
         int? UserId { get; }
         int? RoleId { get; }
         int? SectionId { get; }
         string Language { get; }
    }
}
