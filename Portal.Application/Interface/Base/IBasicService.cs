using His.Reception.DTO;
using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface.Base
{
    public interface IBasicService<T> where T : class, new()
    {
        Task<BaseResponseDto> Get(int id);
        Task<ListResponseDto> Search(RequestBaseFilterDto requestFilterBaseDto);
        Task<BaseResponseDto> GetAll();
        Task<ListResponseDto> GetListPaging(IPaging paging );
        Task<BaseResponseDto> AddAsync(RequestBaseDto baseDto);
        Task<BaseResponseDto> DeleteAsync(BaseRequestPost<int> baseRequestPost);
        Task<BaseResponseDto> EditAsync(RequestBaseDto baseDto);
    }
}
