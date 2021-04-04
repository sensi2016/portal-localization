using His.Reception.DTO;
using Portal.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace His.Reception.Application.Interface
{
    public interface IFileGroupService
    {
        Task DeleteAsync(int id);
        Task AddAsync(FileGroupDto fileGroup);
        Task EditeAsync(FileGroupDto fileGroup);
        Task<List<FileGroupDto>> GetFileGroupWithFilesCount(string tableName, string primeryKey);
        Task<List<FileGroupDto>> GetAll();
        Task<List<FileGroupDto>> GetChildByCodeId(int id);
    }
}
