using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.City
{
    public class ZoneDto:IPaging
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public int? ParentId { get; set; }
        public string ParentTitle { get; set; }
        public int? LevelId { get; set; }
        public int? CityId { get; set; }
        public string CityTitle { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
