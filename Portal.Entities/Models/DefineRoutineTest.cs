using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DefineRoutineTest
    {
        public int Id { get; set; }
        public int? DefineRoutineId { get; set; }
        public int? ServiceId { get; set; }

        public virtual DefineRoutine DefineRoutine { get; set; }
        public virtual Services Service { get; set; }
    }
}
