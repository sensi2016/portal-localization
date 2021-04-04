using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Portal.DTO.City;

namespace Portal.Application.Interface
{
    public interface IZoneService
    {
        Task<BaseResponseDto> GetById(int id);
        Task<BaseResponseDto> GetByProvinceId(int id);
        Task<ListResponseDto> GetAll(IPaging parameter = null);
        Task<ListResponseDto> Search(ZoneDto dto);

        Task<ListResponseDto> Add(ZoneDto dto);
        Task<ListResponseDto> Edit(ZoneDto dto);
        Task<ListResponseDto> Delete(BaseRequestPost<int> dto);

    }
}
