using His.Reception.Application.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.Context;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Service
{
    public class PatientService : IPatientService
    {
        private readonly IWorkContextService _workContextService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IFileManagerService _fileManagerService;
        private readonly IFilesService _filesService;
        private readonly DbSet<Users> _userRepository;

        public PatientService(IUnitOfWork unitOfWork, IWorkContextService workContextService, IStringLocalizer<SharedResource> sharedLocalizer, IFileManagerService fileManagerService, IFilesService filesService)
        {
            _workContextService = workContextService;
            _userRepository = unitOfWork.Set<Users>();
            _sharedLocalizer = sharedLocalizer;
            _fileManagerService = fileManagerService;
            _filesService = filesService;
        }

        public async Task<BaseResponseDto> GetFiles(string fileGroupCode)
        {
            var userPersonId = await _userRepository.Where(x => x.Id == _workContextService.UserId.GetValueOrDefault())
                .Select(x=>x.PersonId.GetValueOrDefault().ToString()).FirstOrDefaultAsync();

            var files = await _filesService.GetFileTagsAsync(fileGroupCode, nameof(Person), userPersonId);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = files,
            };
        }

        public async Task<ListResponseDto> SearchFile(SearchFileDto dto)
        {
            var userPersonId = await _userRepository.Where(x => x.Id == _workContextService.UserId.GetValueOrDefault())
                .Select(x => x.PersonId.GetValueOrDefault().ToString()).FirstOrDefaultAsync();

            return await _filesService.SearchFile(dto,nameof(Person), userPersonId);
        }

        public async Task<BaseResponseDto> ChangeGroupOfFiles(List<ChangeFileGroupDto> dto)
        {
            var userPersonId = await _userRepository.Where(x => x.Id == _workContextService.UserId.GetValueOrDefault())
                .Select(x => x.PersonId.GetValueOrDefault().ToString()).FirstOrDefaultAsync();
            await _filesService.ChangeGroupOfFiles(dto, nameof(Person), userPersonId);

            return BaseResponseDto.Success("");
        }

        public async Task<BaseResponseDto> ListGroupFile()
        {
            var user =await _userRepository.Where(x => x.Id == _workContextService.UserId.GetValueOrDefault()).FirstOrDefaultAsync();
            var lst = await _filesService.GetFileGroupWithFilesCount(nameof(Person), user.PersonId.GetValueOrDefault().ToString());

            return new BaseResponseDto
            {
                Data = lst,
                Status = ResponseStatus.Success
            };
        }

        public async Task<BaseResponseDto> UploudFiles(RequestUploadFilesDto requestUploadFilesDto)
        {
            var user =await _userRepository.Where(d => d.Id == _workContextService.UserId.GetValueOrDefault()).FirstOrDefaultAsync();
            await  _fileManagerService.UploadFiles(new MultiUploadFilesDto {CategoryID="0",FileGroupCode= requestUploadFilesDto.FileGroupCode,Files= requestUploadFilesDto.Files,Tags= requestUploadFilesDto.Tags,TableName=nameof(Person),PrimeryKey= user.PersonId.ToString() });

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message=_sharedLocalizer["UploadFrom.Response.Success"]
            };
        }
    }
}
