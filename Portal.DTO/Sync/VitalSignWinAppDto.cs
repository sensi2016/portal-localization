using System;

namespace Portal.DTO
{
    public class VitalSignWinAppDto : BaseWinAppDto
    {
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string BP { get; set; }
        public string PR { get; set; }
        public string BMI { get; set; }
        public string WT { get; set; }
        public string HT { get; set; }
        public string Notes { get; set; }
        public string Temp { get; set; }
        public string GFR { get; set; }
        public string Mean { get; set; }
        public string SPO { get; set; }
        public string Creatinine { get; set; }
        public string SkinRace { get; set; }
        public DateTime Date { get; set; }
    }

}
