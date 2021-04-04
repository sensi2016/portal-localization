using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DrugStoregCondition
    {
        public int Id { get; set; }
        public int? StoregConditonId { get; set; }
        public int? DrugId { get; set; }

        public virtual Drugs Drug { get; set; }
        public virtual StoregCondition StoregConditon { get; set; }
    }
}
