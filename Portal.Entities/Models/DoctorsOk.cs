using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DoctorsOk
    {
        public Guid Id { get; set; }
        public string Arname { get; set; }
        public string Enspecialty { get; set; }
        public string Arspecialty { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Arcertificates1 { get; set; }
        public string Arcertificates2 { get; set; }
        public string Arcertificates3 { get; set; }
        public string Encertificates1 { get; set; }
        public string Encertificates2 { get; set; }
        public string Encertificates3 { get; set; }
        public string Email { get; set; }
        public double Visitfee { get; set; }
        public string UserId { get; set; }
        public Guid? HospitalId { get; set; }
    }
}
