
using Portal.DTO;

namespace Portal.DAL.Extensions
{
 public static  class BaseResponseDtoExtension
    {
        public static ListResponseDto ToListResponseDto(this BaseResponseDto value)
        {
            return new ListResponseDto
            {
                Status = value.Status,
                Message = value.Message,
                Errors = value.Errors,
                Count = 0,
                Data = value.Data
            };
        }
    }
}
