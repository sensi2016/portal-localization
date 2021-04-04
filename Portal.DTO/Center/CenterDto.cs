using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class CenterDto
    {
        public int Id { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Title { get; set; }
        public string Code { get; set; }
        public int? EconomicalId { get; set; }
        public string Boss { get; set; }
        public string BossPhone { get; set; }
        public string VisitorOrManaging { get; set; }
        public string VisitorPhone { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public int? CenterTypeId { get; set; }
     
        public int? OwnershipTypeId { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? ZoneId { get; set; }
        public bool? IsFreeDelivery { get; set; }
        public string Phone { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Mobile { get; set; }
        public string PostalCode { get; set; }
        public string AreaCode { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string WorkingHourse { get; set; }
        public string PhoneResponseHourse { get; set; }
        public bool? IsActive { get; set; }
        public string ExamTime { get; set; }
        public string AnswerTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Note { get; set; }
        public bool IsHomeDelivery { get; set; }
        public List<ListMultiResponse<int>> WorkTimeTypeId { get; set; }
        public List<ListMultiResponse<int>> ExamPlaces { get;set;}
        public List<ListMultiResponse<int>> SellingTypes { get;set;}
        public List<ListMultiResponse<int>> CenterServices { get;set;}

    }
    public class ResponseCenterDto
    {
        public int Id { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Title { get; set; }
        public bool? IsFreeDelivery { get; set; }
        public string Code { get; set; }
        public int? EconomicalId { get; set; }
        public string Boss { get; set; }
        public string BossPhone { get; set; }
        public string VisitorOrManaging { get; set; }
        public string VisitorPhone { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public int? CenterTypeId { get; set; }
        public List<ListMultiResponse<int>> WorkTimeTypeId { get; set; }
        public int? OwnershipTypeId { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? ZoneId { get; set; }
        public string Phone { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Mobile { get; set; }
        public string PostalCode { get; set; }
        public string AreaCode { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public bool? IsHomeDelivery { get; set; }
        public string Address { get; set; }
        public string WorkingHourse { get; set; }
        public string PhoneResponseHourse { get; set; }
        public bool? IsActive { get; set; }
        public string ExamTime { get; set; }
        public string AnswerTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Note { get; set; }
        public string FileId { get; set; }
        public string UserName { get; set; }
        public int? UserId { get; set; }
        public List<ListMultiResponse<int>> ExamPlaces { get; set; }
        public List<ListMultiResponse<int>> SellingTypes { get; set; }
        public List<ListMultiResponse<int>> CenterServices { get; set; }

    }

    public class CenterInfoDto
    {
        public int Id { get; set; }
        public int? CenterTypeId { get; set; }
        public string CenterTypeTitle { get; set; }
        public string Title { get; set; }
        public string LogoFId { get; set; }
        public string WorkingHours { get; set; }
        public string OwnerFullName { get; set; }
        public int? OwnershipTypeId { get; set; }
        public string OwnershipTypeTitle { get; set; }
        public string ProvinceTitle { get; set; }
        public string CityTitle { get; set; }
        public bool? IsFreeDelivery { get; set; }
        public bool? IsHomeDelivery { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<ServiceDto> Services { get; set; }
        public List<ExamPlaceDto> ExamPlaces { get; set; }
        public List<SellTypeDto> SellTypes { get; set; }
        public List<WorkItemTypeDto> WorkItemTypes { get; set; }
    }

    public class ExamPlaceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class SellTypeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class WorkItemTypeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
