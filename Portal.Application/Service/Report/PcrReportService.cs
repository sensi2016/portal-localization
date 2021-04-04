using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.Application.Mapper;
using Portal.Context;
using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Interface;

// ReSharper disable once CheckNamespace
namespace Portal.Application.Service
{
    public class PcrReportService : IPcrReportService
    {
        private readonly DbSet<RefferFrom> _refferFromRepository;
        private readonly DbSet<GeneralStatus> _generalStatusRepository;
        private readonly IWorkContextService _workContextService;
        private readonly IUnitOfWork _unitOfWork;

        public PcrReportService(IWorkContextService workContextService,IUnitOfWork unitOfWork)
        {
            _workContextService = workContextService;
            _unitOfWork = unitOfWork;
            _refferFromRepository = unitOfWork.Set<RefferFrom>();
            _generalStatusRepository = unitOfWork.Set<GeneralStatus>();
        }

        public async Task<BaseResponseDto> PcrReport(PcrReportParameterDto parameters)
        {
            var reportData = new PcrReportDto();
            var generalStatuses = await _generalStatusRepository.Select(x => x.TitleLang2).ToListAsync();

            var sectionId = _workContextService.SectionId;

            var pcrReportSp = await _unitOfWork.Set<PcrReportSp>()
                .FromSqlInterpolated($"[dbo].[PcrReport] @reportDate={parameters.ReportDate.Date},@sectionId={sectionId}")
                .ToListAsync();

            var pcrReportToday = pcrReportSp.Where(x => x.Today == true).ToList();
            var pcrReportThisMonth = pcrReportSp.Where(x => x.ThisMonth == true).ToList();
            var pcrReportThisYear = pcrReportSp.Where(x => x.ThisYear == true).ToList();

            // total patient
            reportData.TotalPatient = new PcrReportTotalByDate
            {
                TotalInDay = pcrReportToday.Sum(x => x.Count).ToString("N0"),
                TotalInMonth = pcrReportThisMonth.Sum(x => x.Count).ToString("N0"),
                TotalInYear = pcrReportThisYear.Sum(x => x.Count).ToString("N0"),
                Total = pcrReportSp.Sum(x => x.Count).ToString("N0"),
            };

            // total patient chart by age
            reportData.TotalPatientChartByAge = new PcrReportTotalChartPieDonutByDate
            {
                TotalInDay = PcrReportMapper.GroupByAgeRangeChart(pcrReportToday),
                TotalInMonth = PcrReportMapper.GroupByAgeRangeChart(pcrReportThisMonth),
                TotalInYear = PcrReportMapper.GroupByAgeRangeChart(pcrReportThisYear),
                Total = PcrReportMapper.GroupByAgeRangeChart(pcrReportSp),
            };

            // total patient by test result
            reportData.TotalPatientByTestResult = new PcrReportTotalByDate
            {
                TotalInDay = PcrReportMapper.GroupByTestResult(pcrReportToday),
                TotalInMonth = PcrReportMapper.GroupByTestResult(pcrReportThisMonth),
                TotalInYear = PcrReportMapper.GroupByTestResult(pcrReportThisYear),
                Total = PcrReportMapper.GroupByTestResult(pcrReportSp)
            };

            // total patient chart by test result
            reportData.TotalPatientChartByTestResult = new PcrReportTotalChartPieByDate
            {
                TotalInDay = PcrReportMapper.PieChartGroupByTestResultChart(pcrReportToday),
                TotalInMonth = PcrReportMapper.PieChartGroupByTestResultChart(pcrReportThisMonth),
                TotalInYear = PcrReportMapper.PieChartGroupByTestResultChart(pcrReportThisYear),
                Total = PcrReportMapper.PieChartGroupByTestResultChart(pcrReportSp),
            };

            // total patient by patient status
            reportData.TotalPatientByStatus = new PcrReportTotalByDate
            {
                TotalInDay = PcrReportMapper.GroupByPatientStatus(pcrReportToday, generalStatuses),
                TotalInMonth = PcrReportMapper.GroupByPatientStatus(pcrReportThisMonth, generalStatuses),
                TotalInYear = PcrReportMapper.GroupByPatientStatus(pcrReportThisYear, generalStatuses),
                Total = PcrReportMapper.GroupByPatientStatus(pcrReportSp, generalStatuses)
            };

            // total patient chart by status
            reportData.TotalPatientChartByStatus = new PcrReportTotalChartPieDonutByDate
            {
                TotalInDay = PcrReportMapper.GroupByPatientStatusChart(pcrReportToday, generalStatuses),
                TotalInMonth = PcrReportMapper.GroupByPatientStatusChart(pcrReportThisMonth, generalStatuses),
                TotalInYear = PcrReportMapper.GroupByPatientStatusChart(pcrReportThisYear, generalStatuses),
                Total = PcrReportMapper.GroupByPatientStatusChart(pcrReportSp, generalStatuses),
            };

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = reportData
            };
        }

        public async Task<BaseResponseDto> PcrRefferFromReport(PcrReportDateParameterDto parameters)
        {
            var reffersFrom = await _refferFromRepository.Select(x => x.TitleLang2).ToListAsync();

            if (!parameters.FromDate.IsValidDateTime() || !parameters.FromDate.IsValidDateTime())
            { parameters.FromDate = DateTime.Now.Date; parameters.ToDate = DateTime.Now.AddDays(6).Date; }

            var sectionId = _workContextService.SectionId;

            var groupingReceptions = await _unitOfWork.Set<PcrReportRefferFromSp>()
                .FromSqlInterpolated($"[dbo].[PcrReportRefferFrom] @fromDate={parameters.FromDate.GetValueOrDefault().Date},@toDate={parameters.ToDate.GetValueOrDefault().Date},@sectionId={sectionId}")
                .ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = PcrReportMapper.GroupByRefferFromChart(groupingReceptions, reffersFrom, parameters)
            };
        }

        public async Task<BaseResponseDto> PcrTestResultReport(PcrReportDateParameterDto parameters)
        {
            if (!parameters.FromDate.IsValidDateTime() || !parameters.FromDate.IsValidDateTime())
            { parameters.FromDate = DateTime.Now.Date; parameters.ToDate = DateTime.Now.AddDays(6).Date; }

            var sectionId = _workContextService.SectionId;

            var groupingReceptions = await _unitOfWork.Set<PcrReportTestResultSp>()
                .FromSqlInterpolated($"[dbo].[PcrReportTestResultByDate] @fromDate={parameters.FromDate.GetValueOrDefault().Date},@toDate={parameters.ToDate.GetValueOrDefault().Date},@sectionId={sectionId}")
                .ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = PcrReportMapper.BarChartGroupByTestResultChart(groupingReceptions, parameters)
            };
        }
    }
}
