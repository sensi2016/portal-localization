using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using His.Reception.Application.Interface;
using His.Reception.DTO;

using Microsoft.AspNetCore.Mvc;
using Portal.DTO;

namespace His.Reception.Api.Areas.Pacs.Controller
{
    [Area("Pacs")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFilesService _filesService;
        private readonly IFileManagerService _fileManagerService;

        public FilesController(
            IFilesService filesService,
            IFileManagerService fileManagerService)
        {
            _filesService = filesService;
            _fileManagerService = fileManagerService;
        }


        [HttpPost("AddAsync")]
        public async Task<ApiResult> AddAsync(List<FilesDto> files)
        {
            await _filesService.AddAsync(files);
           
            return Ok();
        }

        [HttpPost("EditAsync")]
        public async Task<ApiResult> EditAsync(FilesDto file)
        {
            await _filesService.EditAsync(file);

            return Ok();
        }

        [HttpPost("DeleteAsync")]
        public async Task<ApiResult> DeleteAsync(int id)
        {
            await _filesService.DeleteAsync(id);

            return Ok();
        }

        /// <summary>
        /// get all files with fileGroupId, tableName and tableId
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFilesByFileGroupId")]
        public async Task<ApiResult<List<FilesDto>>> GetFilesByFileGroupId(int fileGroupId, string tableName, string primaryKey)
        {
            return await _filesService.GetFilesByFileGroupId(fileGroupId, tableName, primaryKey);
        }

        [HttpPost("GetAll")]
        public async Task<ApiResult<FileManagerDto>> GetAll(FilesDto file)
        {
            var data = await _filesService.GetAll(file);
            var ids = data.Select(n => n.RefferKey).ToList();

           var result = await _fileManagerService.GetFiles(ids);

            return result; 
        }
        
        [HttpGet("GetById/{id}")]
        public async Task<ApiResult<FileManagerDataDto>> GetById(string id)
        {
           var result = await _fileManagerService.GetById(id);

            return result; 
        }

        [HttpGet("Download/{id}")]
        public async Task<ApiResult<byte[]>> Download(string id)
        {
            var result = await _fileManagerService.Download(id);
            return result;
        }

        [HttpGet("GetImageAsBase64/{id}")]
        public async Task<ApiResult<string>> GetImageAsBase64(string id)
        {
            var result = await _fileManagerService.GetImageAsBase64(id);
            return result;
        }

        [HttpPost("Upload")]
        public async Task<ApiResult<string>> Upload([FromForm]FileUploadDto files)
        {
            var result = await _fileManagerService.Upload(files);
            return result;
        }


    }
}