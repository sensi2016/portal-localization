using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface IPatientCardService
    {
        //admision list
        Task<ListResponseDto> ListPrescription(RequestPatientCardDto requestPatientCardDto, bool isCurrentUser = false);
        Task<ListResponseDto> ListCurrentRadilogy();
        Task<ListResponseDto> CurrentRadilogyById(long id);
        Task<BaseResponseDto> ListTest(BaseRequestPost<long> baseRequest);
        Task<BaseResponseDto> ListDrug(BaseRequestPost<long> baseRequest);
        Task<BaseResponseDto> VitalSign(BaseRequestPost<long> baseRequest);
        Task<BaseResponseDto> PrescriptionInfo(BaseRequestPost<long> baseRequest);
        Task<BaseResponseDto> PrescriptionByShareId(string shareId);
        Task<BaseResponseDto> SetShare(long id);
        Task<BaseResponseDto> GetShare(string fileName);
        Task<BaseResponseDto> RadiologyByShareId(string shareId);
    }
}
