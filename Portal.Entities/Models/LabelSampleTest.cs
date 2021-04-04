using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class LabelSampleTest
    {
        public int Id { get; set; }
        public int? TestLabelingId { get; set; }
        public int? LabelSampleTestStyleId { get; set; }
        public bool? IsBarcode { get; set; }
        public int? Arrange { get; set; }

        public virtual LabelSampleTestStyle LabelSampleTestStyle { get; set; }
        public virtual TestLabeling TestLabeling { get; set; }
    }
}
