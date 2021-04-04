using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Message
{
    public interface IReportType
    {
        ReportTypeEnum ReportTypeModel { get; set; }
    }

    public interface ITranslateReport
    {
        object Translate { get; set; }
    }

    public interface IPatientInfoReport
    {
         PatientInfoReportDto PatientInfo { get; set; }
    }

    public interface IReportHeaderDto
    {
        ReportHeaderDto ReportHeader { get; set; }
    }
}
