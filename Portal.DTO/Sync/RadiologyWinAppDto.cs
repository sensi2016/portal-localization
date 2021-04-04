using System;

namespace Portal.DTO
{
    public class RadiologyWinAppDto : BaseWinAppDto
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime? Date { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string Report { get; set; }
    }

}
