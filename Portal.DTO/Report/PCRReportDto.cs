using System;
using System.Collections.Generic;
using System.Text;
using Portal.DTO.Message;

// ReSharper disable once CheckNamespace
namespace Portal.DTO
{
    public class PcrReportSp
    {
        public long Id { get; set; }
        public string AgeRange { get; set; }
        public string PatientStatus { get; set; }
        public bool? Today { get; set; }
        public bool? ThisMonth { get; set; }
        public bool? ThisYear { get; set; }
        public string Result { get; set; }
        public int Count { get; set; }
    }

    public class PcrReportRefferFromSp
    {
        public long Id { get; set; }
        public string RefferFrom { get; set; }
        public string Result { get; set; }
        public int Count { get; set; }
    }


    public class PcrReportTestResultSp
    {
        public long Id { get; set; }
        public DateTime AnswerDate { get; set; }
        public string Result { get; set; }
        public int Count { get; set; }
    }

    public class PcrReportParameterDto
    {
        public DateTime ReportDate { get; set; }
        public bool IsPrint { get; set; }
    }

    public class PcrReportDateParameterDto
    {
        public bool IsPrint { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class PcrReportDto
    {
        public PcrReportTotalByDate TotalPatient { get; set; }
        public PcrReportTotalChartPieDonutByDate TotalPatientChartByAge { get; set; }

        public PcrReportTotalByDate TotalPatientByTestResult { get; set; }
        public PcrReportTotalChartPieByDate TotalPatientChartByTestResult { get; set; }

        public PcrReportTotalByDate TotalPatientByStatus { get; set; }
        public PcrReportTotalChartPieDonutByDate TotalPatientChartByStatus { get; set; }
    }

    public class PcrReportReceptionDto
    {
        public long ReceptionServiceId { get; set; }
        public DateTime AnswerDate { get; set; }
        public int PatientId { get; set; }
        public int Age { get; set; }
        public string PatientStatusTitle { get; set; }
        public string Covid19TestResult { get; set; }
        public string RefferFromTitle { get; set; }
    }

    public class PcrReportReceptionMapLight
    {
        public PcrReportReceptionService ReceptionService { get; set; }
        public DateTime AnswerDate { get; set; }
        public int PatientId { get; set; }
        public int Age { get; set; }
        public string PatientStatusTitle { get; set; }
        public string RefferFromTitle { get; set; }
    }

    public class PcrReportReceptionService
    {
        public long ReceptionServiceId { get; set; }
        public string TestResult { get; set; }

    }


    public class PcrReportReceptionByDateDto
    {
        public List<PcrReportReceptionDto> InDay { get; set; }
        public List<PcrReportReceptionDto> InWeek { get; set; }
        public List<PcrReportReceptionDto> InMonth { get; set; }
        public List<PcrReportReceptionDto> InYear { get; set; }
        public List<PcrReportReceptionDto> Total { get; set; }
    }

    public class PcrReportTotalByDate
    {
        public string TotalInDay { get; set; }
        public string TotalInMonth { get; set; }
        public string TotalInYear { get; set; }
        public string Total { get; set; }
    }

    public class PcrReportTotalChartPieDonutByDate
    {
        public List<HighChartPieDonut> TotalInDay { get; set; }
        public List<HighChartPieDonut> TotalInMonth { get; set; }
        public List<HighChartPieDonut> TotalInYear { get; set; }
        public List<HighChartPieDonut> Total { get; set; }
    }

    public class PcrReportTotalChartPieByDate
    {
        public List<HighChartPie> TotalInDay { get; set; }
        public List<HighChartPie> TotalInMonth { get; set; }
        public List<HighChartPie> TotalInYear { get; set; }
        public List<HighChartPie> Total { get; set; }
    }

    public class PcrReportTotalChartBarByDate
    {
        public HighChartBarDto TotalInDay { get; set; }
        public HighChartBarDto TotalInMonth { get; set; }
        public HighChartBarDto TotalInYear { get; set; }
        public HighChartBarDto Total { get; set; }
    }

    public class GroupByTitleDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
    }

    public class GroupByAgeRange
    {
        public int AgeGroupCount { get; set; }
        public string AgeGroup { get; set; }
        public int Negative { get; set; }
        public int Positive { get; set; }
    }

    public class GroupByTitleAndTestResult
    {
        public string Title { get; set; }
        public int Negative { get; set; }
        public int Positive { get; set; }
        public int Count { get; set; }
    }

    public class HighChartPieDonut
    {
        public int Y { get; set; }
        public HighChartPieDonutDrillDown DonutDrillDowns { get; set; }
        public string Name { get; set; }
    }

    public class HighChartPie
    {
        public string Title { get; set; }
        public int Count { get; set; }
    }

    public class HighChartPieDonutDrillDown
    {
        public List<string> Categories { get; set; }
        public List<int> Data { get; set; }
    }

    public class HighChartBarDto
    {
        public List<string> Categories { get; set; }
        public List<HighChartBarSeriesDto> Series { get; set; }
    }

    public class HighChartBarSeriesDto
    {
        public string Name { get; set; }
        public List<int> Data { get; set; }
    }

    public class SearchReceptionSp
    {
        public long Id { get; set; }
        public DateTime? AnswerDate { get; set; }
        public long? ReceptionCode { get; set; }
        public string Title { get; set; }
        public DateTime? ReceptionDate { get; set; }
        public string FileNo { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string LatinName { get; set; }
        public string Nationality { get; set; }
        public int? NationalityId { get; set; }
        public string LatinNationality { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportIssueDate { get; set; }
        public int? RegisterUserId { get; set; }
        public string RegisterUser { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string AnswerUser { get; set; }
        public int? AnswerUserId { get; set; }
        public long? ReceptionServiceId { get; set; }
        public string Result { get; set; }
        public int CountAll { get; set; }
    }
}
