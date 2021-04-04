using Portal.DTO;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Interface
{
    public interface IFilesService
    {
        Task<List<FilesDto>> GetFilesByFileGroupId(int fileGroupId, string tableName, string primaryKey);
        Task<bool> GetAnyFilesByFileGroupId(int fileGroupId);
        Task<List<FilesDto>>  GetFilesByFileGroupId(int fileGroupId, string tableName, List<string> primaryKey);
        Task<List<Files>> GetAll(FilesDto file);
        Task<List<FileGroupDto>> GetFileGroupWithFilesCount(string tableName, string primaryKey);
       
        Task DeleteAsync(int id);
        Task AddAsync(List<FilesDto> files);
        Task AddAsync(FilesDto file);
        Task AddTagsAsync(List<FileAndTagUploadDto> file);
        Task<List<FileAndTagUploadDto>> GetFileTagsAsync(string fileGroupId, string tableName, string primaryKey);
        Task EditAsync(FilesDto file);

        Task<ListResponseDto> SearchFile(SearchFileDto dto, string tableName, string primaryKey);
        Task ChangeGroupOfFiles(List<ChangeFileGroupDto> dtos, string tableName, string primaryKey);
    }
}
