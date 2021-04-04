using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Doctor
{
    public class FilterDoctorDto:IPaging
    {
        public string Name { get; set; }
        public string MedicalSystemNo { get; set; }
        public string NationalCode { get; set; }
        public int? ExpertiseId { get; set; }
        public int? ScientificlevelId { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? ZoneId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
