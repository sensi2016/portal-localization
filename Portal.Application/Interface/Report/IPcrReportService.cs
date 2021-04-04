using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Portal.DTO;

// ReSharper disable once CheckNamespace
namespace Portal.Application.Interface
{
    public interface IPcrReportService
    {
        Task<BaseResponseDto> PcrReport(PcrReportParameterDto parameters);
        Task<BaseResponseDto> PcrTestResultReport(PcrReportDateParameterDto parameters);
        Task<BaseResponseDto> PcrRefferFromReport(PcrReportDateParameterDto parameters);
    }
}
