using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Internal;
using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.Entities.Models;
using static Portal.DTO.ConstProperty;


// ReSharper disable once CheckNamespace
namespace Portal.Application.Mapper
{
    public class PcrReportMapper
    {
        public static List<HighChartPieDonut> GroupByAgeRangeChart(List<PcrReportSp> pcrReportSp)
        {
            var ageRanges = new List<string> { "0-10", "10-20", "20-30", "30-40", "40-50", "50-60", "60-70", "70-80", "80-90", "90-100", "Other" };

            var reportData = pcrReportSp.GroupBy(x => x.AgeRange)
                .Select(x => new // grouping
                {
                    AgeRange = x.Key,
                    Count = x.Sum(y => y.Count),
                    Negative = x.Where(y => y.Result == TestResultProperty.Negative).Sum(y => y.Count),
                    Positive = x.Where(y => y.Result == TestResultProperty.Positive).Sum(y => y.Count)
                }).ToList();

            var chartPieDonuts = new List<HighChartPieDonut>();

            foreach (var ageRange in ageRanges)
            {
                var data = reportData.Where(x => x.AgeRange == ageRange).FirstOrDefault();

                if (data != null)
                {
                    chartPieDonuts.Add(new HighChartPieDonut
                    {
                        Y = data.Count,
                        Name = data.AgeRange,
                        DonutDrillDowns = new HighChartPieDonutDrillDown
                        {
                            Categories = new List<string> { TestResultProperty.Negative, TestResultProperty.Positive },
                            Data = new List<int> { data.Negative, data.Positive }
                        }
                    });
                }

                else
                {
                    chartPieDonuts.Add(new HighChartPieDonut
                    {
                        Y = 0,
                        Name = ageRange,
                        DonutDrillDowns = new HighChartPieDonutDrillDown { Categories = new List<string> { TestResultProperty.Negative, TestResultProperty.Positive }, Data = new List<int> { 0, 0 } }
                    });
                }
            }

            return chartPieDonuts;
        }

        public static string GroupByTestResult(List<PcrReportSp> pcrReportSp)
        {
            return
                pcrReportSp.Where(x => x.Result == Covid19TestResultProperty.Negative).Sum(x => x.Count).ToString("N0") + " / " +
                pcrReportSp.Where(x => x.Result == Covid19TestResultProperty.Positive).Sum(x => x.Count).ToString("N0");
        }

        public static List<HighChartPie> PieChartGroupByTestResultChart(List<PcrReportSp> pcrReportSp)
        {
            return new List<HighChartPie>
            {
                new HighChartPie
                {
                    Title = Covid19TestResultProperty.Negative,
                    Count = pcrReportSp.Where(x => x.Result == Covid19TestResultProperty.Negative).Sum(x => x.Count)
                },
                new HighChartPie
                {
                    Title = Covid19TestResultProperty.Positive,
                    Count = pcrReportSp.Where(x => x.Result == Covid19TestResultProperty.Positive).Sum(x => x.Count)
                }
            };
        }

        public static string GroupByPatientStatus(List<PcrReportSp> pcrReportSp, List<string> statuses)
        {
            var grouping = new List<string>();
            foreach (var status in statuses)
                grouping.Add(pcrReportSp.Where(x => x.PatientStatus.NormalizeForCompare() == status.NormalizeForCompare()).Select(x => x.Count).Sum().ToString("N0"));

            return string.Join(" | ", grouping);
        }

