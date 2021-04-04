using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DefineRoutine
    {
        public DefineRoutine()
        {
            DefineRoutineTest = new HashSet<DefineRoutineTest>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<DefineRoutineTest> DefineRoutineTest { get; set; }
    }
}
