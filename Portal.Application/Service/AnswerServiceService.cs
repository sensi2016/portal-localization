using His.Reception.Application.Interface;
using Portal.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Portal.Application.Interface;
using Portal.Application.Mapper;
using Portal.Context;
using Portal.DAL;
using Portal.DTO;
using Portal.DTO.Answer;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Portal.Application.Service
{
    public class AnswerServiceService : IAnswerServiceService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<AnswerService> _answerServiceRepository;
        private readonly DbSet<Receptions> _receptionsRepository;
        private readonly DbSet<SmsReception> _smsReceptionRepository;
        private readonly DbSet<Section> _sectionRepository;
        private readonly DbSet<Users> _userRepository;
        private readonly DbSet<RefferFrom> _refferFromRepository;
        private readonly DbSet<Patient> _patientRepository;
        private readonly DbSet<GeneralStatus> _generalStatusRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly IFileManagerService _fileManagerService;
        private readonly IWorkContextService _workContext;
        private readonly ISmsService _smsService;
        private readonly IEmailService _emailService;
        private readonly IReceptionsService _receptionService;
        private readonly IConfiguration _configuration;
        private readonly ISectionService _sectionService;
        public AnswerServiceService(IUnitOfWork unitOfWork, IConfiguration configuration,
            IStringLocalizer<SharedResource> sharedLocalizer, ISectionService sectionService, IFileManagerService fileManagerService, IWorkContextService workContext, ISmsService smsService, IEmailService emailService, IReceptionsService receptionService)
        {
            _unitOfWork = unitOfWork;
            _sharedLocalizer = sharedLocalizer;
            _answerServiceRepository = _unitOfWork.Set<AnswerService>();
            _userRepository = _unitOfWork.Set<Users>();
            _patientRepository = _unitOfWork.Set<Patient>();
            _receptionsRepository = _unitOfWork.Set<Receptions>();
            _refferFromRepository = _unitOfWork.Set<RefferFrom>();
            _sectionRepository = _unitOfWork.Set<Section>();
            _smsReceptionRepository = _unitOfWork.Set<SmsReception>();
            _generalStatusRepository = _unitOfWork.Set<GeneralStatus>();
            _fileManagerService = fileManagerService;
            _workContext = workContext;
            _smsService = smsService;
            _emailService = emailService;
            _receptionService = receptionService;
            _configuration = configuration;
            _sectionService = sectionService;
        }

        public async Task<BaseResponseDto> Upload(UploadAnswerDto uploadAnswerDto)
        {
            if (!string.IsNullOrEmpty(uploadAnswerDto.Name) && !string.IsNullOrEmpty(uploadAnswerDto.Mobile))
            {
                var dicError = new Dictionary<string, string>() {
                    { "MoileOrName",_sharedLocalizer["UploadAnswerForm.Response.MoileOrName"]}
                };
                var error = Utilities.CreateErrorMessage("MoileOrName", dicError);

                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Errors = error
                };
            }

            if (uploadAnswerDto.File.Length == 0)
            {
                var dicError = new Dictionary<string, string>() {
                    { "NotSendFile",_sharedLocalizer["UploadAnswerForm.Response.NotSendFile"]}//+
                };
                var error = Utilities.CreateErrorMessage("NotSendFile", dicError);

                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Errors = error
                };
            }

            _unitOfWork.Databases.BeginTransaction();

            var map = AnswerMapper.Map(uploadAnswerDto);
            map.InsertUserId = _workContext.UserId;
            map.SectionId = _workContext.SectionId;
            var code = await MaxCode();
            map.Code = (++code).ToString();

            //if (map.UserId != null && map.UserId.GetValueOrDefault() > 0 && !string.IsNullOrEmpty(uploadAnswerDto.Mobile))
            //{
            //    //شماره کاربرهم ذخیره میکنیم
            //    var userMobile = await _userRepository.Where(d => d.Id == uploadAnswerDto.UserId)
            //        .Include(d => d.Person)
            //        .Select(g => new{ g.Person.Mobile)
            //        .FirstOrDefaultAsync();

            //    map.Mobile = userMobile;
            //}
            _answerServiceRepository.Add(map);

            await _unitOfWork.SaveChangesAsync();
            //اپلود تصویر گرفتن ایدی فایل
            var resultId = await _fileManagerService.Upload(new FileUploadDto { File = uploadAnswerDto.File, TableName = "Answer", FileGroupId = 1, PrimeryKey = map.Id.ToString() });
            map.FileId = resultId;
            _answerServiceRepository.Update(map);
            await _unitOfWork.SaveChangesAsync();

            _unitOfWork.Databases.CommitTransaction();


            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = new { FileId = map.FileId, Id = map.Id }
            };
        }


        public async Task<long> MaxCode()
        {
            var maxReceptionCode = await _answerServiceRepository
                //.Select(x => x.Code.TryToLong())
                // .DefaultIfEmpty(0)
                .MaxAsync(d => d.Code);

            var result = maxReceptionCode.TryToLong();

            //return maxReceptionCode;

            return result;
        }

        public async Task<BaseResponseDto> Download(string fileId)
        {
            var result = await _fileManagerService.Download(fileId);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = result
            };
        }

        public async Task<BaseResponseDto> Sms(NotificationDto notification)
        {
            await _smsService.SendSms(new SendSmsDto { Message = notification.Link, Mobile = notification.Moblie });


            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["GlobalForm.Response.SendSms"]//+
            };
        }

        public async Task<BaseResponseDto> Email(NotificationDto notification)
        {
            await _emailService.SendEmail(new SendEmailDto { Body = notification.Link, Email = notification.Email });


            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["GlobalForm.Response.SendEmail"]//+
            };
        }

        public async Task<ListResponseDto> Search(FilterAnswerDto filterAnswer)
        {
            var query = _answerServiceRepository
                .OrderByDescending(d => d.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filterAnswer.Email))
            {
                query = query.Where(d => d.User.Person.Email == filterAnswer.Email);
            }

            if (!string.IsNullOrEmpty(filterAnswer.Mobile))
            {
                query = query.Where(d => EF.Functions.Like(d.Mobile, "%" + filterAnswer.Mobile + "%") || EF.Functions.Like(d.User.Person.Mobile, "%" + filterAnswer.Mobile + "%"));
            }

            if (!string.IsNullOrEmpty(filterAnswer.Name))
            {
                query = query.Where(d => EF.Functions.Like(d.User.Person.FirstName + ' ' + d.User.Person.LastName, $"%{filterAnswer.Name}%") || EF.Functions.Like(d.Name, $"%{filterAnswer.Name}%"));
                //query = query.Where(d=>CustomePredicate.FullName(d.User.Person,""));
            }

            if (filterAnswer.Type != null)
            {
                if (filterAnswer.Type == (int)AnswerTypeEnum.Moblie)
                {
                    query = query.Where(d => d.Mobile != null && d.UserId == null);
                }
                if (filterAnswer.Type == (int)AnswerTypeEnum.User)
                {
                    query = query.Where(d => d.UserId != null);
                }
            }

            if (!string.IsNullOrEmpty(filterAnswer.FromDate))
            {
                query = query.Where(d => d.CreateDate.Value.Date >= filterAnswer.FromDate.TryToDateTime().GetValueOrDefault().Date);
            }

            if (!string.IsNullOrEmpty(filterAnswer.ToDate))
            {
                query = query.Where(d => d.CreateDate.Value.Date <= filterAnswer.ToDate.TryToDateTime().GetValueOrDefault().Date);

            }

            var queryUser = query.Where(d => d.UserId != null).GroupBy(d => d.UserId)
                             .Select(g => new ListAnswerDto
                             {
                                 UserId = g.Key,
                                 Mobile = _answerServiceRepository.Where(d => d.UserId == g.Key).Select(t => t.Mobile).FirstOrDefault(),
                                 Name = _userRepository.Where(t => t.Id == g.Key).Select(y => y.Person.FirstName + " " + y.Person.LastName).FirstOrDefault(),
                                 TotalFile = _answerServiceRepository.Where(d => d.UserId == g.Key).Count(),
                                 Type = "For User ",
                                 Link = _answerServiceRepository.Where(d => d.Id == -1).Select(t => t.FileId).FirstOrDefault()
                             })
                             .AsQueryable();

            var queryMobile = query.Where(d => d.UserId == null).Select(g => new ListAnswerDto
            {
                UserId = null,
                Mobile = _answerServiceRepository.Where(d => d.Id == g.Id).Select(t => t.Mobile).FirstOrDefault(),
                Name = _userRepository.Where(t => t.Id == -1).Select(y => y.Person.FirstName + " " + y.Person.LastName).FirstOrDefault(),
                TotalFile = _answerServiceRepository.Where(d => d.UserId == -1).Count(),
                Type = "For Mobile ",
                Link = _answerServiceRepository.Where(d => d.Id == g.Id).Select(t => t.FileId).FirstOrDefault()
            }).AsQueryable();


            queryUser = queryUser.Concat(queryMobile);

            var lst = await queryUser.ToPagedQuery(filterAnswer).ToListAsync();

            foreach (var item in lst)
            {
                if (item.TotalFile == 0) item.TotalFile = 1;
            }

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Count = await queryUser.CountAsync(),
                Data = lst
            };
        }

        public async Task<ListResponseDto> GetDetail(long userId)
        {
            var lst = await _answerServiceRepository.Where(d => d.UserId == userId).Select(g => new DetailAnswerDto
            {
                Date = g.CreateDate.ToDateStringTry(),
                Link = g.FileId
            }).AsNoTracking().ToListAsync();


            return new ListResponseDto
            {
                Data = lst,
                Status = ResponseStatus.Success
            };
        }


        public async Task<ListResponseDto> SearchForPatient(FilterPatinetAnswerDto filterPatinetAnswerDto)
        {
            var query = _answerServiceRepository
                .OrderByDescending(d => d.Id)
                .Include(d => d.Section.Center)
                .OrderByDescending(d => d.Id)
                .AsQueryable();

            var user = await _userRepository.Where(d => d.Id == _workContext.UserId).FirstOrDefaultAsync();

            query = query.Where(d => d.UserId == _workContext.UserId || d.Mobile == user.UserName);

            if (!string.IsNullOrEmpty(filterPatinetAnswerDto.FromDate))
            {
                query = query.Where(d => d.CreateDate.Value.Date >= filterPatinetAnswerDto.FromDate.TryToDateTime().GetValueOrDefault().Date);
            }

            if (!string.IsNullOrEmpty(filterPatinetAnswerDto.ToDate))
            {
                query = query.Where(d => d.CreateDate.Value.Date <= filterPatinetAnswerDto.ToDate.TryToDateTime().GetValueOrDefault().Date);
            }

            return new ListResponseDto
            {
                Data = await query.ToPagedQuery(filterPatinetAnswerDto).Include(d => d.Section).Select(g => Mapper.AnswerMapper.Map(g)).AsNoTracking().ToListAsync(),
                Count = await query.CountAsync(),
                Status = ResponseStatus.Success
            };
        }

        public async Task<ListResponseDto> CurrentPaitent(FilterPatinetAnswerDto filterPatinetAnswerDto)
        {
            var query = _answerServiceRepository
                .OrderByDescending(d => d.Id)
                .Include(d => d.Section.Center)
                .OrderByDescending(d => d.Id)
                .AsQueryable();

            //var user = await _userRepository.Where(d => d.Id == _workContext.UserId)
            //    .Include(d=>d.CardCode)
            //    .FirstOrDefaultAsync();

            //query = query.Where(d => d.UserId == _workContext.UserId || d.Mobile == user.UserName);

            query = query.Where(d => d.Patient.Person.Users.Any(t => t.Id == _workContext.UserId));

            if (!string.IsNullOrEmpty(filterPatinetAnswerDto.FromDate))
            {
                query = query.Where(d => d.CreateDate.Value.Date >= filterPatinetAnswerDto.FromDate.TryToDateTime().GetValueOrDefault().Date);
            }

            if (!string.IsNullOrEmpty(filterPatinetAnswerDto.ToDate))
            {
                query = query.Where(d => d.CreateDate.Value.Date <= filterPatinetAnswerDto.ToDate.TryToDateTime().GetValueOrDefault().Date);
            }

            var lst = await query.ToPagedQuery(filterPatinetAnswerDto).Include(d => d.Section).Select(g => Mapper.AnswerMapper.Map(g)).AsNoTracking().ToListAsync();

            if (lst?.Count > 0)
            {
                var files = await _fileManagerService.GetFiles(lst.Select(g => g.FileId).ToList());

                lst.ForEach(d => d.FileExtension = files.Data.Select(t => t.Format).FirstOrDefault());
            }
            return new ListResponseDto
            {
                Data = lst, //await query.ToPagedQuery(filterPatinetAnswerDto).Include(d => d.Section).Select(g => Mapper.AnswerMapper.Map(g)).AsNoTracking().ToListAsync(),
                Count = await query.CountAsync(),
                Status = ResponseStatus.Success
            };
        }


        public async Task<BaseResponseDto> GetCovidResult()
        {
            var result = await _receptionService.GetResultByUserId(_workContext.UserId.GetValueOrDefault());

            return result;
        }

        public async Task<BaseResponseDto> FastUpload(FastUploadAnswerDto fastUploadAnswerDto)
        {
            if (fastUploadAnswerDto.SectionId.GetValueOrDefault() < 1)
            {
                var dicError = new Dictionary<string, string>() {
                    { "SectionId",_sharedLocalizer["UploadAnswerForm.Response.SectionId"]}
                };
                var error = Utilities.CreateErrorMessage("SectionId", dicError);

                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Errors = error
                };
            }

            if (string.IsNullOrEmpty(fastUploadAnswerDto.Name) || string.IsNullOrEmpty(fastUploadAnswerDto.Mobile))
            {
                var dicError = new Dictionary<string, string>() {
                    { "MoileOrName",_sharedLocalizer["UploadAnswerForm.Response.MoileOrName"]}
                };
                var error = Utilities.CreateErrorMessage("MoileOrName", dicError);

                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Errors = error
                };
            }

            if (fastUploadAnswerDto.File.Length == 0)
            {
                var dicError = new Dictionary<string, string>() {
                    { "NotSendFile",_sharedLocalizer["UploadAnswerForm.Response.NotSendFile"]}//+
                };
                var error = Utilities.CreateErrorMessage("NotSendFile", dicError);

                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Errors = error
                };
            }

            _unitOfWork.Databases.BeginTransaction();

            var patient = new Patient();
            patient = null;

            if (!string.IsNullOrEmpty(fastUploadAnswerDto.NHSNumber))
                patient = await _patientRepository.Where(d => d.Person.Users.Any(t => t.CardCode.HealthNumber == fastUploadAnswerDto.NHSNumber)).FirstOrDefaultAsync();


            var map = AnswerMapper.Map(fastUploadAnswerDto);

            var code = await MaxCode();
            map.Code = (++code).ToString();
            map.PatientId = patient?.Id;

            _answerServiceRepository.Add(map);

            await _unitOfWork.SaveChangesAsync();
            //اپلود تصویر گرفتن ایدی فایل
            var resultId = await _fileManagerService.Upload(new FileUploadDto { File = fastUploadAnswerDto.File, TableName = nameof(AnswerService), FileGroupId = 1, PrimeryKey = map.Id.ToString() });
            map.FileId = resultId;
            map.IsOutSystem = true;
            _answerServiceRepository.Update(map);
            await _unitOfWork.SaveChangesAsync();

            _unitOfWork.Databases.CommitTransaction();

            //بعد ثبت یک اسمس لینک به کاربر موردنظر ارسال میشه تابتواند جواب دانلود کند

            var link = _configuration["AnswerDownloadUrl"].Replace("{fileId}", map.FileId);

            await _smsService.SendSms(new SendSmsDto { Mobile = map.Mobile, Message = _sharedLocalizer["ReceptionCovidForm.Response.AnswerNotif"].Value.Replace("{LinkAnswer}", link).Replace("{newline}", "\r\n").Replace("{FullName}", fastUploadAnswerDto.Name) });

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["GlobalForm.Response.SendSms"]
            };
        }


        #region excel send answer

        // public async Task<BaseResponseDto> ExcelUpload(string answers)
        public async Task<BaseResponseDto> ExcelUpload(ExcelAnswerDto excelAnswerDto)
        {
            //var excelAnswerDto = JsonConvert.DeserializeObject<ExcelAnswerDto>(answers);
            var listReceptions = new List<Receptions>();
            var listSendedReceptions = new List<ExcelAnswerDetailsDto>();
            var listFailSendedReceptions = new List<ExcelAnswerDetailsDto>();

            var refferFrom = await _refferFromRepository.ToListAsync();
            var generalStatuses = await _generalStatusRepository.ToListAsync();
            var section = (Section)(await _sectionService.GetSectionByCode("Pcr")).Data;

            var maxReceptionId = (await _receptionService.MaxReceptionId()).Data;
            var maxIntenralId = (await _receptionService.MaxInternalId()).Data;
            long receptionId = (long)maxReceptionId;
            int intenralId = (int)maxIntenralId;
            //این کد برای قفل کردن جداول بخاطر کد یونیک پذیرش و بیمار می باشد تا کسی در حال اضافه کردن پذیرش جدید نزند
            //_unitOfWork.Databases.BeginTransaction();

            foreach (var item in excelAnswerDto.Details)
            {
                if (string.IsNullOrEmpty(item.Result) || string.IsNullOrEmpty(item.Name))
                {
                    listFailSendedReceptions.Add(item);
                    continue;
                }

                listSendedReceptions.Add(item);

                #region generate code

                var code = Utilities.GenerateUniqCode();
                var isDuplicate = true;
                int smsSendStatusId = 0;

                while (isDuplicate)
                {
                    if (await _smsReceptionRepository.AnyAsync(d => d.FileName == code))
                    {
                        isDuplicate = true;
                        code = Utilities.GenerateUniqCode();
                    }

                    isDuplicate = false;
                }

                //map.FileName = code;

                #endregion

                try
                {
                    var link = _configuration["AnswerShowReport"].Replace("{fileId}", code);
                    var result = await _smsService.SendSms(new SendSmsDto { Mobile = item.Mobile, Message = _sharedLocalizer["SendSms.Response.FromExcel"].Value.Replace("{Result}", item.Result).Replace("{newline}", "\r\n").Replace("{FullName}", item.Name).Replace("{LinkAnswer}", link) });

                    if (result.Status == ResponseStatus.Success)
                    {
                        smsSendStatusId = (int)SendsmsStatusEnum.Sended;

                    }
                    else
                    {
                        smsSendStatusId = (int)SendsmsStatusEnum.Fail;
                    }
                }
                catch (Exception ex)
                {
                    smsSendStatusId = (int)SendsmsStatusEnum.Fail;
                }
                finally
                {
                    var map = Mapper.AnswerMapper.ExcelMap(item, excelAnswerDto.AnswerDate, _workContext.UserId, section.Id, refferFrom, generalStatuses, smsSendStatusId, code);
                    receptionId = receptionId + 1;
                    intenralId = intenralId + 1;

                    map.ReceptionId = receptionId;
                    map.Patient.InternalId = intenralId;
                    listReceptions.Add(map);
                }
            }

            _receptionsRepository.AddRange(listReceptions);

            await _unitOfWork.SaveChangesAsync();

            //_unitOfWork.Databases.CommitTransaction();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
            };
        }


        public async Task<ListResponseDto> ListExcelUpload(FilterAnswerExcelDto filterAnswerExcelDto)
        {
            var query = _receptionsRepository.OrderByDescending(d => d.Id).AsQueryable();

            if (!string.IsNullOrEmpty(filterAnswerExcelDto.FromAnswerDate))
            {
                query = query.Where(d => d.AnswerDate.HasValue && d.AnswerDate.Value.Date >= filterAnswerExcelDto.FromAnswerDate.TryToDateTime().Value.Date);
            }

            if (!string.IsNullOrEmpty(filterAnswerExcelDto.ToAnswerDate))
            {
                query = query.Where(d => d.AnswerDate.HasValue && d.AnswerDate.Value.Date <= filterAnswerExcelDto.ToAnswerDate.TryToDateTime().Value.Date);
            }

            if (filterAnswerExcelDto.IsPositive.HasValue)
            {
                query = query.Where(d => d.ReceptionService.Any(t => t.Answer1.Any(x => x.Result.Contains("Positive"))));
            }

            if (filterAnswerExcelDto.IsNegative.HasValue)
            {
                query = query.Where(d => d.ReceptionService.Any(t => t.Answer1.Any(x => x.Result.Contains("Negative"))));
            }

            if (!string.IsNullOrEmpty(filterAnswerExcelDto.FullName))
            {
                query = query.Where(d => EF.Functions.Like(d.Patient.Person.FirstName, $"%{filterAnswerExcelDto.FullName}%"));// d.Patient.Person.FullName.Contains(filterAnswerExcelDto.FullName));
            }

            if (!string.IsNullOrEmpty(filterAnswerExcelDto.Mobile))
            {
                query = query.Where(d => EF.Functions.Like(d.Patient.Person.Mobile, $"%{ filterAnswerExcelDto.Mobile}"));
            }

            if (filterAnswerExcelDto.RefferFromId?.Count > 0)
            {
                query = query.Where(d => filterAnswerExcelDto.RefferFromId.Contains(d.RefferFromId.GetValueOrDefault()));
            }

            if (filterAnswerExcelDto.PatientStatusId?.Count > 0)
            {
                query = query.Where(d => d.ReceptionHistory.Any(t => filterAnswerExcelDto.PatientStatusId.Contains(t.PatientStatusId.GetValueOrDefault())));
            }

            var lst = await query
                .Where(d => d.Section.LocalCode == Utilities.SectionCode.Pcr)
                .ToPagedQuery(filterAnswerExcelDto)
                .Select(Mapper.AnswerMapper.MapListExcelAnswer)
                .AsNoTracking()
                .ToListAsync();

            var negative = await query.CountAsync(d => d.ReceptionService.Any(t => t.Answer1.Any(x => x.Result.Equals("Negative"))));
            var positive = await query.CountAsync(d => d.ReceptionService.Any(t => t.Answer1.Any(x => x.Result.Equals("Positive"))));

            // query.CountAsync(d => d.ReceptionService.Any(t => t.Answer1.Select(x => new { x.Result.Equals("Positive"),x.Result.Equals("Negative"))));

            //var pn = await query.GroupBy(d => d.an).Select(g => new
            //{
            //    Positive = g.ReceptionService.Select(t => t.Answer1.Count()).FirstOrDefault()
            //}).FirstOrDefaultAsync();

            return new ListResponseDto
            {
                Status = ResponseStatus.Success,
                Data = new
                {
                    List = lst,
                    TotalPositive = positive,
                    TotalNegative = negative
                },
                Count = await query.Where(d => d.Section.LocalCode == "Pcr").CountAsync()
            };
        }

        public async Task<BaseResponseDto> ReportExcelAnswer(string key)
        {
            var map = await _smsReceptionRepository.Where(d => d.FileName == key)
                 .Select(g => new
                 {
                     Id = g.Id,
                     //Date = g.CreateDate.ToDateTimeStringTry(),
                     Date = g.Reception.AnswerDate.TryToDateString(),
                     Status = g.SendsmsStatus.Title,
                     FullName = g.Reception.Patient.Person.FirstName,
                     FileName = g.FileName,
                     Result = g.Reception.ReceptionService.SelectMany(t => t.Answer1.Select(x => x.Result)).FirstOrDefault(),
                     RefferFrom = g.Reception.RefferFrom.Title,
                     Title = g.Reception.Note,
                     Age = g.Reception.Patient.Person.Age,
                     Mobile = g.Reception.Patient.Person.Mobile
                 })
                 .AsNoTracking()
                 .FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Data = map,
                Status = ResponseStatus.Success
            };
        }

        public async Task<BaseResponseDto> ExcelGetById(long id)
        {
            var result = await _receptionsRepository.Where(d => d.Id == id)
                .Include(d => d.Patient.Person)
                .Include(d => d.ReceptionService)
                .ThenInclude(d => d.Answer1)
                .Include(d => d.ReceptionService)
                .ThenInclude(d => d.Status)
                .Include(d => d.RefferFrom)
                .Include(d => d.ReceptionHistory)
                .ThenInclude(d => d.PatientStatus)
                .Select(g => Mapper.AnswerMapper.ExcelMap(g)).FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Data = result,
                Status = ResponseStatus.Success
            };
        }

        public async Task<BaseResponseDto> EditExcel(ExcelAnswerDetailsDto excelAnswerDetailsDto)
        {
            var cur = await _receptionsRepository.Where(d => d.Id == excelAnswerDetailsDto.Id)
                                                 .Include(d => d.Patient.Person)
                                                 .Include(d => d.ReceptionService)
                                                 .ThenInclude(d => d.Answer1)
                                                 .Include(d => d.ReceptionService)
                                                 .ThenInclude(d => d.Status)
                                                 .Include(d => d.RefferFrom)
                                                 .Include(d => d.ReceptionHistory)
                                                 .ThenInclude(d => d.PatientStatus)
                                                 .Include(d => d.SmsReception)
                                                 .FirstOrDefaultAsync();

            var refferFrom = await _refferFromRepository.ToListAsync();
            var generalStatuses = await _generalStatusRepository.ToListAsync();
            var section = (Section)(await _sectionService.GetSectionByCode("Pcr")).Data;

            var map = Mapper.AnswerMapper.ExcelMap(excelAnswerDetailsDto, cur, "", 1, 1, refferFrom, generalStatuses, 1, "");

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["ExcelUploadForm.Response.EditSuccess"]
            };
        }




        #endregion
    }
}
