using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Center
    {
        public Center()
        {
            CenterExamPlace = new HashSet<CenterExamPlace>();
            CenterSellingType = new HashSet<CenterSellingType>();
            CenterServices = new HashSet<CenterServices>();
            CenterWorkItem = new HashSet<CenterWorkItem>();
            Doctors = new HashSet<Doctors>();
            Section = new HashSet<Section>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public int? EconomicalId { get; set; }
        public string Boss { get; set; }
        public string BossPhone { get; set; }
        public string VisitorOrManaging { get; set; }
        public string VisitorPhone { get; set; }
        public int? CenterTypeId { get; set; }
        public int? WorkTimeTypeId { get; set; }
        public int? OwnershipTypeId { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? ZoneId { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string PostalCode { get; set; }
        public string AreaCode { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string WorkingHours { get; set; }
        public string PhoneResponseHourse { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsFreeDelivery { get; set; }
        public string ExamTime { get; set; }
        public string AnswerTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Note { get; set; }
        public bool? IsHomeDelivery { get; set; }

        public virtual CenterType CenterType { get; set; }
        public virtual City City { get; set; }
        public virtual Country Country { get; set; }
        public virtual OwnershipType OwnershipType { get; set; }
        public virtual Province Province { get; set; }
        public virtual WorkTimeType WorkTimeType { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual ICollection<CenterExamPlace> CenterExamPlace { get; set; }
        public virtual ICollection<CenterSellingType> CenterSellingType { get; set; }
        public virtual ICollection<CenterServices> CenterServices { get; set; }
        public virtual ICollection<CenterWorkItem> CenterWorkItem { get; set; }
        public virtual ICollection<Doctors> Doctors { get; set; }
        public virtual ICollection<Section> Section { get; set; }
    }
}
