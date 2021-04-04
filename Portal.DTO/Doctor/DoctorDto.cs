using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Doctor
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MedicalSystemNo { get; set; }
        public int? PersonId { get; set; }
        public int? SexId { get; set; }
        public short? Age { get; set; }
        public int? ExpertiseId { get; set; }
        public int? ScientificlevelId { get; set; }
        public string Mobile { get; set; }
        public string NationalCode { get; set; }
        public string Note { get; set; }
        public decimal? CostVisit { get; set; }
        public List<IdTitle<int>> Certificates { get; set; }
        public List<ListMultiRequest<int>> Visits { get; set; }
        public List<ListMultiResponse<int>> WorkTimeTypeId { get; set; }
        public List<ListMultiResponse<int>> ExamPlaces { get; set; }

        //clinic
        public string Phone { get; set; }     
        public int? CenterTypeId { get; set; }      
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? ZoneId { get; set; }
        public string PostalCode { get; set; }
        public string AreaCode { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string WorkingHourse { get; set; }
        public string PhoneResponseHourse { get; set; }
        public bool? IsActive { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
  
    }

    public class DoctorInfoDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string ArabicFullName { get; set; }
        public short? Age { get; set; }
        public string Mobile { get; set; }
        public string NationalCode { get; set; }   
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public string ExpertiseTitle { get; set; }
        public string ExpertiseTitleLang2 { get; set; }
        public int? ScientificlevelId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LogoFId { get; set; }
        public decimal? CostVisit { get; set; }

        public List<CertificateDto> Certificates { get; set; }
        public List<DoctorDegreeDto> Degrees { get; set; }
        public List<WorkItemTypeDto> WorkItemTypes { get; set; }
        public List<ExamPlaceDto> ExamPlaces { get; set; }
        public List<ListMultiResponse<int>> WorkTimes { get; set; }
    }


    public class ResponseDoctorDto : DoctorDto
    {
        public string UserName { get; set; }
        public int? UserId { get; set; }
        public string FileId { get; set; }
        public int? CenterId { get; set; }
    }
}
