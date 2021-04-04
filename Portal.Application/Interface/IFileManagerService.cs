using His.Reception.DTO;
using Portal.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace His.Reception.Application.Interface
{
    public interface IFileManagerService
    {
        Task<FileManagerDto> GetFiles(List<string> fileIds);
        Task<FileManagerDataDto> GetById(string id);
        Task<byte[]> Download(string id);
        Task<string> GetImageAsBase64(string id);
        Task<string> Upload(FileUploadDto files);
        Task<string> UploadFiles(MultiUploadFilesDto files);
        Task<(byte[], string)> DownloadFileStream(string id);
    }
}
