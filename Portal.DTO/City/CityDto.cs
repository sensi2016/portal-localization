using System;
using System.Collections.Generic;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Portal.DTO
{
    public class CityDto:IPaging
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }
        public int? ProvinceId { get; set; }
        public string ProvinceTitle { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class ProvinceDto : IPaging
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string NoteLang2 { get; set; }
        public bool? IsActive { get; set; }
        public int? ConteryId { get; set; }
        public string ConteryTitle { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
