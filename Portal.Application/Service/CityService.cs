using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.Context;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Portal.DAL.Extensions;
using Portal.Infrastructure.Mapper;

namespace Portal.Application.Service
{
    public class CityService : ICityService
    {
        private readonly DbSet<City> _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public CityService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _cityRepository = unitOfWork.Set<City>();

            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<BaseResponseDto> GetById(int id)
        {
            var city = await _cityRepository.Include(x=>x.Province)
                .Where(x => x.Id == id).Select(x => Mapper.CityMapper.MapToDto(x))
                .AsNoTracking().FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = city
            };
        }

        public async Task<ListResponseDto> GetAll(IPaging parameter)
        {
            var query = _cityRepository
                .Include(x => x.Province)
                .AsQueryable();

            var count = await query.CountAsync();
            if (count == 0) return ListResponseDto.Success();

            var cities = await query.OrderByDescending(x => x.Id)
                .Select(x => Mapper.CityMapper.MapToDto(x))
                .ToPagedQuery(parameter).AsNoTracking().ToListAsync();

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = count,
                Data = cities
            };
        }

        private async Task<ListResponseDto> GetAllPaging(string message, CityDto dto)
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
            //else if (dto.ReturnModel()) result.Data = new List<CityDto> { dto };

            return result;
        }

        public Task<ListResponseDto> Search(CityDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ListResponseDto> Add(CityDto dto)
        {
            //var validation = CheckValidate.Validator(new RoutineValidator(_sharedLocalizer), dto);
            //if (validation.Status == ResponseStatus.NotValid) return validation;

            var model = Mapper.CityMapper.MapToModel(dto);
            _cityRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();

            var message = _sharedLocalizer["CityForm.Response.AddCitySuccess"];//+
            return await GetAllPaging(message, Mapper.CityMapper.MapToDto(model));
        }

        public async Task<ListResponseDto> Edit(CityDto dto)
        {
            //var validation = CheckValidate.Validator(new RoutineValidator(_sharedLocalizer), dto);
            //if (validation.Status == ResponseStatus.NotValid) return validation;

            var city = await _cityRepository.Include(x => x.Province)
                .Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

            if (city is null) return ListResponseDto.Success(_sharedLocalizer["CityForm.Response.CityNotExist"]);

            Mapper.CityMapper.MapToModel(dto, city);
            _cityRepository.Update(city);
            await _unitOfWork.SaveChangesAsync();

            var message = _sharedLocalizer["CityForm.Response.EditCitySuccess"]; //+
            return await GetAllPaging(message, Mapper.CityMapper.MapToDto(city));
        }

        public async Task<ListResponseDto> Delete(BaseRequestPost<int> dto)
        {
            var city = await _cityRepository.Include(x => x.Province)
                .Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

            if (city is null) return ListResponseDto.Success(_sharedLocalizer["CityForm.Response.CityNotExist"]);

            _cityRepository.Remove(city);
            await _unitOfWork.SaveChangesAsync();

            var message = _sharedLocalizer["CityForm.Response.DeleteCitySuccess"]; //+
            return await GetAllPaging(message, AutoMapper.MapTo<CityDto>(dto));
        }
    }
}
