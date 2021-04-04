using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.Context;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Application.Service
{
    public class DashboardService : IDashboardService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly DbSet<Receptions> _receptionsRepository;
        private readonly DbSet<Patient> _patientsRepository;

        public DashboardService(IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _receptionsRepository = _unitOfWork.Set<Receptions>();
            _patientsRepository = _unitOfWork.Set<Patient>();
        }
        public async Task<BaseResponseDto> Get()
        {            
            DashboardDto dashboardDto = new DashboardDto();

            dashboardDto.TotalDay= await _receptionsRepository.Where(d => d.ReceptionDate.Value.Date == DateTime.Now.Date).CountAsync();
            dashboardDto.TotalMonth = await _receptionsRepository.Where(d => d.ReceptionDate.Value.Date.Month == DateTime.Now.Date.Month).CountAsync();
            dashboardDto.TotalYear = await _receptionsRepository.Where(d => d.ReceptionDate.Value.Date.Year == DateTime.Now.Date.Year).CountAsync();


            dashboardDto.TotalPositive = await _receptionsRepository.Where(d => d.IsResult.Value).CountAsync();
            dashboardDto.TotalNegative = await _receptionsRepository.Where(d => !d.IsResult.Value && d.IsResult!=null).CountAsync();
            dashboardDto.TotalNotAnswered = await _receptionsRepository.Where(d => d.IsResult==null).CountAsync();
            //SqlFunctions.

            dashboardDto.Chart = await _receptionsRepository.Where(d => d.ReceptionDate != null).GroupBy(d =>d.ReceptionDate.Value.Month ).Select(g => new DashboardChartDto
            {
                Title = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(g.Key),
                TotalMen =_receptionsRepository.Where(d=>d.ReceptionDate.Value.Month==g.Key && d.Patient.Person.SexId==1).Count(),
                TotalWomen =_receptionsRepository.Where(d=>d.ReceptionDate.Value.Month==g.Key && d.Patient.Person.SexId==2).Count(),
                TotalMenPostive =_receptionsRepository.Where(d=>d.ReceptionDate.Value.Month==g.Key && d.Patient.Person.SexId==1 && d.IsResult.Value).Count(),
                TotalMenNegative =_receptionsRepository.Where(d=>d.ReceptionDate.Value.Month==g.Key && d.Patient.Person.SexId==1 && !d.IsResult.Value).Count(),
                TotalWomenPostive =_receptionsRepository.Where(d=>d.ReceptionDate.Value.Month==g.Key && d.Patient.Person.SexId==2 && d.IsResult.Value).Count(),
                TotalWomenNegative = _receptionsRepository.Where(d=>d.ReceptionDate.Value.Month==g.Key && d.Patient.Person.SexId==2 && !d.IsResult.Value).Count(),
                
            }).ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = dashboardDto
            };
        }
    }
}
