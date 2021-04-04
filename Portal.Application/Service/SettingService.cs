using His.Reception.Application.Interface;
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
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Service
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Setting> _settingRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public SettingService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _settingRepository = _unitOfWork.Set<Setting>();
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<BaseResponseDto> Update(RequestSettingDto requestSettingDtos)
        {
            var lstSetting =await _settingRepository.ToListAsync();

            foreach(var item in requestSettingDtos.Data)
            {
               var curSetting= lstSetting.Where(d => d.KeyName == item.Name).FirstOrDefault();

                if(curSetting != null)
                {
                    curSetting.KeyValue = item.Value;
                    _settingRepository.Update(curSetting);
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["SettingForm.Response.Success"]//+
            };
            
        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstSetting = await _settingRepository.Select(g=>Mapper.SettingMapper.Map(g)).ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lstSetting
            };
        }

        public async Task<BaseResponseDto> GetValue(string keyName)
        {
            var val =await _settingRepository.Where(d=>d.KeyName==keyName).FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data=val                
            };
        }

     
   

    }
}
