using His.Reception.Application.Interface;
using Portal.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.Application.Validation;
using Portal.Context;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Service
{
    public class CenterService : ICenterService
    {
        private readonly DbSet<Center> _centerRepository;
        private readonly DbSet<CenterExamPlace> _centerExamPlaceRepository;
        private readonly DbSet<CenterSellingType> _centerSellingTypeRepository;
        private readonly DbSet<CenterServices> _centerServicesRepository;
        private readonly DbSet<CenterWorkItem> _centerWorkItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IFileManagerService _fileManagerService;
        private readonly IFilesService _filesService;

        public CenterService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer, IFileManagerService fileManagerService, IFilesService filesService)
        {
            _unitOfWork = unitOfWork;
            _centerRepository = _unitOfWork.Set<Center>();
            _centerExamPlaceRepository = _unitOfWork.Set<CenterExamPlace>();
            _centerSellingTypeRepository = _unitOfWork.Set<CenterSellingType>();
            _centerServicesRepository = _unitOfWork.Set<CenterServices>();
            _centerWorkItemRepository = _unitOfWork.Set<CenterWorkItem>();
            _fileManagerService = fileManagerService;
            _filesService = filesService;
            _sharedLocalizer = sharedLocalizer;
        }


        public async Task<BaseResponseDto> GetById(int id)
        {
            var cur = await _centerRepository.Where(d => d.Id == id)
                .Include(d => d.CenterExamPlace)
                .ThenInclude(d => d.Examplace)
                .Include(d => d.CenterSellingType)
                .ThenInclude(d => d.SellingType)
                .Include(d => d.CenterWorkItem)
                .ThenInclude(d => d.WorkItem)
                .Include(d => d.CenterServices)
                .ThenInclude(d => d.Center)
                .Include(d => d.Section)
                .ThenInclude(d => d.UserRolePermission)
                .ThenInclude(d => d.User)
                .FirstOrDefaultAsync();


            var fileId = await _filesService.GetFilesByFileGroupId(1, nameof(Center), cur.Id.ToString());

            var map = Mapper.CenterMapper.Map(cur);
            map.FileId = fileId.OrderByDescending(d => d.Id).Select(g => g.RefferKey).FirstOrDefault();
            map.UserName = cur.Section.Select(d => d.UserRolePermission.Select(s => s.User.UserName).FirstOrDefault()).FirstOrDefault();
            map.UserId = cur.Section.Select(d => d.UserRolePermission.Select(s => s.User.Id).FirstOrDefault()).FirstOrDefault();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = map
            };

        }

        public async Task<BaseResponseDto> GetCenterInfo(int id)
        {
            var center = await _centerRepository.Where(x => x.Id == id)
                .Select(x => new CenterInfoDto
                {
                    Id = x.Id,
                    CenterTypeId = x.CenterTypeId,
                    CenterTypeTitle = x.CenterType != null ? x.CenterType.Title : null,
                    Title = x.Title,
                    WorkingHours = x.WorkingHours,
                    OwnerFullName = x.Boss,
                    OwnershipTypeId = x.OwnershipTypeId,
                    OwnershipTypeTitle = x.OwnershipType != null ? x.OwnershipType.Title : null,
                    ProvinceTitle = x.Province != null ? x.Province.Title : null,
                    CityTitle = x.City != null ? x.City.Title : null,
                    IsFreeDelivery = x.IsFreeDelivery.GetValueOrDefault(),
                    IsHomeDelivery = x.IsHomeDelivery,
                    Phone = x.Phone,
                    Address = x.Address,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    Services = x.CenterServices != null && x.CenterServices.Any() ? x.CenterServices.Select(y => new ServiceDto { Id = y.Id, Title = y.Service != null ? y.Service.Title : "" }).ToList() : null,
                    ExamPlaces = x.CenterExamPlace.Select(y => new ExamPlaceDto { Id = y.ExamplaceId.GetValueOrDefault(), Title = y.Examplace != null ? y.Examplace.Title : null }).ToList(),
                    WorkItemTypes = x.CenterWorkItem.Select(y => new WorkItemTypeDto { Id = y.WorkItemId.GetValueOrDefault(), Title = y.WorkItem != null ? y.WorkItem.Title : null }).ToList(),
                    SellTypes = x.CenterSellingType.Select(y => new SellTypeDto { Id = y.SellingTypeId.GetValueOrDefault(), Title = y.SellingType != null ? y.SellingType.Title : null }).ToList(),
                }).FirstOrDefaultAsync();

            var logoFile = await _filesService.GetFilesByFileGroupId(1, nameof(Center), center.Id.ToString());
            center.LogoFId = logoFile.TryAny() ? logoFile[0].RefferKey : null;

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = center
            };
        }

        public async Task<BaseResponseDto> Add(CenterDto dto)
        {
            var resultValid = CheckValidate.Valid(new CenterValidation(_sharedLocalizer), dto);
            if (resultValid.Status == ResponseStatus.NotValid) return resultValid;

            var map = Mapper.CenterMapper.Map(dto);
            _centerRepository.Add(map);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["CenterFrom.Response.AddSuccess"],//+
                Data = new { CenterId = map.Id }
            };

        }

        public async Task<BaseResponseDto> Edit(CenterDto centerDto)
        {
            var resultValid = CheckValidate.Valid<CenterDto>(new CenterValidation(_sharedLocalizer), centerDto);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                return resultValid;
            }

            var cur = await _centerRepository.Where(d => d.Id == centerDto.Id)
                .Include(d => d.CenterExamPlace)
                .Include(d => d.CenterSellingType)
                .Include(d => d.CenterServices)
                .Include(d => d.CenterWorkItem)
                .Include(d => d.Section)
                .FirstOrDefaultAsync();

            _centerExamPlaceRepository.RemoveRange(cur.CenterExamPlace.ToList());
            _centerSellingTypeRepository.RemoveRange(cur.CenterSellingType.ToList());
            _centerServicesRepository.RemoveRange(cur.CenterServices.ToList());
            _centerWorkItemRepository.RemoveRange(cur.CenterWorkItem.ToList());

            var map = Mapper.CenterMapper.Map(cur, centerDto);

            _centerRepository.Update(map);

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["CenterFrom.Response.EditSuccess"]//+
            };

        }

        public Task<BaseResponseDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ListResponseDto> Search(FilterCenterDto filterCenterDto)
        {
            var query = _centerRepository
                .OrderByDescending(d => d.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filterCenterDto.CenterName))
            {
                query = query.Where(d => d.Title.Contains(filterCenterDto.CenterName));
            }

            if (filterCenterDto.CountryId != null)
            {
                query = query.Where(d => d.CityId == filterCenterDto.CityId);
            }

            if (filterCenterDto.CenterTypeId != null)
            {
                query = query.Where(d => d.CenterTypeId == filterCenterDto.CenterTypeId);
            }


            if (filterCenterDto.CountryId != null)
            {
                query = query.Where(d => d.CountryId == filterCenterDto.CountryId);
            }

            if (filterCenterDto.ProvinceId != null)
            {
                query = query.Where(d => d.ProvinceId == filterCenterDto.ProvinceId);
            }


            if (filterCenterDto.ZoneId != null)
            {
                query = query.Where(d => d.ZoneId == filterCenterDto.ZoneId);
            }

            if (filterCenterDto.WorkTimeTypeId != null)
            {
                query = query.Where(d => d.WorkTimeTypeId == filterCenterDto.WorkTimeTypeId);
            }

            if (filterCenterDto.IsActive != null)
            {
                query = query.Where(d => d.IsActive == filterCenterDto.IsActive);
            }

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Data = await query.ToPagedQuery(filterCenterDto).Select(Mapper.CenterMapper.MapList).AsNoTracking().ToListAsync(),
                Count = await query.CountAsync()
            };
        }
        public async Task<BaseResponseDto> SetIsActive(List<CenterDto> dtos)
        {
            var ids = dtos.Select(y => y.Id).ToList();
            var centers = await _centerRepository.Where(x => ids.Contains(x.Id)).ToListAsync();

            foreach (var doctor in centers)
            {
                doctor.IsActive = dtos.Where(s => s.Id == doctor.Id).Select(x=>x.IsActive).FirstOrDefault();
            }

            _centerRepository.UpdateRange(centers);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["CenterForm.Response.EditIsActiveSuccess"],//*
                Data = centers
            };
        }

        #region Home    

        public async Task<ListResponseDto> Search(FilterHomeCenterDto filterCenterDto)
        {
            var query = _centerRepository
                .Where(x=>x.IsActive==true)
                .OrderByDescending(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(filterCenterDto.CenterName))
            {
                query = query.Where(d => EF.Functions.Like(d.Title, $"%{filterCenterDto.CenterName}%"));
            }

            if (filterCenterDto.CenterId != null)
            {
                query = query.Where(d => d.Id == filterCenterDto.CenterId);
            }

            if (filterCenterDto.CityId != null)
            {
                query = query.Where(d => d.CityId == filterCenterDto.CityId);
            }

            if (filterCenterDto.CenterServiceId != null)
            {
                query = query.Where(d => d.CenterServices.Any(t => filterCenterDto.CenterServiceId.Contains(t.ServiceId.GetValueOrDefault())));
            }

            if (filterCenterDto.CenterTypeId != null)
            {
                query = query.Where(d => d.CenterTypeId == filterCenterDto.CenterTypeId);
            }

            if (filterCenterDto.IsHomeDelivery)
            {
                query = query.Where(d => d.IsHomeDelivery == filterCenterDto.IsHomeDelivery);
            }

            if (filterCenterDto.CountryId != null)
            {
                query = query.Where(d => d.CountryId == filterCenterDto.CountryId);
            }

            if (filterCenterDto.OwnershipTypeId != null)
            {
                query = query.Where(d => filterCenterDto.OwnershipTypeId.Contains(d.OwnershipTypeId.GetValueOrDefault()));
            }

            if (filterCenterDto.ProvinceId != null)
            {
                query = query.Where(d => d.ProvinceId == filterCenterDto.ProvinceId);
            }

            if (filterCenterDto.ZoneId != null)
            {
                query = query.Where(d => d.ZoneId == filterCenterDto.ZoneId);
            }

            if (filterCenterDto.WorkTimeTypeId != null)
            {
                query = query.Where(d => d.CenterWorkItem.Any(t => filterCenterDto.WorkTimeTypeId.Contains(t.WorkItemId.GetValueOrDefault())));
            }

            if (filterCenterDto.ExamplaceId != null)
            {
                query = query.Where(d => d.CenterExamPlace.Any(t => t.ExamplaceId == filterCenterDto.ExamplaceId));

            }

            if (filterCenterDto.SellingTypeId != null)
            {
                query = query.Where(d => d.CenterSellingType.Any(t => filterCenterDto.SellingTypeId.Contains(t.SellingTypeId.GetValueOrDefault())));
            }

            var lst = await query.Select(Mapper.CenterMapper.MapListHome).ToPagedQuery(filterCenterDto).AsNoTracking().ToListAsync();
            var lstFiles = await _filesService.GetFilesByFileGroupId(1, nameof(Center), lst.Select(d => d.Id.ToString()).ToList());

            lst.ForEach(d => d.Logo = lstFiles.Where(t => t.PrimeryKey == d.Id.ToString()).OrderByDescending(t => t.Id).Select(g => g.RefferKey).FirstOrDefault());

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lst,
                Count = await query.CountAsync()
            };
        }

        public async Task<BaseResponseDto> UploadLogo(UploadLogoDto uploadLogoDto)
        {
            var result = await _fileManagerService.Upload(new FileUploadDto { PrimeryKey = uploadLogoDto.CenterId.ToString(), File = uploadLogoDto.File, TableName = nameof(Center), FileGroupId = 1 });

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
            };
        }

        public async Task<ListResponseDto> SearchApp(FilterHomeCenterAppDto filterHomeCenterAppDto)
        {
            var query = _centerRepository
                            .OrderByDescending(d => d.Id)
                            .AsQueryable();

            if (!string.IsNullOrEmpty(filterHomeCenterAppDto.CenterName))
            {
                query = query.Where(d => EF.Functions.Like(d.Title, $"%{filterHomeCenterAppDto.CenterName}%"));
            }

            if (filterHomeCenterAppDto.CenterId != null)
            {
                query = query.Where(d => d.Id == filterHomeCenterAppDto.CenterId);
            }

            if (filterHomeCenterAppDto.CityId != null)
            {
                query = query.Where(d => d.CityId == filterHomeCenterAppDto.CityId);
            }

            if (filterHomeCenterAppDto.CenterTypeId != null)
            {
                query = query.Where(d => d.CenterTypeId == filterHomeCenterAppDto.CenterTypeId);
            }

            if (filterHomeCenterAppDto.CountryId != null)
            {
                query = query.Where(d => d.CountryId == filterHomeCenterAppDto.CountryId);
            }

            //if (filterHomeCenterAppDto.OwnershipTypeId != null)
            //{
            //    query = query.Where(d => filterHomeCenterAppDto.OwnershipTypeId.Contains(d.OwnershipTypeId.GetValueOrDefault()));
            //}


            if (filterHomeCenterAppDto.ProvinceId != null)
            {
                query = query.Where(d => d.ProvinceId == filterHomeCenterAppDto.ProvinceId);
            }

            if (filterHomeCenterAppDto.ZoneId?.Count > 0)
            {
                query = query.Where(d => filterHomeCenterAppDto.ZoneId.Contains(d.ZoneId.GetValueOrDefault()));
            }


            //if (filterHomeCenterAppDto.IsVisitType != null)
            //{
            //    var visitTypeId = filterHomeCenterAppDto.IsVisitType.GetValueOrDefault() ? (int)VisitTypeEnum.MorningTour : (int)VisitTypeEnum.NightTour;
            //    query = query.Where(d => d.v.Any(t => t.VisitTypeId == visitTypeId));
            //}

            if (filterHomeCenterAppDto.IsGovernmental != null)
            {
                var ownershipTypeId = filterHomeCenterAppDto.IsGovernmental.GetValueOrDefault() ? (int)OwnershipTypeEnum.Governmental : (int)OwnershipTypeEnum.Special;
                query = query.Where(d => d.OwnershipTypeId == ownershipTypeId);
            }

            if (filterHomeCenterAppDto.IsHome != null)
            {
                var examplaceId = filterHomeCenterAppDto.IsHome.GetValueOrDefault() ? (int)ExamplaceEnum.OnHome : (int)ExamplaceEnum.OnCenter;
                query = query.Where(d => d.CenterExamPlace.Any(t => t.ExamplaceId == examplaceId));
            }

            if (filterHomeCenterAppDto.IsOnlineSelling != null)
            {
                var sellingTypeId = filterHomeCenterAppDto.IsOnlineSelling.GetValueOrDefault() ? (int)SellingTypeEnum.Online : (int)SellingTypeEnum.InPlace;
                query = query.Where(d => d.CenterSellingType.Any(t => t.SellingTypeId == sellingTypeId));
            }

            var lst = await query.Select(Mapper.CenterMapper.MapListHome).ToPagedQuery(filterHomeCenterAppDto).AsNoTracking().ToListAsync();
            var lstFiles = await _filesService.GetFilesByFileGroupId(1, nameof(Center), lst.Select(d => d.Id.ToString()).ToList());

            lst.ForEach(d => d.Logo = lstFiles.Where(t => t.PrimeryKey == d.Id.ToString()).OrderByDescending(t => t.Id).Select(g => g.RefferKey).FirstOrDefault());

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lst,
                Count = await query.CountAsync()
            };
        }

        #endregion
    }
}
