using Portal.DAL.Extensions;
using Portal.DTO.Covid;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Portal.Application.Mapper
{
    public class ReceptionCovidAirPortMapper
    {
        public static Receptions Map(ReceptionCovidAirPortDto receptionCovidAirPortDto, Patient curPatient)
        {
            Receptions receptions = new Receptions();
            Patient patient = new Patient();

            Person person = new Person();

            //چک میکنیم که ایا این بیمار دل یست موجد دیگه براش اطلاعات پرسنلی ثبت نمی کنیم و فقط اپدیت میزنیم

            if (curPatient != null)
            {
                patient = curPatient;
                person = curPatient.Person;
            }

            receptions.RelationId = receptionCovidAirPortDto.RelationId;
            receptions.Note = receptionCovidAirPortDto.Note;
            receptions.AnswerDate = receptionCovidAirPortDto.AnswerDate.TryToDateTime();

            person.FirstName = receptionCovidAirPortDto.FirstName;
            person.FatherName = receptionCovidAirPortDto.FatherName;
            person.GrandFatherName = receptionCovidAirPortDto.GrandFatherName;
            person.LastName = receptionCovidAirPortDto.LastName;
            person.Age = receptionCovidAirPortDto.Age;

            if (receptions.RelationId == 1)
                person.Mobile = receptionCovidAirPortDto.Mobile;

            person.Address = receptionCovidAirPortDto.Address;
            person.SexId = receptionCovidAirPortDto.SexId;
            person.Email = receptionCovidAirPortDto.Email;
            person.Phone = receptionCovidAirPortDto.Phone;
            person.NationalCode = receptionCovidAirPortDto.NationalCode;

            patient.Person = person;
            receptions.Patient = patient;

            return receptions;
        }

        public static Receptions Map(Receptions receptions,ReceptionCovidAirPortDto receptionCovidAirPortDto, Patient curPatient)
        {
            Patient patient = new Patient();

            Person person = new Person();

            //چک میکنیم که ایا این بیمار دل یست موجد دیگه براش اطلاعات پرسنلی ثبت نمی کنیم و فقط اپدیت میزنیم

            if (receptions.Patient != null)
            {
                patient = receptions.Patient;
                person = receptions.Patient.Person;
            }

            receptions.RelationId = receptionCovidAirPortDto.RelationId;
            receptions.Note = receptionCovidAirPortDto.Note;
            receptions.AnswerDate = receptionCovidAirPortDto.AnswerDate.TryToDateTime();

            person.FirstName = receptionCovidAirPortDto.FirstName;
            person.FatherName = receptionCovidAirPortDto.FatherName;
            person.GrandFatherName = receptionCovidAirPortDto.GrandFatherName;
            person.LastName = receptionCovidAirPortDto.LastName;
            person.Age = receptionCovidAirPortDto.Age;

            if (receptions.RelationId == 1)
                person.Mobile = receptionCovidAirPortDto.Mobile;

            person.Address = receptionCovidAirPortDto.Address;
            person.SexId = receptionCovidAirPortDto.SexId;
            person.Email = receptionCovidAirPortDto.Email;
            person.Phone = receptionCovidAirPortDto.Phone;
            person.NationalCode = receptionCovidAirPortDto.NationalCode;

            patient.Person = person;
            receptions.Patient = patient;

            return receptions;
        }

        public static Expression<Func<Receptions, ListCovidAirPortDto>> MapList

        {
            get
            {
                return x => new ListCovidAirPortDto
                {
                    Id=x.Id,
                    PersonId = x.Patient.Person != null ?  x.Patient.Person.Id :0,
                    Age= x.Patient.Person != null ? x.Patient.Person.Age :0,
                    FirstName = x.Patient.Person != null ? x.Patient.Person.FirstName :"",
                    LastName = x.Patient.Person != null ? x.Patient.Person.LastName :"",
                    FatherName = x.Patient.Person != null ? x.Patient.Person.FatherName :"",
                    GrandFatherName = x.Patient.Person != null ? x.Patient.Person.GrandFatherName :"",
                    Mobile =  x.Parent != null ? x.Parent.Mobile : (x.Patient.Person!=null ?  x.Patient.Person.Mobile :""),
                    Sex = x.Patient.Person.Sex !=null ? x.Patient.Person.Sex.Title :"",
                    ReceptionCode=x.SectionId.ToString() +"-"+ x.ReceptionId.ToString(),
                    ReceptionDate=x.ReceptionDate.ToDateTimeStringTry(),
                    AnswerDate=x.AnswerDate.ToDateTimeStringTry(),
                    RelationShip=x.Relation !=null ? x.Relation.Title:"",
                    Result=x.IsResult
                };
            }
        }

        public static ResultAnswerReportDto AnswerMapper(Receptions receptions)
        {
            ResultAnswerReportDto resultAnswerReport = new ResultAnswerReportDto();

            resultAnswerReport.Age = receptions?.Patient.Person.Age;
            resultAnswerReport.AnswerDate = receptions?.AnswerDate.ToDateTimeStringTry();
            resultAnswerReport.Email = receptions?.Patient?.Person?.Email;
            resultAnswerReport.Mobile =receptions.ParentId==null ? receptions?.Patient?.Person?.Mobile : receptions?.Parent?.Mobile;
            resultAnswerReport.FullName = receptions?.Patient?.Person?.FullNameThree;
            resultAnswerReport.ReceptionCode =receptions.SectionId +"-"+ receptions.ReceptionId.ToString();
            resultAnswerReport.Barcode = (receptions.SectionId.ToString() ?? "") + "-" + receptions.ReceptionId.ToString();
            resultAnswerReport.ReceptionDate = receptions?.ReceptionDate.ToDateTimeStringTry();
            resultAnswerReport.Sex = receptions?.Patient?.Person?.Sex?.Title;
            resultAnswerReport.Answers = new ResultAnswerItemReportDto();
            resultAnswerReport.Answers.FullName = "Covid 19";
            resultAnswerReport.Answers.Comment =receptions.ResultNote;
            resultAnswerReport.Answers.Result = receptions.IsResult != null ? (receptions.IsResult.Value == true ? "+": "-") :"";
            resultAnswerReport.CenterId = receptions?.Section?.CenterId;

            return  resultAnswerReport; 

        }

    }

}
