using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Answer1
    {
        public long Id { get; set; }
        public long? ReceptionServiceId { get; set; }
        public string Result { get; set; }
        public string Comment { get; set; }
        public int? ReadyAnswerId { get; set; }

        public virtual ReadyAnswer1 ReadyAnswer { get; set; }
        public virtual ReceptionService ReceptionService { get; set; }
    }
}
