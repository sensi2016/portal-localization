using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class ListSectionSettingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string LocalCode { get; set; }
        public string No { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int SuperVisorPersonelFullName { get; set; }
        public string SectionTypeTitle { get; set; }
        public bool IsActive { get; set; }
    }
}
