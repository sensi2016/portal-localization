using System;

namespace Portal.DTO
{
    public class PrescriptionWinAppDto : BaseWinAppDto
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public string Pharmacy { get; set; }
        public string Barcode { get; set; }
    }

}
