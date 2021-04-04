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
    public class ServiceService : IServiceService
    {
        private readonly DbSet<Services> _serviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public ServiceService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _serviceRepository = unitOfWork.Set<Services>();

            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<BaseResponseDto> ByParentLocalCode(string localCode)
        {
            var lst = await _serviceRepository.Where(d => d.Parent.LocalCode == localCode).ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lst
            };

        }
    }
}
