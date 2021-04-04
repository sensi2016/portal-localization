using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Doctor
{
    public class ListDoctorDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Scientificlevel { get; set; }
        public string MedicalSystemNo { get; set; }
        public string NationalCode { get; set; }
        public int? Age { get; set; }
        public string Sex { get; set; }
        public string CityAndZone { get; set; }
        public string Center { get; set; }
        public string PhoneClinic { get; set; }
        public string AddressClinic { get; set; }
        public string Expertise { get; set; }
        public string Scientificleve { get; set; }
        public string CooperationDate { get; set; }
        public decimal? CostVisit { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsHospitalization { get; set; }
        public bool? IsEmergency { get; set; }
        public bool? IsOutpatient { get; set; }
        public bool? IsSurgeryRoom { get; set; }
        public int CountPatientSynced { get; set; }
    }
}
