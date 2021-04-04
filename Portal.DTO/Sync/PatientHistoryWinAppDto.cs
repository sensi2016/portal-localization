using System;

namespace Portal.DTO
{
    public class PatientHistoryWinAppDto : BaseWinAppDto
    {
        public string PatientId { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string DoctorId { get; set; }
        public string CC { get; set; }
        public string PHI { get; set; }
        public string PMH { get; set; }
        public string PSH { get; set; }
        public string POM { get; set; }
        public string II { get; set; }
        public string RR { get; set; }
        public string PR { get; set; }
        public string CancerStage { get; set; }
        public string CancerType { get; set; }
    }

}
