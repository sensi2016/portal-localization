using Portal.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Interface
{
    public interface IPatientService
    {
        Task<BaseResponseDto> UploudFiles(RequestUploadFilesDto requestUploadFilesDto);
        Task<BaseResponseDto> GetFiles(string fileGroupCode);
        Task<BaseResponseDto> ListGroupFile();
        Task<ListResponseDto> SearchFile(SearchFileDto dto);
        Task<BaseResponseDto> ChangeGroupOfFiles(List<ChangeFileGroupDto> dto);
    }
}