        public static List<HighChartPieDonut> GroupByPatientStatusChart(List<PcrReportSp> pcrReportSp, List<string> statuses)
        {
            var reportData = pcrReportSp.GroupBy(x => x.PatientStatus)
                .Select(x => new // grouping
                {
                    PatientStatus = x.Key,
                    Count = x.Sum(y => y.Count),
                    Negative = x.Where(y => y.Result == TestResultProperty.Negative).Sum(y => y.Count),
                    Positive = x.Where(y => y.Result == TestResultProperty.Positive).Sum(y => y.Count)
                }).ToList();

            var chartPieDonuts = new List<HighChartPieDonut>();

            foreach (var status in statuses)
            {
                var data = reportData.Where(x => x.PatientStatus == status).FirstOrDefault();

                if (data != null)
                {
                    chartPieDonuts.Add(new HighChartPieDonut
                    {
                        Y = data.Count,
                        Name = data.PatientStatus,
                        DonutDrillDowns = new HighChartPieDonutDrillDown
                        {
                            Categories = new List<string> { TestResultProperty.Negative, TestResultProperty.Positive },
                            Data = new List<int> { data.Negative, data.Positive }
                        }
                    });
                }

                else
                {
                    chartPieDonuts.Add(new HighChartPieDonut
                    {
                        Y = 0,
                        Name = status,
                        DonutDrillDowns = new HighChartPieDonutDrillDown { Categories = new List<string> { TestResultProperty.Negative, TestResultProperty.Positive }, Data = new List<int> { 0, 0 } }
                    });
                }
            }

            return chartPieDonuts;
        }

        public static HighChartBarDto GroupByRefferFromChart(List<PcrReportRefferFromSp> pcrReportRefferFromSp, List<string> reffersFrom, PcrReportDateParameterDto parameter)
        {
            var barSeries = new List<HighChartBarSeriesDto>
            {
                new HighChartBarSeriesDto{Name = Covid19TestResultProperty.Negative,Data = new List<int>()},
                new HighChartBarSeriesDto{Name = Covid19TestResultProperty.Positive,Data = new List<int>()},
            };
            foreach (var refferFrom in reffersFrom)
            {
                var data = pcrReportRefferFromSp.Where(x => x.RefferFrom.NormalizeForCompare() == refferFrom.NormalizeForCompare() && x.Result == Covid19TestResultProperty.Negative)
                    .Select(x => x.Count).DefaultIfEmpty(0).FirstOrDefault();
                barSeries[0].Data.Add(data);

                data = pcrReportRefferFromSp.Where(x => x.RefferFrom.NormalizeForCompare() == refferFrom.NormalizeForCompare() && x.Result == Covid19TestResultProperty.Positive)
                    .Select(x => x.Count).DefaultIfEmpty(0).FirstOrDefault();
                barSeries[1].Data.Add(data);
            }

            return new HighChartBarDto
            {
                Categories = reffersFrom,
                Series = barSeries
            };
        }

        public static HighChartBarDto BarChartGroupByTestResultChart(List<PcrReportTestResultSp> groupingReceptions, PcrReportDateParameterDto parameters)
        {
            if (!parameters.FromDate.HasValue || !parameters.ToDate.HasValue) return new HighChartBarDto();

            var barSeries = new List<HighChartBarSeriesDto>
            {
                new HighChartBarSeriesDto{Name = Covid19TestResultProperty.Negative,Data = new List<int>()},
                new HighChartBarSeriesDto{Name = Covid19TestResultProperty.Positive,Data = new List<int>()},
            };

            var days = parameters.FromDate.Value.CompareInDays(parameters.ToDate.Value);
            var dayCategories = new List<string>();
            for (var addDay = 0; addDay <= days; addDay++)
            {
                var day = parameters.FromDate.Value.AddDays(addDay);
                dayCategories.Add(day.ToDateString());

                var data = groupingReceptions.Where(x => x.AnswerDate.Date == day && x.Result.NormalizeForCompare() == Covid19TestResultProperty.Negative.NormalizeForCompare())
                    .FirstOrDefault()?.Count;
                barSeries[0].Data.Add(data.GetValueOrDefault());

                data = groupingReceptions.Where(x => x.AnswerDate.Date == day && x.Result.NormalizeForCompare() == Covid19TestResultProperty.Positive.NormalizeForCompare())
                    .FirstOrDefault()?.Count; ;
                barSeries[1].Data.Add(data.GetValueOrDefault());
            }

            return new HighChartBarDto
            {
                Categories = dayCategories,
                Series = barSeries
            };
        }
    }
}
