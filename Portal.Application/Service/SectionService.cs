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
    public class SectionService : ISectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Section> _sectionRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public SectionService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sectionRepository = _unitOfWork.Set<Section>();
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<BaseResponseDto> GetSectionByCode(string code)
        {
            var cur = await _sectionRepository.Where(d => d.LocalCode == code).FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = cur
            };
        }
    }
}
