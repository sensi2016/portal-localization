using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ProhibitedUsage
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public int? GenericDrugId { get; set; }
        public int? DrugId { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }

        public virtual Drugs Drug { get; set; }
        public virtual GenericDrug GenericDrug { get; set; }
        public virtual Group Group { get; set; }
    }
}
