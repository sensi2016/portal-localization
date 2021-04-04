using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface ICityService
    {
        Task<BaseResponseDto> GetById(int id);
        Task<ListResponseDto> GetAll(IPaging parameter=null);
        Task<ListResponseDto> Search(CityDto dto);

        Task<ListResponseDto> Add(CityDto dto);
        Task<ListResponseDto> Edit(CityDto dto);
        Task<ListResponseDto> Delete(BaseRequestPost<int> dto);
    }
}
