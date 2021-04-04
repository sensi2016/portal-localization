using Portal.DTO;
using Portal.DTO.City;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface IProvinceService
    {
        Task<BaseResponseDto> GetById(int id);
        Task<ListResponseDto> GetAll(IPaging parameter = null);
        Task<ListResponseDto> Search(ProvinceDto dto);

        Task<ListResponseDto> Add(ProvinceDto dto);
        Task<ListResponseDto> Edit(ProvinceDto dto);
        Task<ListResponseDto> Delete(BaseRequestPost<int> dto);

    }
}
