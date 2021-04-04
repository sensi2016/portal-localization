using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.DTO.Answer;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Portal.Application.Mapper
{
    public class AnswerMapper
    {
        public static AnswerService Map(UploadAnswerDto uploadAnswerDto)
        {
            return new AnswerService
            {
                UserId = uploadAnswerDto.UserId,
                Mobile = uploadAnswerDto.Mobile,
                PatientId = uploadAnswerDto.PatientId,
                CreateDate = DateTime.Now,
                Note = uploadAnswerDto.Note,
                Name = uploadAnswerDto.Name

            };
        }

        public static AnswerService Map(FastUploadAnswerDto fastUploadAnswerDto)
        {
            return new AnswerService
            {
                Mobile = fastUploadAnswerDto.Mobile,
                CreateDate = DateTime.Now,
                Note = fastUploadAnswerDto.Note,
                Name = fastUploadAnswerDto.Name,
                SectionId = fastUploadAnswerDto.SectionId,
                Nhsnumber = fastUploadAnswerDto.NHSNumber

            };
        }



        public static AnswerService Map(AnswerService answerService, UploadAnswerDto uploadAnswerDto)
        {
            answerService.UserId = uploadAnswerDto.UserId;
            answerService.Mobile = uploadAnswerDto.Mobile;
            answerService.PatientId = uploadAnswerDto.PatientId;
            answerService.CreateDate = DateTime.Now;
            answerService.Note = uploadAnswerDto.Note;
            answerService.Name = uploadAnswerDto.Name;

            return answerService;
        }


        public static ListAnswerPatientDto Map(AnswerService answerService)
        {
            return new ListAnswerPatientDto
            {
                Id = answerService.Id,
                Date = answerService.CreateDate.ToDateTimeStringTry(),
                Note = answerService.Note,
                FileId = answerService.FileId,
                Center = (answerService.Section != null && answerService.Section.Center != null) ? answerService.Section.Center.Title : ""
            };
        }

        public static Receptions ExcelMap(ExcelAnswerDetailsDto excelAnswerDtos, string answerDate, int? userId, int sectionId, List<RefferFrom> refferFrom, List<GeneralStatus> generalStatuses, int smsSendStatusId, string code)
        {
            var receptions = new Receptions();
            var receptionService = new ReceptionService();
            var patient = new Patient();
            var person = new Person();
            var answer = new Answer1();
            var smsReception = new SmsReception();
            var receptionHistory = new ReceptionHistory();

            receptionHistory.CreateDate = DateTime.Now;
            receptionHistory.PatientStatusId = generalStatuses.Where(d => d.Code1 == excelAnswerDtos.Status).Select(g => g.Id).FirstOrDefault(); ;
            receptionHistory.SectionId = sectionId;
            receptionHistory.IsCurrent = true;
            receptionHistory.UserId = userId;

            answer.Result = excelAnswerDtos.Result;
            answer.Comment = excelAnswerDtos.Notice;
            receptionService.Answer1.Add(answer);

            smsReception.Mobile = excelAnswerDtos.Mobile;
            smsReception.CreateDate = DateTime.Now;
            smsReception.SendsmsStatusId = smsSendStatusId;
            smsReception.FileName = code;

            if (smsSendStatusId == (int)SendsmsStatusEnum.Sended) smsReception.SendDate = DateTime.Now;

            receptions.ReceptionDate = DateTime.Now;
            receptions.AnswerDate = answerDate.TryToDateTime();
            receptions.Note = excelAnswerDtos.Title;
            receptions.SectionId = sectionId;
            receptions.RefferFromId = refferFrom.Where(d => d.Code1 == excelAnswerDtos.RefferFrom).Select(g => g.Id).FirstOrDefault();
            receptions.SmsReception.Add(smsReception);
            receptions.ReceptionHistory.Add(receptionHistory);

            person.Age = (short)excelAnswerDtos.Age;
            person.FirstName = excelAnswerDtos.Name;
            person.Mobile = excelAnswerDtos.Mobile;

            patient.Person = person;
            receptions.Patient = patient;
            receptions.ReceptionService.Add(receptionService);
            return receptions;

            //return new AnswerService
            //{
            //    Age = excelAnswerDtos.Age,
            //    CreateDate = DateTime.Now,
            //    Name = excelAnswerDtos.Name,
            //    Title = excelAnswerDtos.Title,
            //    RefferFrom = excelAnswerDtos.RefferFrom,
            //    Mobile = excelAnswerDtos.Mobile,
            //    Result = excelAnswerDtos.Result,
            //    PatientStatus = excelAnswerDtos.Status,
            //    SendsmsStatusId = 1,
            //    UserId = userId,
            //    InsertUserId = userId,
            //    IsExcel=true,
            //    FileName=Guid.NewGuid().ToString()
            //};
        }

        public static Receptions ExcelMap(ExcelAnswerDetailsDto excelAnswerDtos, Receptions receptions, string answerDate, int? userId, int sectionId, List<RefferFrom> refferFrom, List<GeneralStatus> generalStatuses, int smsSendStatusId, string code)
        {
            // زراعتی : اینها استفاده نمی‌شدن کامنت کردم
            //var receptionService = receptions.ReceptionService.FirstOrDefault();
            //if(receptionService!=null)receptionService.Answer1.Add(answer);

            //var patient = receptions.Patient;
            //patient.Person = person;

            var person = receptions.Patient.Person;
            person.Age = (short)excelAnswerDtos.Age;
            person.FirstName = excelAnswerDtos.Name;
            person.Mobile = excelAnswerDtos.Mobile;

            var answer = receptions.ReceptionService?.FirstOrDefault()?.Answer1.FirstOrDefault();
            if (answer != null)
            {
                answer.Result = excelAnswerDtos.Result;
                answer.Comment = excelAnswerDtos.Notice;
            }
            
            var smsReception = receptions?.SmsReception.FirstOrDefault();
            if (smsReception != null)
            {
                smsReception.Mobile = excelAnswerDtos.Mobile;
                //smsReception.CreateDate = DateTime.Now;
                //smsReception.SendsmsStatusId = smsSendStatusId;
                //smsReception.FileName = code;
                //if (smsSendStatusId == (int)SendsmsStatusEnum.Sended)smsReception.SendDate = DateTime.Now;
            }

            var receptionHistory = receptions?.ReceptionHistory.FirstOrDefault();
            if (receptionHistory != null)
            {
                receptionHistory.PatientStatusId = generalStatuses.Where(d => d.Code1 == excelAnswerDtos.Status).Select(g => g.Id).FirstOrDefault();
                //receptionHistory.CreateDate = DateTime.Now;
                //receptionHistory.SectionId = sectionId;
                //receptionHistory.IsCurrent = true;
                //receptionHistory.UserId = userId;
            }

            receptions.Note = excelAnswerDtos.Title;
            receptions.RefferFromId = refferFrom.Where(d => d.Code1 == excelAnswerDtos.RefferFrom).Select(g => g.Id).FirstOrDefault();
            // receptions.ReceptionDate = DateTime.Now;
            //receptions.AnswerDate = answerDate.TryToDateTime();
            //receptions.SectionId = sectionId;
            //receptions.SmsReception.Add(smsReception);
            //receptions.ReceptionHistory.Add(receptionHistory);
            //receptions.Patient = patient;
            //receptions.ReceptionService.Add(receptionService);

            return receptions;
        }
        public static ExcelAnswerDetailsDto ExcelMap(Receptions receptions)
        {
            ExcelAnswerDetailsDto excelAnswerDetailsDto = new ExcelAnswerDetailsDto();

            excelAnswerDetailsDto.Id = receptions.Id;
            excelAnswerDetailsDto.Mobile = receptions?.Patient?.Person?.Mobile;
            excelAnswerDetailsDto.Name = receptions?.Patient?.Person?.FirstName;
            excelAnswerDetailsDto.Age = receptions?.Patient?.Person?.Age;
            excelAnswerDetailsDto.Result = receptions?.ReceptionService?.SelectMany(d => d.Answer1?.Select(g => g.Result)).FirstOrDefault();
            excelAnswerDetailsDto.Status = receptions?.ReceptionHistory?.Select(t=>t?.PatientStatus?.Code1).FirstOrDefault();
            excelAnswerDetailsDto.Title = receptions.Note;
            excelAnswerDetailsDto.RefferFrom = receptions?.RefferFrom?.Code1;
            excelAnswerDetailsDto.Notice = receptions?.ReceptionService?.SelectMany(d => d.Answer1?.Select(g => g.Comment)).FirstOrDefault();

            return excelAnswerDetailsDto;
        }

        public static Expression<Func<Receptions, object>> MapListExcelAnswer
        {
            get
            {
                return x => new
                {
                    Id = x.Id,
                    Date = x.ReceptionDate.ToDateTimeStringTry(),
                    AnswerDate = x.AnswerDate.ToDateTimeStringTry(),
                    Status = x.SmsReception.Any() ? x.SmsReception.Select(t => t.SendsmsStatus.Title).FirstOrDefault() : "",
                    FullName = x.Patient != null ? x.Patient.Person.FullName : "",
                    FileName = x.SmsReception.Any() ? x.SmsReception.Select(r => r.FileName).FirstOrDefault() : "",
                    Result = x.SmsReception.Any() ? x.ReceptionService.SelectMany(t => t.Answer1.Select(r => r.Result)).FirstOrDefault() : "",
                    Notice = x.SmsReception.Any() ? x.ReceptionService.SelectMany(t => t.Answer1.Select(r => r.Comment)).FirstOrDefault() : "",
                    RefferFrom = x.RefferFrom != null ? Utilities.Language.GetTilteByLang(x.RefferFrom, Utilities.Language.CurrentLanguage) : "",
                    GeneralStatuse = x.ReceptionHistory != null ? x.ReceptionHistory.Select(g => Utilities.Language.GetTilteByLang(g.PatientStatus, Utilities.Language.CurrentLanguage)).FirstOrDefault() : "",
                    Title = x.Note,
                    Mobile = x.SmsReception.Any() ? x.SmsReception.Select(t => t.Mobile).FirstOrDefault() : "",
                };
            }

        }
    }
}
