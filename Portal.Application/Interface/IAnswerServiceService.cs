using Portal.DTO;
using Portal.DTO.Answer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface IAnswerServiceService
    {
        Task<BaseResponseDto> Upload(UploadAnswerDto uploadAnswerDto);
        Task<BaseResponseDto> FastUpload(FastUploadAnswerDto fastUploadAnswerDto);
        Task<BaseResponseDto> ExcelUpload(ExcelAnswerDto excelAnswerDto);
        Task<BaseResponseDto> Download(string fileId);
        Task<BaseResponseDto> Sms(NotificationDto notification);
        Task<BaseResponseDto> Email(NotificationDto notification);
        Task<ListResponseDto> Search(FilterAnswerDto filterAnswer);
        Task<ListResponseDto> GetDetail(long userId);
        Task<ListResponseDto> SearchForPatient(FilterPatinetAnswerDto filterPatinetAnswerDto);
        Task<BaseResponseDto> GetCovidResult();
        Task<ListResponseDto> CurrentPaitent(FilterPatinetAnswerDto filterPatinetAnswerDto);
        Task<ListResponseDto> ListExcelUpload(FilterAnswerExcelDto baseRequestPagingPost);
        Task<BaseResponseDto> ExcelGetById(long id);
        Task<BaseResponseDto> EditExcel(ExcelAnswerDetailsDto excelAnswerDetailsDto);
        Task<BaseResponseDto> ReportExcelAnswer(string key);
    }
}
