using His.Reception.Application.Interface;
using Portal.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.Application.Validation;
using Portal.Context;
using Portal.DTO;
using Portal.DTO.Doctor;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Portal.Application.Mapper;

namespace Portal.Application.Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Doctors> _doctorRepository;
        private readonly DbSet<Prescription> _prescriptionRepository;
        private readonly DbSet<DoctorVisitType> _doctorVisitRepository;
        private readonly DbSet<CenterWorkItem> _centerWorkItemsRepository;
        private readonly DbSet<CenterExamPlace> _centerExamPlaceRepository;
        private readonly DbSet<DoctorCertificate> _doctorCertificateRepository;
        private readonly DbSet<Certificate> _certificateRepository;
        private readonly DbSet<DataTransferConvert> _dataTransferConvertRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IFilesService _filesService;
        private readonly IFileManagerService _fileManagerService;

        public DoctorService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer, IFilesService filesService, IFileManagerService fileManagerService)
        {
            _doctorRepository = unitOfWork.Set<Doctors>();
            _prescriptionRepository = unitOfWork.Set<Prescription>();
            _doctorVisitRepository = unitOfWork.Set<DoctorVisitType>();
            _centerWorkItemsRepository = unitOfWork.Set<CenterWorkItem>();
            _centerExamPlaceRepository = unitOfWork.Set<CenterExamPlace>();
            _certificateRepository = unitOfWork.Set<Certificate>();
            _doctorCertificateRepository = unitOfWork.Set<DoctorCertificate>();
            _dataTransferConvertRepository = unitOfWork.Set<DataTransferConvert>();

            _filesService = filesService;
            _fileManagerService = fileManagerService;
            
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }
       

        //todo حتما سرویس جدا شود
        private async Task<List<IdTitle<int>>> AddCertificateWithTitle(List<string> titles)
        {
            var existCertificates = await _certificateRepository.Where(x => titles.Contains(x.Title)).ToListAsync();
            titles.RemoveAll(x=>existCertificates.Select(y=>y.Title).ToList().Contains(x));

            var addCertificates = titles.Select(x => new Certificate {Title = x}).ToList();
            _certificateRepository.AddRange(addCertificates);
            await _unitOfWork.SaveChangesAsync();

            existCertificates.AddRange(addCertificates);
            return existCertificates.Select(x=>new IdTitle<int>{Id = x.Id,Title = x.Title}).ToList();
        }

        public async Task<BaseResponseDto> Add(DoctorDto dto)
        {
            var resultValid = CheckValidate.Valid(new DoctorValidation(_sharedLocalizer), dto);
            if (resultValid.Status == ResponseStatus.NotValid) return resultValid;

            if (dto.Certificates.TryAny())
            {
                var certificateTitles = dto.Certificates.Select(x => x.Title).ToList();
                dto.Certificates = await AddCertificateWithTitle(certificateTitles);
            }

            var map = DoctorMapper.Map(dto);
            _doctorRepository.Add(map);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["DoctorFrom.Response.AddSuccess"],//+
                Data = new { CenterId = map.Id, PersonId = map.PersonId }
            };
        }

        public async Task<BaseResponseDto> Edit(DoctorDto dto)
        {
            var resultValid = CheckValidate.Valid(new DoctorValidation(_sharedLocalizer), dto);
            if (resultValid.Status == ResponseStatus.NotValid) return resultValid;

            var cur = await _doctorRepository.Where(x => x.Id == dto.Id)
                .Include(x => x.Person)
                .Include(x => x.Center).ThenInclude(x => x.Section)
                .Include(x => x.DoctorVisitType)
                .Include(x => x.Center).ThenInclude(x => x.CenterExamPlace)
                .Include(x => x.Center).ThenInclude(x => x.CenterWorkItem)
                .Include(x=>x.DoctorCertificate).ThenInclude(x=>x.Certificate)
                .FirstOrDefaultAsync();

            _doctorVisitRepository.RemoveRange(cur.DoctorVisitType);
            _centerExamPlaceRepository.RemoveRange(cur.Center.CenterExamPlace);
            _centerWorkItemsRepository.RemoveRange(cur.Center.CenterWorkItem);
            _doctorCertificateRepository.RemoveRange(cur.DoctorCertificate);

            if (dto.Certificates.TryAny())
            {
                var certificateTitles = dto.Certificates.Select(x => x.Title).ToList();
                dto.Certificates = await AddCertificateWithTitle(certificateTitles);
            }

            var map = DoctorMapper.Map(cur, dto);
            _doctorRepository.Update(map);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["DoctorFrom.Response.EditSuccess"],
                Data = new { CenterId = map.Id }
            };
        }

        public Task<BaseResponseDto> Delete(int id)=> throw new NotImplementedException();

        public async Task<BaseResponseDto> GetById(int id)
        {
            var cur = await _doctorRepository.Where(x => x.Id == id)
                .Include(x => x.Person.Users)
                .Include(x => x.Center).ThenInclude(x => x.Section)
                .ThenInclude(x => x.UserRolePermission).ThenInclude(x => x.User)
                
                .Include(x => x.DoctorVisitType)
                .Include(x => x.Center).ThenInclude(x => x.CenterExamPlace)
                .Include(x => x.Center).ThenInclude(x => x.CenterWorkItem)
                .Include(x => x.Center).ThenInclude(x => x.CenterSellingType)
                .Include(x=>x.DoctorCertificate).ThenInclude(x=>x.Certificate)
                .FirstOrDefaultAsync();

            var fileId = await _filesService.GetFilesByFileGroupId(1, nameof(Doctors), cur.Id.ToString());
            var map = DoctorMapper.Map(cur);
            map.FileId = fileId.OrderByDescending(x => x.Id).Select(x => x.RefferKey).FirstOrDefault();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = map
            };

        }

        public async Task<BaseResponseDto> Info(int doctorId)
        {
            var doctor = await _doctorRepository.Where(x => x.Id == doctorId)
                .Include(x => x.Person)
                .Include(x => x.Center).ThenInclude(x => x.CenterWorkItem).ThenInclude(x => x.WorkItem)
                .Include(x => x.Center).ThenInclude(x => x.CenterExamPlace).ThenInclude(x => x.Examplace)
                .Include(x => x.Expertise)
                .Include(x => x.DoctorCertificate).ThenInclude(x=>x.Certificate)
                .FirstOrDefaultAsync();

            var map = DoctorMapper.MapInfo(doctor);

            var logoFile = await _filesService.GetFilesByFileGroupId(1, nameof(Doctors), doctorId.ToString());
            map.LogoFId = logoFile.TryAny() ? logoFile[0].RefferKey : null;

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = map
            };
        }

        public async Task<ListResponseDto> Search(FilterDoctorDto dto)
        {
            var queryable = _doctorRepository.OrderByDescending(x => x.Id)
                .If(dto.CountryId != null, x => x.Where(y => y.Center.CountryId == dto.CountryId))
                .If(dto.ProvinceId != null, x => x.Where(y => y.Center.ProvinceId == dto.ProvinceId))
                .If(dto.CityId != null, x => x.Where(y => y.Center.CityId == dto.CityId))
                .If(dto.ZoneId != null, x => x.Where(y => y.Center.ZoneId == dto.ZoneId))
                .If(dto.ExpertiseId != null, x => x.Where(y => y.ExpertiseId == dto.ExpertiseId))
                .If(!string.IsNullOrEmpty(dto.MedicalSystemNo), x => x.Where(y => y.MedicalSystemNo == dto.MedicalSystemNo))
                .If(!string.IsNullOrEmpty(dto.Name), x => x.Where(y => EF.Functions.Like(y.Person.FirstName + " " + y.Person.LastName, $"%{dto.Name}%")))
                .If(dto.ScientificlevelId != null, x => x.Where(y => y.ScientificlevelId == dto.ScientificlevelId))
                .If(!string.IsNullOrEmpty(dto.NationalCode), x => x.Where(y => y.Person.NationalCode == dto.NationalCode))
                .AsQueryable();

            var count = await queryable.CountAsync();
            if (count == 0) return ListResponseDto.Success();

            var doctors = await queryable.ToPagedQuery(dto).Select(DoctorMapper.MapList)
                .ToListAsync();

            var doctorUserIds = doctors.Select(x => x.UserId).ToList();
            //var prescriptionPatients = await _prescriptionRepository
            //    .Include(x=>x.Reception)
            //    .Where(x => doctorIds.Contains(x.DoctorId.GetValueOrDefault()) && x.Reception!=null)
            //    .Select(x => new { x.DoctorId, x.Reception.PatientId })
            //    .Distinct().ToListAsync();

            var patientSynced = await _dataTransferConvertRepository
                .Where(x => x.TableName == "Patient" && doctorUserIds.Contains(x.UserId.GetValueOrDefault()))
                .Select(x=>new {x.UserId})
                .GroupBy(x=>x.UserId).Select(x=>new{UserId=x.Key,Count=x.Count()})
                .ToListAsync();
            doctors.ForEach(x => x.CountPatientSynced = patientSynced.Where(y => y.UserId == x.UserId).Select(y=>y.Count).FirstOrDefault());

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = count,
                Data = doctors
            };
        }

        public async Task<ListResponseDto> SearchAppHome(FilterDoctorAppHomeDto dto)
        {
            var queryable = _doctorRepository.OrderByDescending(x => x.Id)
                .Where(x => x.IsActive.Value)
                .If(!string.IsNullOrEmpty(dto.FullName), x => x.Where(y => EF.Functions.Like(y.Person.FirstName + " " + y.Person.LastName, $"%{dto.FullName}%")))
                .If(dto.SexId != null, x => x.Where(y => y.Person.SexId == dto.SexId))
                .If(dto.CountryId != null, x => x.Where(y => y.Center.CountryId == dto.CountryId))
                .If(dto.ProvinceId != null, x => x.Where(y => y.Center.ProvinceId == dto.ProvinceId))
                .If(dto.CityId != null, x => x.Where(y => y.Center.CityId == dto.CityId))
                .If(dto.ZoneId != null, x => x.Where(y => dto.ZoneId.Contains(y.Center.ZoneId.GetValueOrDefault())))
                .If(dto.ExpertiseId != null, x => x.Where(y => y.ExpertiseId == dto.ExpertiseId))
                .AsQueryable();

            if (dto.IsVisitType != null)
            {
                var visitTypeId = dto.IsVisitType.GetValueOrDefault() ? (int)VisitTypeEnum.MorningTour : (int)VisitTypeEnum.NightTour;
                queryable = queryable.Where(d => d.DoctorVisitType.Any(t => t.VisitTypeId == visitTypeId));
            }

            if (dto.IsGovernmental != null)
            {
                var ownershipTypeId = dto.IsGovernmental.GetValueOrDefault() ? (int)OwnershipTypeEnum.Governmental : (int)OwnershipTypeEnum.Special;
                queryable = queryable.Where(d => d.Center.OwnershipTypeId == ownershipTypeId);
            }

            if (dto.IsHome != null)
            {
                var examPlaceId = dto.IsHome.GetValueOrDefault() ? (int)ExamplaceEnum.OnHome : (int)ExamplaceEnum.OnCenter;
                queryable = queryable.Where(d => d.Center.CenterExamPlace.Any(t => t.ExamplaceId == examPlaceId));
            }

            var count = await queryable.CountAsync();
            if (count == 0) return ListResponseDto.Success();

            var doctorHomes = await queryable.ToPagedQuery(dto).Select(DoctorMapper.MapListHome).AsNoTracking().ToListAsync();

            var filesPrimaryKey = doctorHomes.Select(d => d.Id.ToString()).ToList();
            var files = await _filesService.GetFilesByFileGroupId(1, nameof(Center), filesPrimaryKey);
            foreach (var doctorHome in doctorHomes) doctorHome.Logo = files.Where(x => x.PrimeryKey == doctorHome.Id.ToString())
                .OrderByDescending(x => x.Id).Select(x => x.RefferKey).FirstOrDefault();

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = count,
                Data = doctorHomes
            };
        }

        public async Task<ListResponseDto> SearchHome(FilterDoctorHomeDto dto)
        {
            var queryable = _doctorRepository.OrderByDescending(x => x.Id)
                //.Where(x => x.IsActive == true)
                .If(!string.IsNullOrEmpty(dto.FullName), x => x.Where(y => EF.Functions.Like(y.Person.FirstName + " " + y.Person.LastName, $"%{dto.FullName}%")))
                .If(dto.WorkTimeTypeId != null, x => x.Where(y => y.Center.CenterWorkItem.Any(z => dto.WorkTimeTypeId.Contains(z.WorkItemId.GetValueOrDefault()))))
                .If(dto.ExamplaceId != null, x => x.Where(y => y.Center.CenterExamPlace.Any(z => z.ExamplaceId == dto.ExamplaceId)))
                .If(dto.CountryId != null, x => x.Where(y => y.Center.CountryId == dto.CountryId))
                .If(dto.ProvinceId != null, x => x.Where(y => y.Center.ProvinceId == dto.ProvinceId))
                .If(dto.CityId != null, x => x.Where(y => y.Center.CityId == dto.CityId))
                .If(dto.ZoneId != null, x => x.Where(y => y.Center.ZoneId == dto.ZoneId))
                .If(dto.ExpertiseId != null, x => x.Where(y => dto.ExpertiseId.Contains(y.ExpertiseId.GetValueOrDefault())))
                .If(dto.ScientificlevelId != null, x => x.Where(d => dto.ScientificlevelId.Contains(d.ScientificlevelId.GetValueOrDefault())))
                .If(dto.SexId != null, x => x.Where(y => dto.SexId.Contains(y.Person.SexId.GetValueOrDefault())))
                .If(dto.VisitTypeId != null, x => x.Where(y => y.DoctorVisitType.Any(z => dto.VisitTypeId.Contains(z.VisitTypeId.GetValueOrDefault()))))
                .If(!string.IsNullOrEmpty(dto.PhoneClinic), x => x.Where(y => y.PhoneClinic == dto.PhoneClinic))
                .If(dto.IsActive.HasValue, x => x.Where(y => y.IsActive == dto.IsActive))
                .AsQueryable();

            var count = await queryable.CountAsync();
            if (count == 0) return ListResponseDto.Success();

            var doctorHomes = await queryable.ToPagedQuery(dto).Select(DoctorMapper.MapListHome)
                .AsNoTracking().ToListAsync();

            var filesPrimaryKey = doctorHomes.Select(x => x.Id.ToString()).ToList();
            var files = await _filesService.GetFilesByFileGroupId(1, nameof(Center), filesPrimaryKey);
            foreach (var doctorHome in doctorHomes) doctorHome.Logo = files.Where(x => x.PrimeryKey == doctorHome.Id.ToString())
                .OrderByDescending(x => x.Id).Select(x => x.RefferKey).FirstOrDefault();

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = count,
                Data = doctorHomes
            };
        }

        public async Task<BaseResponseDto> UploadLogo(UploadLogoDoctorDto dto)
        {
            var fileUpload = new FileUploadDto
            {
                PrimeryKey = dto.DoctorId.ToString(),
                File = dto.File,
                TableName = nameof(Doctors),
                FileGroupId = 1
            };

            await _fileManagerService.Upload(fileUpload);
            return new BaseResponseDto { Status = ResponseStatus.Success };
        }

        public async Task<BaseResponseDto> SetIsActive(List<DoctorDto> dtos)
        {
            var ids = dtos.Select(y => y.Id).ToList();
            var doctors = await _doctorRepository.Where(x => ids.Contains(x.Id)).ToListAsync();

            foreach (var doctor in doctors)
            {
                doctor.IsActive = dtos.Where(s => s.Id == doctor.Id).Select(x=>x.IsActive).FirstOrDefault();
            }

            _doctorRepository.UpdateRange(doctors);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["DoctorFrom.Response.EditIsActiveSuccess"],//*
                Data = doctors
            };
        }
    }
}

