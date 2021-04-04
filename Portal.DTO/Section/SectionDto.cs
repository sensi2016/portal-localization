using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class RequestSetSectionDto
    {
        public object Menu { get; set; }
    }

    public class SetSectionDto
    {
        public int SectionId { get; set; }
        public int? RoleId { get; set; }
    }

    public class SetLanguageDto
    {
        public string Language { get; set; }
    }

    public class SectionDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string LocalCode { get; set; }
        public string No { get; set; }
        public string Phone { get; set; }
        public int? SuperVisorPersonelId { get; set; }
        public int? SectionTypeId { get; set; }
        public int? StockId { get; set; }
        public int? TrayId { get; set; }
        public bool? IsActive { get; set; }
        public int? BossId { get; set; }
        public int? SectionKindId { get; set; }
        public int? SectionFieldId { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public virtual List<SectionPropertyListDto> SectionProperties { get; set; }
        public virtual List<ListStoreDto> Stores { get; set; }
    }

    public class SectionFilterDto:BaseRequestPaging
    {
        public List<int> IdList { get; set; }
    }

    public class ResponseSectionStockDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LocalCode { get; set; }
        public string Type { get; set; }
        public string Note { get; set; }
   
    }

    public class ListSectionDto
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
        public string Section { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }

      //  public List<PageNameDto> PageNames {get;set;}
    }

    public class ListStoreDto
    {
        public int Id { get; set; }
        public string Titile { get; set; }
    }

    public class SectionPropertyListDto
    {
        public int Id { get; set; }
        public string Titile { get; set; }
    }
}
