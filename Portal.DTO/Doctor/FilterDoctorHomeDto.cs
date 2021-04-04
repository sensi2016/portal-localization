using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Doctor
{
    public class FilterDoctorHomeDto:IPaging
    {
        public string FullName { get; set; }
        public List<int> SexId { get; set; }
        public List<int> ExpertiseId { get; set; }
        public List<int> ScientificlevelId { get; set; }
        public List<int> OwnershipTypeId { get; set; }
        public List<int> WorkTimeTypeId { get; set; }
        public int? ExamplaceId { get; set; }
        public List<int> VisitTypeId { get; set; }
    //    public int? CenterTypeId { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? ZoneId { get; set; }
        public string PhoneClinic { get; set; }
        public bool? IsActive { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class FilterDoctorAppHomeDto : IPaging
    {
        public string FullName { get; set; }
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
