using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.Context;
using Portal.DTO;
using Portal.DTO.City;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.DAL.Extensions;
using Portal.Infrastructure.Mapper;

namespace Portal.Application.Service
{
    public class ZoneService : IZoneService
    {
        private readonly DbSet<Zone> _zoneRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ZoneService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _zoneRepository = unitOfWork.Set<Zone>();
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<BaseResponseDto> GetById(int id)
        {
            var zone = await _zoneRepository.Include(x => x.Parent).Include(x => x.City)
                .Where(x => x.Id == id).Select(x => Mapper.ZoneMapper.MapToDto(x))
                .AsNoTracking().FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = zone
            };
        }

        public async Task<ListResponseDto> GetAll(IPaging parameter)
        {
            var query = _zoneRepository.Include(x => x.Parent).Include(x => x.City).AsQueryable();

            var count = await query.CountAsync();
            if (count == 0) return ListResponseDto.Success();

            var zones = await query.OrderByDescending(x => x.Id)
                .Select(x => Mapper.ZoneMapper.MapToDto(x))
                .ToPagedQuery(parameter).AsNoTracking().ToListAsync();

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = count,
                Data = zones
            };
        }

        private async Task<ListResponseDto> GetAllPaging(string message, ZoneDto dto)
        {
            var result = new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Message = message
            };

            if (dto.HasValidPaging())
            {
                var getList = await GetAll(dto);
                result.Count = getList.Count;
                result.Data = getList.Data;
            }
            // if (dto.ReturnModel()) result.Data = new List<ZoneDto> { dto };

            return result;
        }

        public Task<ListResponseDto> Search(ZoneDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ListResponseDto> Add(ZoneDto dto)
        {
            //var validation = CheckValidate.Validator(new RoutineValidator(_sharedLocalizer), dto);
            //if (validation.Status == ResponseStatus.NotValid) return validation;

            var model = Mapper.ZoneMapper.MapToModel(dto);
            _zoneRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();

            var message = _sharedLocalizer["ZoneForm.Response.AddZoneSuccess"];//+
            return await GetAllPaging(message, Mapper.ZoneMapper.MapToDto(model));
        }

        public async Task<ListResponseDto> Edit(ZoneDto dto)
        {
            //var validation = CheckValidate.Validator(new RoutineValidator(_sharedLocalizer), dto);
            //if (validation.Status == ResponseStatus.NotValid) return validation;

            var zone = await _zoneRepository.Include(x => x.Parent).Include(x => x.City)
                .Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

            if (zone is null) return ListResponseDto.Success(_sharedLocalizer["ZoneForm.Response.ZoneNotExist"]);

            Mapper.ZoneMapper.MapToModel(dto, zone);
            _zoneRepository.Update(zone);
            await _unitOfWork.SaveChangesAsync();

            var message = _sharedLocalizer["ZoneForm.Response.EditZoneSuccess"]; //+
            return await GetAllPaging(message, Mapper.ZoneMapper.MapToDto(zone));
        }

        public async Task<ListResponseDto> Delete(BaseRequestPost<int> dto)
        {
            var zone = await _zoneRepository.Include(x => x.Parent).Include(x => x.City)
                .Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

            if (zone is null) return ListResponseDto.Success(_sharedLocalizer["ZoneForm.Response.ZoneNotExist"]);

            _zoneRepository.Remove(zone);
            await _unitOfWork.SaveChangesAsync();

            var message = _sharedLocalizer["ZoneForm.Response.DeleteZoneSuccess"]; //+
            return await GetAllPaging(message, AutoMapper.MapTo<ZoneDto>(dto));
        }

        public async Task<BaseResponseDto> GetByProvinceId(int id)
        {
            var lst = await _zoneRepository.Where(d => d.City.ProvinceId == id).Select(g => AutoMapper.MapTo<ZoneDto>(g)).ToListAsync();

            return new BaseResponseDto
            {
                Data = lst,
                Status = ResponseStatus.Success
            };
        }
    }
}
