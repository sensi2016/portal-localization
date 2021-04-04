using His.Reception.Application.Interface;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Validation;
using Portal.Context;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Application.Service
{
    public class FileGroupService : IFileGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<FileGroup> _fileGroup;
        private readonly DbSet<Files> _files;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public FileGroupService(IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _fileGroup = _unitOfWork.Set<FileGroup>();
            _files = _unitOfWork.Set<Files>();
        }

        public async Task AddAsync(FileGroupDto fileGroupDto)
        {
            var resultValid = CheckValidate.Valid<FileGroupDto>(new FileGroupValidation(_sharedLocalizer), fileGroupDto);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                throw new Exception(resultValid.Message);
            }


            var item = MyMapper<FileGroupDto, FileGroup>.Copy(fileGroupDto);

            await _fileGroup.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EditeAsync(FileGroupDto fileGroupDto)
        {
            var resultValid = CheckValidate.Valid<FileGroupDto>(new FileGroupValidation(_sharedLocalizer), fileGroupDto);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                throw new Exception(resultValid.Message);
            }

            var fileGroup = await _fileGroup.FindAsync(fileGroupDto.Id);
            if (fileGroup == null)
            {
                throw new Exception(_sharedLocalizer["NoFoundRecord"]);//+
            }

            fileGroup = MyMapper<FileGroupDto, FileGroup>.Update(fileGroupDto, fileGroup);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var fileGroup = await _fileGroup.FindAsync(id);
            if (fileGroup == null)
            {
                throw new Exception(_sharedLocalizer["NoFoundRecord"]);//+
            }

            _fileGroup.Remove(fileGroup);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<FileGroupDto>> GetFileGroupWithFilesCount(string TableName, string primeryKey)
        {
            var files = await _files
                .Where(p => p.TableName == TableName && p.PrimeryKey == primeryKey)
                .AsNoTracking()
                .GroupBy(n => n.FileGroupId)
                .Select(s => new
                {
                    fileGroupId = s.Key,
                    Count = s.Count()
                }).ToListAsync();

            var result = await _fileGroup
                .Select(s => new FileGroupDto
                {
                    Id = s.Id,
                    ParentId = s.ParentId ?? 0,
                    Title = s.Title,
                    FileCount = files.FirstOrDefault(n => n.fileGroupId == s.Id).Count
                }).ToListAsync();

            return result;
        }

        public async Task<List<FileGroupDto>> GetAll()
        {
            var result = await _fileGroup
                .Select(s => new FileGroupDto
                {
                    Id = s.Id,
                    ParentId = s.ParentId ?? 0,
                    Title = s.Title,
                    LevelNo =s.LevelNo
                }).ToListAsync();

            return result;
        }

        public async Task<List<FileGroupDto>> GetChildByCodeId(int id)
        {
            var parent = await _fileGroup.FindAsync(id);
            var child = await _fileGroup.Where(n => n.ParentId == id).ToListAsync();
            if(child!= null)
            {
                child.Add(parent);
            }

            return child.Select(s => new FileGroupDto
            {
                Id = s.Id,
                ParentId = s.ParentId ?? 0,
                Title = s.Title,
                LevelNo = s.LevelNo
            }).ToList();
        }
    }
}
