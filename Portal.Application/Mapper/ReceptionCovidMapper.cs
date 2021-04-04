using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.DTO.Covid;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Portal.Application.Mapper
{
    public class ReceptionCovidMapper
    {
        public static Receptions Map(ReceptionCovidDto receptionCovidDto, Patient curPatient)
        {
            Receptions receptions = new Receptions();
            Patient patient = new Patient();
            PatientExtraInfo patientExtraInfo = new PatientExtraInfo();
            Person person = new Person();

            //چک میکنیم که ایا این بیمار دل یست موجد دیگه براش اطلاعات پرسنلی ثبت نمی کنیم و فقط اپدیت میزنیم

            if (curPatient != null)
            {
                patient = curPatient;
                person = curPatient.Person;

                if (curPatient.PatientExtraInfo != null && curPatient.PatientExtraInfo.Count > 0)
                    patientExtraInfo = curPatient.PatientExtraInfo.FirstOrDefault();

            }

            receptions.IsHaveSign = receptionCovidDto.IsHaveSign;
            receptions.RelationId = receptionCovidDto.RelationId;
            receptions.RecoveryDate = receptionCovidDto.RecoveryDate;
            receptions.ConsumeDrug = receptionCovidDto.ConsumeDrug;
            receptions.DateOfSign = receptionCovidDto.DateOfSign;
            receptions.HospitalEnteryDate = receptionCovidDto.HospitalEnteryDate;

            person.FirstName = receptionCovidDto.FirstName;
            person.FatherName = receptionCovidDto.FatherName;
            person.GrandFatherName = receptionCovidDto.GrandFatherName;
            person.Age = receptionCovidDto.Age;
            if (receptions.RelationId == (int)RelationShipEnum.MySelf)
                person.Mobile = receptionCovidDto.Mobile;
            person.ProvinceId = receptionCovidDto.ProvinceId;
            person.CityId = receptionCovidDto.CityId;
            person.ZoneId = receptionCovidDto.ZoneId;
            person.Address = receptionCovidDto.Address;
            person.SexId = receptionCovidDto.SexId;

            patientExtraInfo.IsPregnant = receptionCovidDto.IsPregnant;
            patientExtraInfo.PregnancySeasonId = receptionCovidDto.PregnancySeasonId;
            patientExtraInfo.Weight = receptionCovidDto.Weight;
            patientExtraInfo.Height = receptionCovidDto.Height;
            patientExtraInfo.BloodGroupId = receptionCovidDto.BloodGroupId;
            patientExtraInfo.JobId = receptionCovidDto.JobId;
            patientExtraInfo.JobTypeId = receptionCovidDto.JobTypeId;


            if (receptionCovidDto.Signs?.Count > 0)
            {
                var receptionSign = receptionCovidDto.Signs.Select(g => new ReceptionSign
                {
                    SignId = g

                }).ToList();

                foreach (var item in receptionSign)
                {
                    receptions.ReceptionSign.Add(item);
                }
            }


            if (receptionCovidDto.SpecialIllnesses?.Count > 0)
            {
                var specialIllnessReceptions = receptionCovidDto.SpecialIllnesses.Select(g => new SpecialIllnessReception
                {
                    SpecialIllnessId = g

                }).ToList();

                foreach (var item in specialIllnessReceptions)
                {
                    receptions.SpecialIllnessReception.Add(item);
                }
            }

            if (receptionCovidDto.RequestReceptionAnswers?.Count > 0)
            {
                var receptionAnswers = receptionCovidDto.RequestReceptionAnswers.Select(g => new ReceptionAnswer
                {
                    AnswerId = g.AnswerId,
                    QuestionId = g.QuestionId,
                    InfoDate = g.InfoDate.TryToDateTime()
                }).ToList();

                foreach (var item in receptionAnswers)
                {
                    receptions.ReceptionAnswer.Add(item);
                }
            }

            patient.Person = person;

            if (curPatient?.PatientExtraInfo != null)
                patient.PatientExtraInfo.Add(patientExtraInfo);

            receptions.Patient = patient;

            return receptions;
        }

        public static ReceptionCovidDto Map(Receptions receptions)
        {
            ReceptionCovidDto receptionCovidDto = new ReceptionCovidDto();

            receptionCovidDto.Id = receptions.Id;
            receptionCovidDto.IsHaveSign = receptions.IsHaveSign;
            receptionCovidDto.RelationId = receptions.RelationId;
            //receptionCovidDto.RelationTitle = receptions?.Relation?.Title;
            receptionCovidDto.RecoveryDate = receptions.RecoveryDate;
            receptionCovidDto.ConsumeDrug = receptions.ConsumeDrug;
            receptionCovidDto.DateOfSign = receptions.DateOfSign;
            receptionCovidDto.HospitalEnteryDate = receptions.HospitalEnteryDate;

            receptionCovidDto.FirstName = receptions?.Patient?.Person?.FirstName;
            receptionCovidDto.FatherName = receptions?.Patient?.Person?.FatherName;
            receptionCovidDto.GrandFatherName = receptions?.Patient?.Person?.GrandFatherName;
            receptionCovidDto.Age = receptions?.Patient?.Person?.Age;
            receptionCovidDto.Mobile = receptions?.Patient?.Person?.Mobile;
            receptionCovidDto.ProvinceId = receptions?.Patient?.Person?.ProvinceId;
            receptionCovidDto.CityId = receptions?.Patient?.Person?.CityId;
            receptionCovidDto.ZoneId = receptions?.Patient?.Person?.ZoneId;
            receptionCovidDto.Address = receptions?.Patient?.Person?.Address;
            receptionCovidDto.SexId = receptions?.Patient?.Person?.SexId;

            receptionCovidDto.IsPregnant = receptions.Patient.PatientExtraInfo.Select(g => g.IsPregnant).FirstOrDefault();
            receptionCovidDto.PregnancySeasonId = receptions.Patient.PatientExtraInfo.Select(g => g.PregnancySeasonId).FirstOrDefault();
            receptionCovidDto.Weight = receptions.Patient.PatientExtraInfo.Select(g => g.Weight).FirstOrDefault();
            receptionCovidDto.Height = receptions.Patient.PatientExtraInfo.Select(g => g.Height).FirstOrDefault();
            receptionCovidDto.BloodGroupId = receptions.Patient.PatientExtraInfo.Select(g => g.BloodGroupId).FirstOrDefault();
            receptionCovidDto.JobId = receptions.Patient.PatientExtraInfo.Select(g => g.JobId).FirstOrDefault();
            receptionCovidDto.JobTypeId = receptions.Patient.PatientExtraInfo.Select(g => g.JobTypeId).FirstOrDefault();

            receptionCovidDto.Signs = receptions?.ReceptionSign?.Select(g => g.SignId.GetValueOrDefault()).ToList();
            receptionCovidDto.SpecialIllnesses = receptions?.SpecialIllnessReception?.Select(g => g.SpecialIllnessId.GetValueOrDefault()).ToList();
            receptionCovidDto.RequestReceptionAnswers = receptions?.ReceptionAnswer?.Select(g => new RequestReceptionAnswerDto { AnswerId = g.AnswerId.GetValueOrDefault(), QuestionId = g.QuestionId.GetValueOrDefault(), InfoDate = g.InfoDate.ToDateStringTry() }).ToList();

            return receptionCovidDto;
        }

        public static Expression<Func<Receptions, ListReceptionCovidDto>> MapList
        {
            get
            {
                return x => new ListReceptionCovidDto
                {
                    Id = x.Id,
                    Age = x.Patient.Person.Age,
                    Name = x.Patient.Person.FirstName,
                    IsPregnant = x.Patient.PatientExtraInfo.Select(d => d.IsPregnant.GetValueOrDefault()).FirstOrDefault(),
                    Mobile = x.Patient.Person.Mobile,
                    Sex = x.Patient.Person.Sex != null ? x.Patient.Person.Sex.Title : "",
                    IsResult = x.IsResult
                };
            }
        }

        public static Expression<Func<Receptions, BaseInfoSetAnswerCovidDto>> MapBaseInfoAnswer
        {
            get
            {
                return x => new BaseInfoSetAnswerCovidDto
                {
                    RelationShip = x.Relation != null ? x.Relation.Title : "",
                    Name = x.Patient.Person.FullNameThree,
                    SexAndAge = (x.Patient.Person.Sex != null ? x.Patient.Person.Sex.Title : "") + (x.Patient.Person.Age != null ? " | " + x.Patient.Person.Age : ""),
                    Mobile = x.Patient.Person.Mobile,
                    IsResult = x.IsResult,
                    ResultNote = x.ResultNote,
                    PersonId=x.Patient.PersonId,
                    LatinName=x.Patient.Person.LatinName,
                    ReceptionCode=x.ReceptionId
                };
            }
        }

    }
}
