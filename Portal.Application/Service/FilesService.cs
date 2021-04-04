
using His.Reception;
using His.Reception.Application.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Validation;
using Portal.Context;
using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Interface;
using Portal.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqKit;

namespace Portal.Application.Service
{
    public class FilesService : IFilesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Files> _files;
        private readonly DbSet<FileGroup> _repositoryFileGroup;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IWorkContextService _workContext;

        public FilesService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer, IWorkContextService workContext)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _files = _unitOfWork.Set<Files>();
            _repositoryFileGroup = _unitOfWork.Set<FileGroup>();
            _workContext = workContext;
        }

        public async Task AddAsync(List<FilesDto> files)
        {
            //var resultValid = CheckValidate.Valid<FilesDto>(new FilesValidation(_sharedLocalizer), file);
            //if (resultValid.Status == ResponseStatus.NotValid)
            //{
            //    throw new Exception(resultValid.Message);
            //}

            foreach (var file in files)
            {
                var newFile = MyMapper<FilesDto, Files>.Copy(file);

                await _files.AddAsync(newFile);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddAsync(FilesDto file)
        {
            var newFile = MyMapper<FilesDto, Files>.Copy(file);

            await _files.AddAsync(newFile);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AddTagsAsync(List<FileAndTagUploadDto> file)
        {
            foreach (var item in file)
            {
                var map = Mapper.FileMapper.MapMulit(item, _workContext.UserId.GetValueOrDefault());
                _files.Add(map);
            }

            await _unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var fileGroup = await _files.FindAsync(id);
            if (fileGroup == null)
            {
                throw new Exception(_sharedLocalizer["NoFoundRecord"]);//+
            }

            _files.Remove(fileGroup);

            await _unitOfWork.SaveChangesAsync(); ;
        }

        public async Task EditAsync(FilesDto file)
        {
            var resultValid = CheckValidate.Valid<FilesDto>(new FilesValidation(_sharedLocalizer), file);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                throw new Exception(resultValid.Message);
            }

            var files = await _files.FindAsync(file.Id);
            if (files == null)
            {
                throw new Exception(_sharedLocalizer["NoFoundRecord"]);//+
            }

            files = MyMapper<FilesDto, Files>.Update(file, files);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<Files>> GetAll(FilesDto file)
        {
            var query = _files.AsQueryable();
            if (file.FileGroupId != null)
            {
                query = query.Where(n => n.FileGroupId == file.FileGroupId);
            }
            if (file.PrimeryKey != null)
            {
                query = query.Where(n => n.PrimeryKey == file.PrimeryKey);
            }
            if (file.TableName != null)
            {
                query = query.Where(n => n.TableName == file.TableName);
            }
            if (file.RefferKey != null)
            {
                query = query.Where(n => n.RefferKey == file.RefferKey);
            }

            var result = await query.ToListAsync();

            return result;
        }

        public Task<bool> GetAnyFilesByFileGroupId(int fileGroupCode)
        {
            return _files.AnyAsync(n => n.FileGroupId == fileGroupCode);
        }

        public async Task<List<FileGroupDto>> GetFileGroupWithFilesCount(string tableName, string primaryKey)
        {
            var files = await _files
                .Where(p => p.TableName == tableName && p.PrimeryKey == primaryKey)
                .AsNoTracking()
                .GroupBy(n => n.FileGroupId)
                .Select(s => new
                {
                    fileGroupId = s.Key,
                    Count = s.Count()
                }).ToListAsync();

            var result = await _repositoryFileGroup
                .Select(s => new FileGroupDto
                {
                    Id = s.Id,
                    ParentId = s.ParentId ?? 0,
                    Title =Utilities.Language.GetTilteByLang(s,Utilities.Language.CurrentLanguage),
                    Code = s.Code,
                    LevelNo = s.LevelNo
                    //FileCount = files.FirstOrDefault(n => n.fileGroupId == s.Id).Count
                }).ToListAsync();

            result.ForEach(d => d.FileCount = files.Where(n => n.fileGroupId == d.Id).Select(g => g.Count).FirstOrDefault());

            return result;
        }

        public async Task<List<FilesDto>> GetFilesByFileGroupId(int fileGroupId, string tableName, string primaryKey)
        {
            if (fileGroupId == 0 || string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(primaryKey))
            {
                throw new ArgumentNullException("Parameter Not Valid ");
            }

            return await _files
                .Where(p =>
                        p.TableName == tableName &&
                        p.PrimeryKey == primaryKey &&
                        p.FileGroupId == fileGroupId)
                .AsNoTracking()
                .Select(s => new FilesDto
                {
                    Id = s.Id,
                    RefferKey = s.RefferKey,
                    FileGroupId = s.FileGroupId,
                    FileGroupTitle = s.FileGroup.Title
                })
                .ToListAsync();
        }

        public async Task<List<FilesDto>> GetFilesByFileGroupId(int fileGroupId, string tableName, List<string> primaryKey)
        {
            if (fileGroupId == 0 || string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("Parameter Not Valid ");
            }

            return await _files
                .Where(p =>
                        p.TableName == tableName &&
                        //p.PrimeryKey == primeryKey &&
                        p.FileGroupId == fileGroupId &&
                        primaryKey.Contains(p.PrimeryKey)
                )
                .AsNoTracking()
                .Select(s => new FilesDto
                {
                    Id = s.Id,
                    RefferKey = s.RefferKey,
                    FileGroupId = s.FileGroupId,
                    FileGroupTitle = s.FileGroup.Title,
                    TableName = s.TableName,
                    PrimeryKey = s.PrimeryKey
                })
                .ToListAsync();
        }

        public async Task<List<FileAndTagUploadDto>> GetFileTagsAsync(string fileGroupCode, string tableName, string primaryKey)
        {
            return await _files.Where(x => x.TableName == tableName && x.FileGroup.Code == fileGroupCode && x.PrimeryKey == primaryKey)
                .Select(x => new FileAndTagUploadDto
                {
                    Id = x.Id,
                    RefferKey = x.RefferKey,
                    FileGroupId = x.FileGroupId,
                    FileGroupTitle = x.FileGroup.Title,
                    TableName = x.TableName,
                    PrimeryKey = x.PrimeryKey,
                    Tags = x.FileTag.Select(y => y.TagName).ToList(),
                    FileName = x.FileName,
                    CreateDate = x.CreateDate.ToDateStringTry()
                }).AsNoTracking().ToListAsync();
        }

        // مپر نوشته شود
        public async Task<ListResponseDto> SearchFile(SearchFileDto dto, string tableName, string primaryKey)
        {
            var queryable = _files.Where(x => x.TableName == tableName && x.PrimeryKey == primaryKey)
                .If(!string.IsNullOrEmpty(dto.GroupCode), x => x.Where(y => y.FileGroup.Code == dto.GroupCode))
                .If(!string.IsNullOrEmpty(dto.Name), x => x.Where(y => EF.Functions.Like(y.FileName, $"%{dto.Name}%")))
                .AsQueryable();

            var count = await queryable.CountAsync();
            if (count == 0) return ListResponseDto.Success();

            var files = await queryable.Select(x => new FileAndTagUploadDto
            {
                Id = x.Id,
                RefferKey = x.RefferKey,
                FileGroupId = x.FileGroupId,
                FileGroupTitle = x.FileGroup.Title,
                TableName = x.TableName,
                PrimeryKey = x.PrimeryKey,
                Tags = x.FileTag.Select(y => y.TagName).ToList(),
                FileName = x.FileName,
                CreateDate = x.CreateDate.ToDateStringTry()
            }).ToPagedQuery(dto).AsNoTracking().ToListAsync();

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = count,
                Data = files
            };
        }

        public async Task ChangeGroupOfFiles(List<ChangeFileGroupDto> dtos, string tableName, string primaryKey)
        {
            if (!dtos.TryAny()) return;

            var ids = dtos.Select(x => x.FileId).ToList();
            var files = await _files.Where(x => x.TableName == tableName && x.PrimeryKey == primaryKey && ids.Contains(x.Id)).ToListAsync();

            foreach (var file in files) file.FileGroupId = dtos.Where(x => x.FileId == file.Id).First().GroupId;

            _files.UpdateRange(files);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
