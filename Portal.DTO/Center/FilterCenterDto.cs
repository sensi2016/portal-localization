using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class FilterCenterDto:IPaging
    {
        public string Logo { get; set; }
        public string CenterName { get; set; }
        public string Phone { get; set; }
        public int? CityId { get; set; }
        public int? CenterTypeId { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? ZoneId { get; set; }
        public int? WorkTimeTypeId { get; set; }
        public bool? IsActive { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class FilterHomeCenterDto : IPaging
    {
        public string Logo { get; set; }
        public string CenterName { get; set; }
        public int? CenterId { get; set; }
        public int? CenterTypeId { get; set; }
        public List<int> OwnershipTypeId { get; set; }
        public List<int> SellingTypeId { get; set; }
        public List<int> CenterServiceId { get; set; }
        public string Phone { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? ZoneId { get; set; }
        public bool IsHomeDelivery { get; set; }
        public int? ExamplaceId { get; set; }
        public List<int> WorkTimeTypeId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class FilterHomeCenterAppDto : IPaging
    {
        public string CenterName { get; set; }
        public int? CenterId { get; set; }
        public int? CenterTypeId { get; set; }
        public int? SexId { get; set; }
        public int? ExpertiseId { get; set; }
        public int? StatusId { get; set; }
        public int? ScientificlevelId { get; set; }
        //public List<int> OwnershipTypeId { get; set; }
        public bool? IsVisitType { get; set; }
        //    public int? CenterTypeId { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }

        public List<int> ZoneId { get; set; }
        public bool? IsOnlineSelling { get; set; }
        public bool? IsGovernmental { get; set; }
        public bool? IsHome { get; set; }

        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
