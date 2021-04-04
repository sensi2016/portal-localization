using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class CompanyAndType
    {
        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public int? CompanyTypeId { get; set; }

        public virtual Company Company { get; set; }
        public virtual CompanyType CompanyType { get; set; }
    }
}
