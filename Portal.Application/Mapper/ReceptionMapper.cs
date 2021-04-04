using Microsoft.EntityFrameworkCore.Internal;
using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.DTO.Covid;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Portal.Application.Mapper
{
    public class ReceptionMapper
    {
        public static Receptions Map(ReceptionLabDto receptionLabDto, int userId, int sectionId, int serviceId)
        {
            var receptions = new Receptions();
            var receptionService = new ReceptionService();
            var patient = new Patient();
            var person = new Person();
            var receptionHistory = new ReceptionHistory();
            var patientExtraInfo = new PatientExtraInfo();

            receptionHistory.CreateDate = DateTime.Now;
            receptionHistory.SectionId = sectionId;
            receptionHistory.IsCurrent = true;
            receptionHistory.UserId = userId;

            patientExtraInfo.NationalityId = receptionLabDto.NationalityId;
            patientExtraInfo.PassportIssueDate = receptionLabDto.PassportIssuedDate.TryToDateTime();
            patientExtraInfo.PassportNumber = receptionLabDto.PassportNumber;

            receptions.ReceptionDate = receptionLabDto.ReceptionDate.TryToDateTime();
            receptions.Note = receptionLabDto.Title;
            receptions.SectionId = sectionId;

            var ageString = (DateTime.Now.Year - receptionLabDto.BrithDate.TryToDateTime().GetValueOrDefault().Year).TryToString();
            person.Age =short.Parse(ageString!=""?ageString:"0");

            person.FirstName = receptionLabDto.FirstName;
            person.LastName = receptionLabDto.LastName;
            person.Mobile = receptionLabDto.Mobile;
            person.LatinName = receptionLabDto.LatinName;
            person.GrandFatherName = receptionLabDto.GrandFatherName;
            person.Email = receptionLabDto.Email;
            person.BirthDate = receptionLabDto.BrithDate.TryToDateTime();
            person.SexId = receptionLabDto.SexId;
            person.FatherName = receptionLabDto.FatherName;
            person.Address = receptionLabDto.Address;

            patient.FileNo = receptionLabDto.FileNo;
            patient.Person = person;
            patient.CreateDate = DateTime.Now;
            receptions.Patient = patient;

            receptionService.CreateDate = DateTime.Now;
            receptionService.ServiceId = serviceId;
            receptionService.StatusId = (int)ReceptionServiceStatusEnum.WaitForAnswer;

            receptions.ReceptionService.Add(receptionService);
            patient.PatientExtraInfo.Add(patientExtraInfo);
            receptions.ReceptionHistory.Add(receptionHistory);

            return receptions;
        }

        public static ReceptionLabDto Map(Receptions receptions)
        {
            var receptionLabDto = new ReceptionLabDto();
            receptionLabDto.Id = receptions.Id;

            receptionLabDto.NationalityId= receptions.Patient?.PatientExtraInfo?.Select(g=>g.NationalityId).FirstOrDefault();
            receptionLabDto.PassportIssuedDate = receptions.Patient?.PatientExtraInfo?.Select(g => g.PassportIssueDate.ToDateStringTry()).FirstOrDefault();
            receptionLabDto.PassportNumber = receptions.Patient?.PatientExtraInfo?.Select(g => g.PassportNumber).FirstOrDefault();

            receptionLabDto.ReceptionDate = receptions.ReceptionDate.ToDateTimeStringTry();
            receptionLabDto.Title = receptions.Note;

            receptionLabDto.Age=receptions.Patient?.Person?.Age;
            receptionLabDto.FirstName = receptions.Patient?.Person?.FirstName;
            receptionLabDto.LastName = receptions.Patient?.Person?.LastName;
            receptionLabDto.Mobile = receptions.Patient?.Person?.Mobile;
            receptionLabDto.LatinName = receptions.Patient?.Person?.LatinName;
            receptionLabDto.GrandFatherName = receptions.Patient?.Person?.GrandFatherName;
            receptionLabDto.FatherName = receptions.Patient?.Person?.FatherName;
            receptionLabDto.Email = receptions.Patient?.Person?.Email;
            receptionLabDto.BrithDate = receptions.Patient?.Person?.BirthDate.ToDateTimeStringTry();
            receptionLabDto.SexId = receptions.Patient?.Person?.SexId;
            receptionLabDto.FileNo = receptions.Patient?.FileNo;
            receptionLabDto.Address = receptions.Patient?.Person?.Address;

            return receptionLabDto;
        }

        public static Receptions Map(Receptions receptions,ReceptionLabDto receptionLabDto, int userId, int sectionId, int serviceId)
        {
            var patient = receptions.Patient;
            var person = patient.Person;
            var patientExtraInfo = patient.PatientExtraInfo.FirstOrDefault();


            if (patientExtraInfo != null)
            {
                patientExtraInfo.NationalityId = receptionLabDto.NationalityId;
                patientExtraInfo.PassportIssueDate = receptionLabDto.PassportIssuedDate.TryToDateTime();
                patientExtraInfo.PassportNumber = receptionLabDto.PassportNumber;
            }

            receptions.ReceptionDate = receptionLabDto.ReceptionDate.TryToDateTime();
            receptions.Note = receptionLabDto.Title;

            var ageString = (DateTime.Now.Year - receptionLabDto.BrithDate.TryToDateTime().GetValueOrDefault().Year).TryToString();
            person.Age = short.Parse(ageString != "" ? ageString : "0");
            
            person.FirstName = receptionLabDto.FirstName;
            person.LastName = receptionLabDto.LastName;
            person.Mobile = receptionLabDto.Mobile;
            person.LatinName = receptionLabDto.LatinName;
            person.GrandFatherName = receptionLabDto.GrandFatherName;
            person.Email = receptionLabDto.Email;
            person.BirthDate = receptionLabDto.BrithDate.TryToDateTime();
            person.SexId = receptionLabDto.SexId;
            person.FatherName = receptionLabDto.FatherName;
            person.Address = receptionLabDto.Address;

            patient.FileNo = receptionLabDto.FileNo;
            patient.Person = person;
            receptions.Patient = patient;

            return receptions;
        }

        public static ListReceptionLabDto Map(SearchReceptionSp reception)
        {
            return new ListReceptionLabDto
                {
                    Id = reception.Id,
                    FullName = reception.FullName,
                    FileNo = reception.FileNo,
                    AnswerDate = reception.AnswerDate.ToDateTimeStringTry(),
                    ReceptionDate = reception.ReceptionDate.ToDateTimeStringTry(),
                    ReceptionCode = reception.ReceptionCode,
                    Mobile = reception.Mobile,
                    LatinName = reception.LatinName,
                    Title = reception.Title,
                    Nationality = reception.Nationality,
                    LatinNationality = reception.LatinNationality,
                    Status = reception.Status,
                    StatusCode = reception.StatusCode,
                    AnswerUser = reception.AnswerUser,
                    RegisterUser = reception.RegisterUser,
                    PassportIssuedDate = reception.PassportIssueDate.TryToDateString(),
                    PassportNumber = reception.PassportNumber,
                    ReceptionServiceId = reception.ReceptionServiceId,
                    Result = reception.Result
                };
        }

        public static Expression<Func<Receptions, ListReceptionLabDto>> MapList
        {
            get
            {
                return x => new ListReceptionLabDto
                {
                    Id = x.Id,
                    FullName = x.Patient.Person != null ? x.Patient.Person.FullName : "",
                    FileNo = x.Patient.FileNo,
                    AnswerDate = x.AnswerDate.ToDateTimeStringTry(),
                    ReceptionDate = x.ReceptionDate.ToDateTimeStringTry(),
                    ReceptionCode = x.ReceptionId,
                    Mobile = x.Patient.Person != null ? x.Patient.Person.Mobile : "",
                    LatinName = x.Patient.Person != null ? x.Patient.Person.LatinName : "",
                    Title = x.Note,
                    Nationality = x.Patient.PatientExtraInfo.Any() ? x.Patient.PatientExtraInfo.Select(g => g.Nationality.TitleLang2).FirstOrDefault() : "",
                    LatinNationality = x.Patient.PatientExtraInfo.Any() ? x.Patient.PatientExtraInfo.Select(g => g.Nationality.Title).FirstOrDefault() : "",
                    Status = x.ReceptionService.Any() ? x.ReceptionService.Select(g => g.Status.Title).FirstOrDefault() : "",
                    StatusCode = x.ReceptionService.Any() ? x.ReceptionService.Select(g => g.Status.Code2).FirstOrDefault() : "",
                    AnswerUser = x.ReceptionService.Any() ? x.ReceptionService.Select(g => g.AnswerUser != null ? g.AnswerUser.Person.FullName : "").FirstOrDefault() : "",
                    RegisterUser = x.ReceptionHistory.Any() ? x.ReceptionHistory.Select(g => g.User != null ? g.User.Person.FullName : "").FirstOrDefault() : "",
                    PassportIssuedDate = x.Patient.PatientExtraInfo.Any() ? x.Patient.PatientExtraInfo.Select(g => g.PassportIssueDate.ToDateTimeStringTry()).FirstOrDefault() : "",
                    PassportNumber = x.Patient.PatientExtraInfo.Any() ? x.Patient.PatientExtraInfo.Select(g => g.PassportNumber).FirstOrDefault() : "",
                    ReceptionServiceId = x.ReceptionService.Any() ? x.ReceptionService.Select(g => (long?)g.Id).FirstOrDefault() : null,
                    Result = x.ReceptionService.Any() ? x.ReceptionService.Select(g => g.Answer1.Any() ? g.Answer1.Select(t => t.Result).FirstOrDefault() : "قيد الأنتظار ").FirstOrDefault() : "قيد الأنتظار ",
                };
            }
        }

        public static Expression<Func<Receptions, LabCovidReportDto>> MapLabReport
        {
            get
            {
                return x => new LabCovidReportDto
                {
                    BrithDate = x.Patient.Person != null ? x.Patient.Person.BirthDate.ToDateTimeStringTry() :"",
                    FullName = x.Patient.Person != null ? x.Patient.Person.FullName :"",
                    LatinSex = x.Patient.Person.Sex != null ? x.Patient.Person.Sex.Title : "",
                    Age = x.Patient.Person.Age.GetValueOrDefault(),
                    Sex= x.Patient.Person.Sex != null ? x.Patient.Person.Sex.TitleLang2 : "",
                    FileNo=x.Patient.FileNo,
                    Nationality = x.Patient.PatientExtraInfo.Any() ? x.Patient.PatientExtraInfo.Select(g => g.Nationality.TitleLang2).FirstOrDefault() : "",
                    LatinNationality = x.Patient.PatientExtraInfo.Any() ? x.Patient.PatientExtraInfo.Select(g => g.Nationality.Title).FirstOrDefault() : "",
                    PassportIssuedDate = x.Patient.PatientExtraInfo.Any() ? x.Patient.PatientExtraInfo.Select(g => g.PassportIssueDate.ToDateTimeStringTry()).FirstOrDefault() : "",
                    PassportNumber = x.Patient.PatientExtraInfo.Any() ? x.Patient.PatientExtraInfo.Select(g => g.PassportNumber).FirstOrDefault() : "",
                    ReceptionCode=x.ReceptionId,
                    ReceptionDate=x.ReceptionDate.ToDateTimeStringTry(),
                    Result = x.ReceptionService.Any() ? x.ReceptionService.Select(g => g.Answer1.Any() ? g.Answer1.Select(t => t.Result).FirstOrDefault() : "قيد الأنتظار ").FirstOrDefault() : "قيد الأنتظار ",
                    LatinName= x.Patient.Person != null ? x.Patient.Person.LatinName : "",
                };

            }
        }
    }
}
