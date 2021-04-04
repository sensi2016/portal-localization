using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using His.Reception.Application.Interface;
using His.Reception.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portal.DTO;

namespace His.Reception.Api.Areas.Pacs.Controller
{
    [Area("Pacs")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class FilesGroupController : ControllerBase
    {
        private readonly IFileGroupService _fileGroup;
        private readonly IFilesService _files;

        public FilesGroupController(IFileGroupService fileGroup,
            IFilesService files)
        {
            _fileGroup = fileGroup;
            _files = files;
        }

        [HttpPost("AddAsync")]
        public async Task<ApiResult> AddAsync(FileGroupDto fileGroup)
        {
            await _fileGroup.AddAsync(fileGroup);

            return Ok();
        }


        [HttpPost("EditAsync")]
        public async Task<ApiResult> EditAsync(FileGroupDto file)
        {
            await _fileGroup.EditeAsync(file);

            return Ok();
        }


        [HttpPost("DeleteAsync")]
        public async Task<ApiResult> DeleteAsync(int id)
        {
            var IsHasChild = await _files.GetAnyFilesByFileGroupId(id);
            if (IsHasChild)
            {
                throw new Exception("");
            }

            await _fileGroup.DeleteAsync(id);

            return Ok();
        }

        /// <summary>
        /// get all fileGroup with files count in each group
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFileGroupWithFilesCount")]
        public async Task<ApiResult<List<FileGroupDto>>> GetFileGroupWithFilesCount(string tableName, string primaryKey)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(primaryKey))
            {
                return BadRequest();
            }

            return await _fileGroup.GetFileGroupWithFilesCount(tableName, primaryKey);
        }

        /// <summary>
        /// get all fileGroup
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<ApiResult<List<FileGroupDto>>> GetAll(string tableName, string primaryKey)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(primaryKey))
            {
                return BadRequest();
            }

            return await _fileGroup.GetAll();
        }

        [HttpGet("GetAllByParentId")]
        public async Task<ApiResult<List<FileGroupDto>>> GetChildByCodeId(int id)
        {
            return await _fileGroup.GetChildByCodeId(id);
        }
    }
}