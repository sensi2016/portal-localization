using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface ICenterService
    {
        Task<BaseResponseDto> GetById(int id);
        Task<BaseResponseDto> GetCenterInfo(int id);
        Task<ListResponseDto> Search(FilterCenterDto filterCenterDto);
        Task<ListResponseDto> Search(FilterHomeCenterDto filterCenterDto);
        Task<ListResponseDto> SearchApp(FilterHomeCenterAppDto filterHomeCenterAppDto);
        Task<BaseResponseDto> Add(CenterDto centerDto);
        Task<BaseResponseDto> Edit(CenterDto centerDto);
        Task<BaseResponseDto> Delete(int id);
        Task<BaseResponseDto> UploadLogo(UploadLogoDto uploadLogoDto);
        Task<BaseResponseDto> SetIsActive(List<CenterDto> dtos);



    }
}
