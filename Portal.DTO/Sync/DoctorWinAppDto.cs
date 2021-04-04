using System;

namespace Portal.DTO
{
    public class DoctorWinAppDto : BaseWinAppDto
    {
        public string Enname { get; set; }
        public string ArName { get; set; }
        public string Enspecialty { get; set; }
        public string Arspecialty { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Arcertificates_1 { get; set; }
        public string Arcertificates_2 { get; set; }
        public string Arcertificates_3 { get; set; }
        public string Encertificates_1 { get; set; }
        public string Encertificates_2 { get; set; }
        public string Encertificates_3 { get; set; }
        public string Mac_address { get; set; }
        public string Email { get; set; }
        public double Visitfee { get; set; }
        public Guid? HospitalId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Mac { get; set; }

    }

}
