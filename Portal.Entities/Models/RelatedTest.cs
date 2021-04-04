using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class RelatedTest
    {
        public int Id { get; set; }
        public int? MainServiceId { get; set; }
        public int? SubServiceId { get; set; }

        public virtual Services MainService { get; set; }
        public virtual Services SubService { get; set; }
    }
}
