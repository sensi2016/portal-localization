using His.Reception.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Portal.DTO;

namespace Portal.Application.Interface
{
    public interface IMenuService
    {
        Task<BaseResponseDto> GetAllByUserId(MenuGetDto menuGetDto);

        Task<BaseResponseDto> GetMenuRight(MenuGetDto menuGetDto, long? receptionId = 0);
    }
}
