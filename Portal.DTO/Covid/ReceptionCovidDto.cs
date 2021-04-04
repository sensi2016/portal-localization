using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Covid
{
    public class ReceptionCovidDto
    {
        public long Id { get; set; }
        public int? RelationId { get; set; }
       // public string RelationTitle { get; set; }
        public string Mobile { get; set; }
        public short? Age { get; set; }
        public string FirstName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public bool? IsPregnant { get; set; }
        public int? PregnancySeasonId { get; set; }
        public int? CityId { get; set; }
        public int? SexId { get; set; }
        public int? ProvinceId { get; set; }
        public int? ZoneId { get; set; }
        public string Address { get; set; }
        public int? JobId { get; set; }
        public int? JobTypeId { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public bool? IsHaveSign { get; set; }
        public string ConsumeDrug { get; set; }

        public DateTime? DateOfSign { get; set; }
        public DateTime? HospitalEnteryDate { get; set; }
        public DateTime? RecoveryDate { get; set; }
        public List<int> Signs { get; set; }
        public List<int> SpecialIllnesses { get; set; }
        public List<RequestReceptionAnswerDto> RequestReceptionAnswers { get; set; }
        public int? BloodGroupId { get; set; }
        // public int? JobTypeId { get; set; }
    }

    public class AnswerLabDto
    {
        public int ReceptionServiceId { get; set; }
        public string Result { get; set; }
        public string AnswerDate { get; set; }
        public string Note { get; set; }
    }
        public class ReceptionLabDto
    {
        public long Id { get; set; }
        // public string RelationTitle { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public short? Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public int? NationalityId { get; set; }
        public string GrandFatherName { get; set; }
        public string LatinName { get; set; }
        public string Title { get; set; }
        public int? SexId { get; set; }
        public string FileNo { get; set; }
        public string Address { get; set; }
        public string BrithDate { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssuedDate { get; set; }
        public string ReceptionDate { get; set; }
     
    }

    public class FilterReceptionLabDto:IPaging
    {       
        public string Mobile { get; set; }
        public string Email { get; set; }
        public short? Age { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public List<int> NationalityIds { get; set; }
        public List<int> RegisterUserIds { get; set; }
        public List<int> AnswerUserIds { get; set; }
        public string GrandFatherName { get; set; }
        public string LatinName { get; set; }
        public string Title { get; set; }
        public int? SexId { get; set; }
        public int? StatusId { get; set; }
        public long? ReceptionCode { get; set; }
        public string FileNo { get; set; }
        public string Address { get; set; }
        public string BrithDate { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssuedDate { get; set; }
        public string AnswerDate { get; set; }
        public string ReceptionFromDate { get; set; }
        public string ReceptionToDate { get; set; }
        public string Result { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class ListReceptionLabDto
    {
        public long Id { get; set; }
        // public string RelationTitle { get; set; }
        public string Mobile { get; set; }
        public string FullName { get; set; }
        public string LatinName { get; set; }
        public string Title { get; set; }
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public long? ReceptionCode { get; set; }
        public long? ReceptionServiceId { get; set; }
        public string FileNo { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssuedDate { get; set; }
        public string AnswerDate { get; set; }
        public string ReceptionDate { get; set; }
        public string RegisterUser { get; set; }
        public string AnswerUser { get; set; }
        public string Nationality { get; set; }
        public string LatinNationality { get; set; }
        public string Result { get; set; }
    }

    public class LabCovidReportDto
    {
        public string FullName { get; set; }
        public string LatinName { get; set; }
        public string Nationality { get; set; }
        public string LatinNationality { get; set; }
        public string BrithDate { get; set; }
        public string Result { get; set; }
        public string PassportNumber { get; set; }
        public string PassportIssuedDate { get; set; }
        public string ReceptionDate { get; set; }
        public long? ReceptionCode { get; set; }
        public string FileNo { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string LatinSex { get; set; }

    }
    public class FilterReceptionCovidDto : IPaging
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }


    public class ListReceptionCovidDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Sex { get; set; }
        public short? Age { get; set; }
        public bool IsPregnant { get; set; }
        public bool? IsResult { get; set; }

    }

    public class SetAnswerCovidDto
    {
        public long ReceptionId { get; set; }
        public bool? IsResult { get; set; }
        public string ResultNote { get; set; }
    }

    public class BaseInfoSetAnswerCovidDto
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string RelationShip  { get; set; }
        public string SexAndAge  { get; set; }
        public int? PersonId { get; set; }
        public string ResultNote  { get; set; }
        public string FileId  { get; set; }
        public long? ReceptionCode  { get; set; }
        public string LatinName  { get; set; }
        public bool? IsResult  { get; set; }
    }

   
}

