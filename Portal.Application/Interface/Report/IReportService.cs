using System;
using System.Threading.Tasks;
using His.Reception.DTO;
using Portal.DTO;

// ReSharper disable once CheckNamespace
namespace His.Reception.Application.Interface
{
    public interface IReportService
    {
        Task<BaseResponseDto> ReportHeader(string reportTitle);
        Task<BaseResponseDto> ReportFooter(string reportTitle);
        Task<ListResponseDto> ReportDate(ReportDto report);
        Task<BaseResponseDto> PatientInfo(long receptionId);
        Task<BaseResponseDto> PrescriptionShare(PrescriptionShareReportParametersDto prescriptionShareReportParametersDto);
    }
}
