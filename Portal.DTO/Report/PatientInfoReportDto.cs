using Portal.DTO.Message;
using System;

// ReSharper disable once CheckNamespace
namespace Portal.DTO
{ 
    public class PatientInfoReportDto : IReportType
    {
        public string Avatar { get; set; }
        public string FullName { get; set; }
        public string NationalCode { get; set; }
        public string Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? Age { get; set; }
        public string Mobile { get; set; }

        public int PatientNumber { get; set; }
        public long ReceptionCode { get; set; }
        public string DepartmentTitle { get; set; }
        public string RoomNumber { get; set; }
        public string BedNumber { get; set; }
        public string DoctorFullName { get; set; }
        public DateTime? AdmissionDateTime { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public ReportTypeEnum ReportTypeModel { get; set; }
    }
}
