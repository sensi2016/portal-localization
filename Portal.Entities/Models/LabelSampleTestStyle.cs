﻿using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class LabelSampleTestStyle
    {
        public LabelSampleTestStyle()
        {
            LabelBloodBag = new HashSet<LabelBloodBag>();
            LabelSampleTest = new HashSet<LabelSampleTest>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }
        public int? Arrange { get; set; }

        public virtual ICollection<LabelBloodBag> LabelBloodBag { get; set; }
        public virtual ICollection<LabelSampleTest> LabelSampleTest { get; set; }
    }
}
