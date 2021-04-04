using His.Reception.DTO;
using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface ISettingService
    {
        Task<BaseResponseDto> GetValue(string keyName);
        Task<BaseResponseDto> GetAll();
        Task<BaseResponseDto> Update(RequestSettingDto requestSettingDtos);

    }
}
