using Portal.DTO;
using Portal.DTO.Covid;
using Portal.DTO.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface IReceptionsService
    {
        Task<BaseResponseDto> AddCovid(ReceptionCovidDto receptionCovidDto);
        Task<BaseResponseDto> GetLabById(long id);
        Task<BaseResponseDto> AddLab(ReceptionLabDto receptionCovidDto);
        Task<BaseResponseDto> EditLab(ReceptionLabDto receptionCovidDto);
        Task<ListResponseDto> SearchLab(FilterReceptionLabDto receptionLabDto);
        Task<BaseResponseDto> Get(long id);
        Task<ListResponseDto> Search(FilterReceptionCovidDto receptionCovidFilterDto);
        Task<BaseResponseDto> SetAnswer(SetAnswerCovidDto setAnswerCovidDto);
        Task<BaseResponseDto> SetLabAnswer(AnswerLabDto answerLabDto);
        Task<BaseResponseDto> LabCovidReport(long receptionId);
        Task<BaseResponseDto> BaseInfoAnswer(long receptionId);
        Task<BaseResponseDto> GetResultByUserId(long userId);
        Task<BaseResponseDto> MaxInternalId();
        Task<BaseResponseDto> MaxReceptionId();

        #region airport covid

        Task<BaseResponseDto> AddCovidAirPort(ReceptionCovidAirPortDto receptionCovidAirPortDto);
        Task<BaseResponseDto> EditCovidAirPort(ReceptionCovidAirPortDto receptionCovidAirPortDto,bool IsSendSms);
        Task<BaseResponseDto> ResendSendSms(long receptionId);
        Task<BaseResponseDto> ReceiptPrint(long receptionId);
        Task<BaseResponseDto> AnswerPrint(long receptionId);
        Task<BaseResponseDto> UploadFile(BaseUploadFileDto<long> baseUploadFile);
        Task<BaseResponseDto> VerifyCode(VerifyReceptionIdDto requestVerifyDto);
        Task<ListResponseDto> SearchAirPort(FilterCovidAirPortDto filterCovidAirPortDto);
        Task<BaseResponseDto> Skip(ReceptionIdDto receptionIdDto);
        Task<BaseResponseDto> AnswerCovidSms(ReceptionIdDto receptionIdDto);
        Task<BaseResponseDto> CheckByMobile(RequestMobileVerifyDto requestMobileVerifyDto);

        #endregion
    }
}
