using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.Context;
using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Infrastructure.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Service
{
    public class ProvinceService:IProvinceService
    {
        private readonly DbSet<Province> _provinceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ProvinceService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _provinceRepository = unitOfWork.Set<Province>();

            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<BaseResponseDto> GetById(int id)
        {
            var city = await _provinceRepository.Include(x => x.Country)
                .Where(x => x.Id == id).Select(x => Mapper.ProvinceMapper.MapToDto(x))
                .AsNoTracking().FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = city
            };
        }

        public async Task<ListResponseDto> GetAll(IPaging parameter)
        {
            var query = _provinceRepository.Include(x => x.Country).AsQueryable();

            var count = await query.CountAsync();
            if (count == 0) return ListResponseDto.Success();

            var cities = await query.OrderByDescending(x=>x.Id)
                .Select(x => Mapper.ProvinceMapper.MapToDto(x))
                .ToPagedQuery(parameter).AsNoTracking().ToListAsync();

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = count,
                Data = cities
            };
        }

        private async Task<ListResponseDto> GetAllPaging(string message, ProvinceDto dto)
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

        public Task<ListResponseDto> Search(ProvinceDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ListResponseDto> Add(ProvinceDto dto)
        {
            //var validation = CheckValidate.Validator(new RoutineValidator(_sharedLocalizer), dto);
            //if (validation.Status == ResponseStatus.NotValid) return validation;

            var model = Mapper.ProvinceMapper.MapToModel(dto);
            _provinceRepository.Add(model);
            await _unitOfWork.SaveChangesAsync();

            var message = _sharedLocalizer["CityForm.Response.AddCitySuccess"];//+
            return await GetAllPaging(message, Mapper.ProvinceMapper.MapToDto(model));
        }

        public async Task<ListResponseDto> Edit(ProvinceDto dto)
        {
            //var validation = CheckValidate.Validator(new RoutineValidator(_sharedLocalizer), dto);
            //if (validation.Status == ResponseStatus.NotValid) return validation;

            var city = await _provinceRepository.Include(x => x.Country)
                .Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

            if (city is null) return ListResponseDto.Success(_sharedLocalizer["CityForm.Response.CityNotExist"]);

            Mapper.ProvinceMapper.MapToModel(dto, city);
            _provinceRepository.Update(city);
            await _unitOfWork.SaveChangesAsync();

            var message = _sharedLocalizer["CityForm.Response.EditCitySuccess"]; //+
            return await GetAllPaging(message, Mapper.ProvinceMapper.MapToDto(city));
        }

        public async Task<ListResponseDto> Delete(BaseRequestPost<int> dto)
        {
            var city = await _provinceRepository.Include(x => x.Country)
                .Where(x => x.Id == dto.Id).FirstOrDefaultAsync();

            if (city is null) return ListResponseDto.Success(_sharedLocalizer["CityForm.Response.CityNotExist"]);

            _provinceRepository.Remove(city);
            await _unitOfWork.SaveChangesAsync();

            var message = _sharedLocalizer["CityForm.Response.DeleteCitySuccess"]; //+
            return await GetAllPaging(message, AutoMapper.MapTo<ProvinceDto>(dto));
        }

    }
}
