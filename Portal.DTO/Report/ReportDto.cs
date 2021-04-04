using System;
using Newtonsoft.Json;
using Portal.DTO;
using Portal.DTO.Message;

// ReSharper disable once CheckNamespace
namespace Portal.DTO
{
    public class ReportDto
    {
        public string ReportName { get; set; }
        [JsonProperty]
        public object Parameters { get; set; }
    }

    public class BaseReportParametersDto : IReportParameters
    {
        public long ReceptionId { get; set; }
        public bool IsPrint { get; set; }
    }

    public class ReportParametersDto : IReportParameters, IDateRange,IPaging
    {
        public long ReceptionId { get; set; }
        public bool IsPrint { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class ReportTourAndFollowUpSheetParametersDto : IReportParameters
    {
        public long ReceptionId { get; set; }
        public long PrescriptionId { get; set; }
        public bool IsPrint { get; set; }

        public DateTime FromDate { get; set; }

    }

    public class DailyVitalObservationReportParametersDto: IReportParameters, IPaging
    {
        public long ReceptionId { get; set; }
        public bool IsPrint { get; set; }
        public DateTime ReportDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
    

    public class ReportHeaderDto
    {
        public string HospitalName { get; set; }
        public string Title { get; set; }
        public string DepartmentTitle { get; set; }
        public string Description { get; set; }
    }

    public class PrescriptionShareReportParametersDto 
    {
        public string Key { get; set; }
        public bool IsPrint { get; set; }
     
    }
}
