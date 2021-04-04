
using Portal.DAL.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Portal.Application.Infrastructure;
using Portal.Application.Interface;
using Portal.Application.Mapper;
using Portal.Application.Validation;
using Portal.Context;
using Portal.DTO;
using Portal.DTO.Enum;
using Portal.DTO.User;
using Portal.Entities.Models;
using Portal.Infrastructure;
using Portal.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using UAParser;
using His.Reception.Application.Interface;

namespace Portal.Service
{
    public class UserManagerService : IUserManagerService
    {
        //private readonly IMenuService _menuService;
        private readonly IWorkContextService _workContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Login> _loginRepository;
        private readonly DbSet<Users> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        private readonly DbSet<UserPermission> _userPermissionRepository;
        private readonly DbSet<UserRolePermission> _userRolePermissionsRepository;
        private readonly DbSet<FileTag> _fileTagRepository;
        private readonly DbSet<UserRole> _userRolesRepository;
        private readonly DbSet<Role> _roleRepository;
        private readonly DbSet<Permissions> _PermissionRepository;
        private readonly DbSet<Section> _sectionRepository;
        private readonly DbSet<ReceptionService> _receptionServices;
        private readonly DbSet<CardCode> _cardCodeRepository;
        private readonly DbSet<UserCardCode> _userCardCode;
        private readonly DbSet<Person> _personRepository;
        private readonly DbSet<Patient> _patientRepository;
        private readonly DbSet<PatientExtraInfo> _patientExtraInfoRepository;
        private readonly DbSet<MobileActivation> _mobileActivationRepository;
        private readonly IOptions<RequestLocalizationOptions> _locOption;
        private readonly ISmsService _smsService;
        private readonly IFilesService _filesService;
        private readonly IFileManagerService _fileManagerService;
        private readonly IOptions<ConfigSmsDto> _confiSmsDto;

        public UserManagerService(
            IOptions<ConfigSmsDto> configSms,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer<SharedResource> sharedLocalizer,
            IWorkContextService workContextService,
            IOptions<RequestLocalizationOptions> locOption,
            ISmsService smsService,
            IFilesService filesService,
            IFileManagerService fileManagerService
            )

        {
            _confiSmsDto = configSms;
            _unitOfWork = unitOfWork;
            _loginRepository = _unitOfWork.Set<Login>();
            _userRepository = _unitOfWork.Set<Users>();
            _personRepository = _unitOfWork.Set<Person>();
            _patientRepository = _unitOfWork.Set<Patient>();
            _receptionServices = _unitOfWork.Set<ReceptionService>();
            _patientExtraInfoRepository = _unitOfWork.Set<PatientExtraInfo>();
            _httpContextAccessor = httpContextAccessor;
            _sharedLocalizer = sharedLocalizer;
            _userPermissionRepository = _unitOfWork.Set<UserPermission>();
            _userRolePermissionsRepository = _unitOfWork.Set<UserRolePermission>();
            _sectionRepository = _unitOfWork.Set<Section>();
            _PermissionRepository = _unitOfWork.Set<Permissions>();
            _cardCodeRepository = _unitOfWork.Set<CardCode>();
            _userCardCode = _unitOfWork.Set<UserCardCode>();
            _fileTagRepository = _unitOfWork.Set<FileTag>();

            _userRolesRepository = _unitOfWork.Set<UserRole>();
            _roleRepository = _unitOfWork.Set<Role>();
            _mobileActivationRepository = _unitOfWork.Set<MobileActivation>();
            //_menuService = menuService;
            _workContext = workContextService;
            _locOption = locOption;
            _smsService = smsService;
            _filesService = filesService;
            _fileManagerService = fileManagerService;

        }

        //[ValidateFilterAttribute]
        public async Task<BaseResponseDto> Login(LoginDto loginDto)
        {
            var resultVaild = CheckValidate.Valid<LoginDto>(new LoginValidation(_sharedLocalizer, _locOption), loginDto);

            if (resultVaild.Status == ResponseStatus.NotValid)
            {
                return new LoginResponseDto
                {
                    Status = resultVaild.Status,
                    Message = resultVaild.Message,
                    Errors = resultVaild.Errors
                };
            }

            LoginUserInfoDto loginUserInfoDto = new LoginUserInfoDto();
            var refreshId = Guid.NewGuid();
            var token = Guid.NewGuid();
            RefreshIdDto refreshIdDto = new RefreshIdDto();

            #region check User logn

            var hashPassowrd = Utilities.ComputeHash(loginDto.Password, new SHA256CryptoServiceProvider()).Replace("-", "");
            var curUser = await _userRepository
                //.Include(u => u.UserRolePermission).ThenInclude(up => up.Permission)
                .Include(u => u.UserRolePermission).ThenInclude(up => up.Role)
                .Include(u => u.UserRolePermission).ThenInclude(up => up.Section)
                .Include(u => u.UserRolePermission).ThenInclude(up => up.Permission)
                //.Include(u=>u.UserPermission).ThenInclude(up=>up.Permission)
                .Include(u => u.CardCode)
                .Include(u => u.UserCardCode)
                .Include(u => u.Person)
                .Where(u => u.UserName == loginDto.UserName && u.Password == hashPassowrd)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (curUser is null)
            {
                //loginResponce.
                var dicError = new Dictionary<string, string>() {
                    { "NotFound",_sharedLocalizer["GlobalForm.Response.UserNotFound"]}//+
                };

                var error = Utilities.CreateErrorMessage("UserNotFound", dicError);
                return new LoginResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Errors = error
                };
            }

            #region block user 

            if (curUser.IsActive != null && !curUser.IsActive.GetValueOrDefault())
            {
                var dicError = new Dictionary<string, string>() {
                    { "UserBlock",_sharedLocalizer["GlobalForm.Response.UserBlock"]}
                };
                var error = Utilities.CreateErrorMessage("UserBlock", dicError);

                return new LoginResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Message = "",
                    Errors = error
                };
            }

            if (!curUser.IsVerify.GetValueOrDefault())
            {
                var dicError = new Dictionary<string, string>() {
                    { "BeforVerify",_sharedLocalizer["GlobalForm.Response.NotVerify"]}
                };
                var error = Utilities.CreateErrorMessage("BeforVerify", dicError);

                return new LoginResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Message = "",
                    Errors = error
                };
            }

            #endregion

            #endregion

            //بدست اورن بخش ها و پرمیزن ها بر اساس رول کاربر
            //var sections = await GetSections(curUser.Id);

            #region cache permission

            // await CacheUserPermission(token, curUser);

            #endregion

            #region add login

            var userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

            var uaParser = Parser.GetDefault();
            ClientInfo clientInfo = uaParser.Parse(userAgent);
            //زمانی که زبانی انتخاب نشود ما زبان دیفالت ریسورت را در جدول لاگین ثبت میکنیم
            if (string.IsNullOrEmpty(loginDto.Language))
                loginDto.Language = _locOption.Value.DefaultRequestCulture.Culture.Name;


            await _loginRepository.AddAsync(new Login
            {
                StartDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddHours(24),
                Ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                Browser = clientInfo.UserAgent.Family,
                Token = token,
                UserId = curUser.Id,
                Language = loginDto.Language,
                CurrentSectionId = curUser.UserRolePermission.Select(g => g.SectionId).FirstOrDefault(),
                RoleId = curUser.UserRolePermission.Select(g => g.RoleId).FirstOrDefault()
            });

            await _unitOfWork.SaveChangesAsync();

            #endregion

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = new
                {
                    Token = token.ToString(),
                    UserInfo = new UserInfoDto
                    {
                        FullName = curUser.Person.FullName,
                        UserName = curUser.UserName,
                        RoleId = curUser.UserRolePermission.Select(g => g.RoleId.ToString()).FirstOrDefault(),
                        SectionId = curUser.UserRolePermission.Select(g => g.Section.LocalCode).FirstOrDefault(),
                        RoleCode = curUser.UserRolePermission.Select(g => g.Role.Code1.ToString()).FirstOrDefault(),
                        RoleName = curUser.UserRolePermission.Select(g => g.Role.Title).FirstOrDefault(),
                        CenterId = curUser.UserRolePermission.Select(g => g.Section.CenterId).FirstOrDefault(),
                        NhsNumber = curUser?.CardCode?.HealthNumber,
                        CardExpireDate = curUser.UserCardCode.Select(g => g.ExpireDate.ToDateStringTry()).FirstOrDefault(),
                        Permissions = GetUserPermission(curUser)
                    }
                }
            };

        }

        public async Task<BaseResponseDto> RegisterAsync(UserRegisterDto userDto)
        {
            var resultValid = CheckValidate.Valid<UserRegisterDto>(new RegisterUserValidation(_sharedLocalizer, _userRepository, _httpContextAccessor.HttpContext), userDto);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                return resultValid;
            }

            var curUser = await _userRepository.Where(d => d.UserName == userDto.Mobile).FirstOrDefaultAsync();
            //چک تکراری بودن کاربر
            if (curUser == null)
            {
                //چک می کنیم همچین موبایلی با کد وریفای یکی است
                if (!await _mobileActivationRepository.AnyAsync(d => d.Mobile == userDto.Mobile && d.VerifyCode == userDto.VerifyCode))
                {
                    var dicError = new Dictionary<string, string>() {
                    { "NotFound",_sharedLocalizer["GlobalForm.Response.VerifyCodeNotFound"]}
                };
                    var error = Utilities.CreateErrorMessage("VerifyCodeNotFound", dicError);

                    return new LoginResponseDto
                    {
                        Status = ResponseStatus.NotFound,
                        Errors = error
                    };
                }
                // نکته یک بخش ویزه بیماران درست کردیم و همان را ست میکنیم
                //کاربر هم ثبت میشه هم وریفای
                var hashPassword = Utilities.ComputeHashSHA256(userDto.Password);
                userDto.Password = hashPassword;

                //قبل ثبت نام چک میکنیم همچین پرسنی وجود داردیا نه اگر داشت دیگه پرسن ثبت نمیکنیم


                var curPerson = await GetPersonExist(userDto.Mobile);

                var map = UserRegisterMapper.Map(userDto, curPerson);
                map.IsActive = true;
                map.IsVerify = true;



                _userRepository.Add(map);

                await _unitOfWork.SaveChangesAsync();

            }
            else
            {
                if (curUser.IsActive != null && !curUser.IsActive.GetValueOrDefault())
                {
                    var dicError = new Dictionary<string, string>() {
                    { "UserBlock",_sharedLocalizer["GlobalForm.Response.UserBlock"]}
                };
                    var error = Utilities.CreateErrorMessage("UserBlock", dicError);

                    return new LoginResponseDto
                    {
                        Status = ResponseStatus.NotValid,
                        Message = "",
                        Errors = error
                    };
                }

                if (curUser.IsVerify.GetValueOrDefault())
                {
                    var dicError = new Dictionary<string, string>() {
                    { "BeforVerify",_sharedLocalizer["GlobalForm.Response.BeforVerify"]}
                };
                    var error = Utilities.CreateErrorMessage("BeforVerify", dicError);

                    return new LoginResponseDto
                    {
                        Status = ResponseStatus.NotValid,
                        Message = "",
                        Errors = error
                    };
                }

                //var result = await CheckLimitedVerifyCode(userDto.Mobile);

                //if (result.Status != ResponseStatus.Success)
                //{
                //    return result;
                //}

                //var code = Utilities.GenerateVerifyCode();

                //await _smsService.SendSms(new SendSmsDto { Mobile = userDto.Mobile, Message = ResponseVerifyCode(code) });

                //var mapmmob = MobileActivationMapper.Map(new RequestVerifyDto { Mobile = userDto.Mobile, VerifyCode = code });



                //curUser.Password = Utilities.ComputeHashSHA256(userDto.Password);


                //_mobileActivationRepository.Add(mapmmob);

                //await _unitOfWork.SaveChangesAsync();

                return new BaseResponseDto
                {
                    Status = ResponseStatus.Success,
                    //                    Message

                };
            }


            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["RegisterForm.Response.RegisterSuccess"]
            };

        }

        public async Task<BaseResponseDto> RegisterForCenter(UserCenterRegisterDto userDto)
        {
            var resultValid = CheckValidate.Valid<UserCenterRegisterDto>(new UserCenterRegisterValidation(_sharedLocalizer, _userRepository, _httpContextAccessor.HttpContext), userDto);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                return resultValid;
            }

            //برای هر بخش یک یوز  یوزر پرمیشن ایجاد میکنیم 

            if (userDto.Id == 0)
            {
                var section = await _sectionRepository.Where(d => d.CenterId == userDto.CenterId)
                                                        .Include(d => d.Center)
                                                        .FirstOrDefaultAsync();

                var role = Utilities.GetRoleId((CenterTypeEnum)section.Center.CenterTypeId);

                var map = UserRegisterMapper.MapUserForCenter(userDto, role, section.Id, null);
                map.IsActive = true;
                map.IsVerify = true;
                _userRepository.Add(map);
            }
            else
            {
                var curUser = await _userRepository.Where(d => d.Id == userDto.Id).FirstOrDefaultAsync();

                curUser.UserName = userDto.UserName;

                if (!string.IsNullOrEmpty(userDto.Password))
                    curUser.Password = Utilities.ComputeHashSHA256(userDto.Password);

                _userRepository.Update(curUser);
            }

            await _unitOfWork.SaveChangesAsync();


            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["RegisterForm.Response.UserRegisterSuccess"]//+
            };
        }

        public async Task<BaseResponseDto> Logout(Guid token)
        {
            var curLogin = await _loginRepository.FirstOrDefaultAsync(x => x.Token == token);
            curLogin.ExpireDate = DateTime.Now;
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = "Logout User"
            };
        }

        public async Task<Login> CheckToken(Guid token)
        {
            Login login = await GetLoginByToken(token);


            //_userRepository.Where(d => d.Id == login.UserId).Include(d => d.Person);

            if (login is null)
                return null;
            else
            {
                if (login.ExpireDate > DateTime.Now)
                {
                    return login;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<BaseResponseDto> Verify(RequestVerifyDto requestVerifyDto)
        {
            if (await _mobileActivationRepository.AnyAsync(d => d.Mobile == requestVerifyDto.Mobile && d.VerifyCode == requestVerifyDto.VerifyCode))
            {
                var curUser = await _userRepository.Where(d => d.UserName == requestVerifyDto.Mobile).FirstOrDefaultAsync();
                curUser.IsVerify = true;
                curUser.IsActive = true;

                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                var dicError = new Dictionary<string, string>() {
                    { "NotFound",_sharedLocalizer["GlobalForm.Response.UserNotFound"]}
                };
                var error = Utilities.CreateErrorMessage("NotFound", dicError);

                return new LoginResponseDto
                {
                    Status = ResponseStatus.NotFound,
                    Errors = error
                };
            }

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["VerifyForm.Response.VerifySuccess"]
            };
        }

        public async Task<BaseResponseDto> GetCaptcha()
        {
            int width = 150;
            int height = 50;
            var captchaCode = Captcha.GenerateCaptchaCode();
            var result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            _httpContextAccessor.HttpContext.Session.SetString("CaptchaCode", result.CaptchaCode);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = result.CaptchBase64Data
            };
        }

        public async Task<BaseResponseDto> GenerateVerify(RequestMobileVerifyDto requestVerifyDto)
        {
            //if (await _userRepository.AnyAsync(d => d.UserName == requestVerifyDto.Mobile))
            //{

            var result = await CheckLimitedVerifyCode(requestVerifyDto.Mobile);

            if (result.Status != ResponseStatus.Success)
            {
                return result;
            }

            var code = Utilities.GenerateVerifyCode();
            var map = MobileActivationMapper.Map(new RequestVerifyDto { Mobile = requestVerifyDto.Mobile, VerifyCode = code });
            _mobileActivationRepository.Add(map);
            await _unitOfWork.SaveChangesAsync();

            await _smsService.SendSms(new SendSmsDto { Message = ResponseVerifyCode(code), Mobile = requestVerifyDto.Mobile });

            //}
            //else
            //{
            //    var dicError = new Dictionary<string, string>() {
            //        { "NotFound",_sharedLocalizer["LoginForm.Response.UserNotFound"]}//+
            //    };

            //    var error = Utilities.CreateErrorMessage("UserNotFound", dicError);
            //    return new BaseResponseDto
            //    {
            //        Status = ResponseStatus.NotValid,
            //        Message = _sharedLocalizer["LoginForm.Response.UserNotFound"],//+
            //        Errors = error
            //    };


            //}

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["VerifyForm.Response.SendSms"]//+
            };
        }

        public async Task<BaseResponseDto> ForgetPassword(RequestForgetPasswordyDto requestForgetPasswordyDto)
        {
            var curUser = await _userRepository.Where(d => d.UserName == requestForgetPasswordyDto.Mobile).FirstOrDefaultAsync();

            if (curUser != null)
            {
                if (!await _mobileActivationRepository.AnyAsync(d => d.Mobile == requestForgetPasswordyDto.Mobile && d.VerifyCode == requestForgetPasswordyDto.VerifyCode))
                {
                    var dicError = new Dictionary<string, string>() {
                    { "NotFound",_sharedLocalizer["GlobalForm.Response.VerifyCodeNotFound"]}
                    };
                    var error = Utilities.CreateErrorMessage("VerifyCodeNotFound", dicError);

                    return new LoginResponseDto
                    {
                        Status = ResponseStatus.NotFound,
                        Errors = error
                    };
                }


                //var password = Utilities.GenerateRandomNumber(8);
                var hash = Utilities.ComputeHashSHA256(requestForgetPasswordyDto.Password);

                curUser.Password = hash;

                //بعد تغییر پسورد تاریچه تولید پسور راهم ذخیره میکنیم
                // var map = MobileActivationMapper.Map(new RequestVerifyDto { Mobile = requestVerifyDto.Mobile, VerifyCode = requestVerifyDto.Password });
                //  _mobileActivationRepository.Add(map);
                await _unitOfWork.SaveChangesAsync();

                // await _smsService.SendSms(new SendSmsDto { Mobile = requestVerifyDto.Mobile, Message = _sharedLocalizer["SendSms.Response.NewPassword"].Value.Replace("{code}", password) });
            }
            else
            {
                var dicError = new Dictionary<string, string>() {
                    { "UserNotFounds",_sharedLocalizer["GlobalForm.Response.UserNotFound"]}//+
                };

                var error = Utilities.CreateErrorMessage("UserNotFound", dicError);
                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotFound,
                    Errors = error
                };
            }

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["ForgetPasswordForm.Response.Success"]//+

            };
        }

        #region old action service

        public async Task<Login> GetLoginByToken(Guid token)
        {
            return await _loginRepository.FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task<BaseResponseDto> GetById(int userId)
        {
            var curUser = await _userRepository.Where(d => d.Id == userId)
                                                .Include(d => d.Person)
                                                // .Select(g => Mapper.UserMapper.Map(g))
                                                .FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = curUser
            };
        }


        //public async Task<ListResponseDto> Search(UserFilterDto userFilterDto)
        //{
        //    // validate paging post
        //    //var validator = new BaseRequestPagingValidator("PrescriptionServiceChart", _sharedLocalizer);
        //    //var resultValidator = CheckValidate.Valid(validator, filterBasketDrugDto);
        //    //if (resultValidator.Status == ResponseStatus.NotValid) { return Mapper.BaseResponseMapper.Map(resultValidator); }

        //    // create query
        //    IQueryable<Users> query = _userRepository.Include(x => x.Person);

        //    // query filters
        //    if (userFilterDto.IdList?.Count > 0) query = query.Where(q => userFilterDto.IdList.Contains(q.Id));


        //    // query paging
        //    query = query.ToPagedQuery(userFilterDto.PageSize, userFilterDto.PageNumber);

        //    // result
        //    return new ListResponseDto
        //    {
        //        Status = ResponseStatus.Success,
        //        Count = await query.CountAsync(),
        //        Data = await query.Select(x => Mapper.UserMapper.MapFullName(x)).ToListAsync()
        //    };
        //}

        //public async Task<BaseResponseDto> EditAsync(UserRegisterDto userDto)
        //{
        //    var resultValid = CheckValidate.Valid<UserRegisterDto>(new UserValidation(_sharedLocalizer), userDto);
        //    if (resultValid.Status == ResponseStatus.NotValid)
        //    {
        //        return resultValid;
        //    }

        //    var curUser = await _userRepository.Where(d => d.Id == userDto.Id).FirstOrDefaultAsync();

        //    if (!string.IsNullOrEmpty(userDto.Password))
        //    {
        //        var hashPassword = Utilities.ComputeHash(userDto.Password, new SHA256CryptoServiceProvider()).Replace("-", "");
        //        userDto.Password = hashPassword;
        //    }

        //    var map = Mapper.UserMapper.Map(curUser, userDto);

        //    _userRepository.Update(map);
        //    await _unitOfWork.SaveChangesAsync();

        //    //get list Users
        //    var result = await ListUser(new BaseRequestPost<int> { PageNumber = userDto.PageNumber, PageSize = userDto.PageSize });
        //    result.Message = _sharedLocalizer["UserForm.Response.RegisterUserSuccess"];//+

        //    return result;


        //}

        public async Task<ListResponseDto> ListUser(BaseRequestPost<int> baseRequestPost)
        {
            var lstUsers = await _userRepository
                .ToPagedQuery(baseRequestPost).OrderByDescending(d => d.Id)
                .ToListAsync();

            return new ListResponseDto
            {
                Data = lstUsers,
                Status = ResponseStatus.Success,
                Count = _userRepository.Count()
            };
        }

        public async Task<BaseResponseDto> SetLanguage(Guid token, string language)
        {
            var curLogin = await _loginRepository.Where(u => u.Token == token).FirstOrDefaultAsync();
            curLogin.Language = language;

            _loginRepository.Update(curLogin);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["GlobalForm.Response.SetLangugeSuccess"]//+
            };
        }

        public async Task<BaseResponseDto> SetSection(Guid token, SetSectionDto setSectionDto)
        {
            //get sections permission
            var curLogin = await _loginRepository.Where(u => u.Token == token).FirstOrDefaultAsync();

            var curPermission = await _userRepository.Where(d => d.Id == curLogin.UserId && d.UserPermission.Any(a => a.SectionId == setSectionDto.SectionId)).FirstOrDefaultAsync();

            if (curPermission is null)
            {
                var dicError = new Dictionary<string, string>() {
                    { "NotFoundSection",_sharedLocalizer["SectionForm.Response.NotAccessSection"]}//+
                };
                var error = Utilities.CreateErrorMessage("NotAccessSection", dicError);

                return new LoginResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Message = "",
                    Errors = error
                };
            }

            RequestSetSectionDto requestSetSectionDto = new RequestSetSectionDto();

            curLogin.CurrentSectionId = setSectionDto.SectionId;
            curLogin.RoleId = setSectionDto.RoleId;

            _loginRepository.Update(curLogin);
            await _unitOfWork.SaveChangesAsync();

            //  requestSetSectionDto.Menu = (await _menuService.GetAllByUserId(new DTO.MenuGetDto { UserId = _workContext.UserId.GetValueOrDefault(), SectionId = setSectionDto.SectionId })).Data;

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["SectionForm.Response.SetSectionSuccess"],//+
                Data = requestSetSectionDto
            };
        }

        //public async Task<List<PermissionDto>> GetPermissionsAsync(string token)
        //{
        //    var curLogin = await _loginRepository.Where(d => d.Token == Guid.Parse(token)).FirstOrDefaultAsync();
        //    var curUser = await _userRepository
        //      .Include(u => u.UserPermission)
        //      .ThenInclude(up => up.Permission)
        //      .Include(u => u.UserPermission)
        //      .ThenInclude(up => up.Section)
        //      .Where(u => u.PersonId == curLogin.UserId)
        //      .FirstOrDefaultAsync();

        //    var lstPermissionForCache = curUser.UserPermission.Select(g => new PermissionDto
        //    {
        //        PermissionName = g.Permission.Title,
        //        PermissionId = g.PermissionId,
        //        SectionId = g.SectionId,
        //        UserId = g.UserId,
        //        PageAdress = g.Permission.PageAddress
        //    });

        //    return lstPermissionForCache.ToList();
        //}

        public async Task<List<PermissionDto>> GetPermissionsAsync(int? userid)
        {
            var curUser = await _userRepository
              .Include(u => u.UserPermission)
              .ThenInclude(up => up.Permission)
              .Include(u => u.UserPermission)
              .ThenInclude(up => up.Section)
              .Where(u => u.PersonId == userid)
              .FirstOrDefaultAsync();

            var lstPermissionForCache = curUser.UserPermission.Select(g => new PermissionDto
            {
                PermissionName = g.Permission.Title,
                PermissionId = g.PermissionId,
                SectionId = g.SectionId,
                UserId = g.UserId,
                PageAdress = g.Permission.PageAddress
            });

            return lstPermissionForCache.ToList();
        }

        //public async Task<ListResponseDto> ListPermission(BaseRequestPost<int> baseRequestPost)
        //{
        //    var lst = await _PermissionRepository.ToPagedQuery(baseRequestPost.PageSize, baseRequestPost.PageNumber).Select(g => new ResponsePermissionDto
        //    {
        //        Id = g.Id,
        //        Title = g.Title
        //    }).ToListAsync();


        //    return new ListResponseDto
        //    {
        //        Data = lst,
        //        Status = ResponseStatus.Success,
        //        Count = await _PermissionRepository.ToPagedQuery(baseRequestPost.PageSize, baseRequestPost.PageNumber).CountAsync()
        //    };
        //}

        //public async Task<ListResponseDto> ListPermissionForUser(RequestUserPermissionDto requestUserPermissionDto)
        //{
        //    var lstPermissonForUser = await _userPermissionRepository.Where(d => d.SectionId == requestUserPermissionDto.SectionId && d.UserId == requestUserPermissionDto.UserId)
        //                                .Include(d => d.Permission)
        //                                .Select(g => new ResponsePermissionDto
        //                                {
        //                                    Id = g.PermissionId,
        //                                    Title = g.Permission.Title
        //                                }).ToListAsync();

        //    //get all id use user
        //    var ids = lstPermissonForUser.Select(t => t.Id).ToList();

        //    //
        //    var lstPermissionNotUse = await _PermissionRepository
        //        .Where(d => !ids.Contains(d.Id))
        //        .ToPagedQuery(requestUserPermissionDto.PageSize, requestUserPermissionDto.PageNumber).Select(g => new ResponsePermissionDto
        //        {
        //            Id = g.Id,
        //            Title = g.Title
        //        }).ToListAsync();

        //    return new ListResponseDto
        //    {
        //        Data = new { UserPermission = lstPermissonForUser, NotUserPermission = lstPermissionNotUse },
        //        Status = ResponseStatus.Success,
        //        Count = await _userPermissionRepository.Where(d => d.SectionId == requestUserPermissionDto.SectionId && d.UserId == requestUserPermissionDto.UserId).CountAsync()
        //    };
        //}

        //public async Task<BaseResponseDto> AddUserPermission(AddOrDeleteUserPermissionDto addOrDeleteUserPermissionDto)
        //{
        //    var resultValid = CheckValidate.Valid<AddOrDeleteUserPermissionDto>(new UserPermissionValidation(_sharedLocalizer), addOrDeleteUserPermissionDto);
        //    if (resultValid.Status == ResponseStatus.NotValid)
        //    {
        //        return resultValid;
        //    }

        //    var lstPermisson = await _userPermissionRepository.Where(d => d.SectionId == addOrDeleteUserPermissionDto.SectionId && d.UserId == addOrDeleteUserPermissionDto.UserId).ToListAsync();


        //    foreach (var id in addOrDeleteUserPermissionDto.PermissionIds)
        //    {
        //        var curPerm = lstPermisson.Where(d => d.Id == id).FirstOrDefault();

        //        //exist permission for user
        //        if (curPerm != null)
        //            continue;

        //        _userPermissionRepository.Add(
        //                    new UserPermission
        //                    {
        //                        PermissionId = id,
        //                        SectionId = addOrDeleteUserPermissionDto.SectionId,
        //                        UserId = addOrDeleteUserPermissionDto.UserId
        //                    }
        //            );
        //    }

        //    await _unitOfWork.SaveChangesAsync();

        //    //get all permission use and not use for current  user
        //    var userPermission = await ListPermissionForUser(new RequestUserPermissionDto { UserId = addOrDeleteUserPermissionDto.UserId, SectionId = addOrDeleteUserPermissionDto.SectionId, PageNumber = addOrDeleteUserPermissionDto.PageNumber, PageSize = addOrDeleteUserPermissionDto.PageSize });

        //    return new ListResponseDto
        //    {
        //        Status = ResponseStatus.Success,
        //        Data = userPermission.Data
        //    };
        //}

        //public async Task<BaseResponseDto> DeleteUserPermission(AddOrDeleteUserPermissionDto addOrDeleteUserPermissionDto)
        //{
        //    var resultValid = CheckValidate.Valid<AddOrDeleteUserPermissionDto>(new UserPermissionValidation(_sharedLocalizer), addOrDeleteUserPermissionDto);
        //    if (resultValid.Status == ResponseStatus.NotValid)
        //    {
        //        return resultValid;
        //    }
        //    //لیست پرمیژن های کاربر در سکشن مربوط
        //    var lstForUserPermisson = await _userPermissionRepository.Where(d => d.SectionId == addOrDeleteUserPermissionDto.SectionId && d.UserId == addOrDeleteUserPermissionDto.UserId).ToListAsync();

        //    //delete permisson by ids
        //    foreach (var item in addOrDeleteUserPermissionDto.PermissionIds)
        //    {
        //        var curPerm = lstForUserPermisson.Where(d => d.PermissionId == item && d.SectionId == addOrDeleteUserPermissionDto.SectionId && d.UserId == addOrDeleteUserPermissionDto.UserId).FirstOrDefault();
        //        if (curPerm != null)
        //            _userPermissionRepository.Remove(curPerm);
        //    }

        //    await _unitOfWork.SaveChangesAsync();


        //    //get all permission use and not use for current  user
        //    var userPermission = await ListPermissionForUser(new RequestUserPermissionDto { UserId = addOrDeleteUserPermissionDto.UserId, SectionId = addOrDeleteUserPermissionDto.SectionId, PageNumber = addOrDeleteUserPermissionDto.PageNumber, PageSize = addOrDeleteUserPermissionDto.PageSize });


        //    return new ListResponseDto
        //    {
        //        Status = ResponseStatus.Success,
        //        Data = userPermission.Data
        //    };
        //}

        public async Task<BaseResponseDto> GetAll()
        {
            var lstUsers = await _userRepository
                .Include(x => x.Person)
                // .Select(Mapper.UserMapper.MapList)
                .ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lstUsers
            };
        }

        public async Task<BaseResponseDto> GetAllbyCurrentSection()
        {
            var lstUsers = await _userRepository
                .Include(x => x.Person)
                .Where(d => d.UserRolePermission.Any(t => t.SectionId == _workContext.SectionId))
                 .Select(g => new
                 {
                     g.Person.FullName,
                     g.Id
                 })
                .ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lstUsers
            };
        }

        #endregion

        //#region role  user

        //public async Task<BaseResponseDto> AddUserRole(AddOrDeleteUserRoleDto addOrDeleteUserRoleDto)
        //{
        //    //var resultValid = CheckValidate.Valid<AddOrDeleteUserPermissionDto>(new UserPermissionValidation(_sharedLocalizer), addOrDeleteUserPermissionDto);
        //    //if (resultValid.Status == ResponseStatus.NotValid)
        //    //{
        //    //    return resultValid;
        //    //}

        //    var lstRoles = await _userRolesRepository.Where(d => d.UserId == addOrDeleteUserRoleDto.UserId).ToListAsync();


        //    foreach (var id in addOrDeleteUserRoleDto.RoleIds)
        //    {
        //        var curPerm = lstRoles.Where(d => d.Id == id).FirstOrDefault();

        //        //exist permission for user
        //        if (curPerm != null)
        //            continue;

        //        _userRolesRepository.Add(
        //                    new UserRole
        //                    {
        //                        RoleId = id,
        //                        UserId = addOrDeleteUserRoleDto.UserId
        //                    }
        //            );
        //    }

        //    await _unitOfWork.SaveChangesAsync();

        //    //get all permission use and not use for current  user

        //    var result = await ListRoleForUser(new RequestUserRoleDto { UserId = addOrDeleteUserRoleDto.UserId, PageSize = addOrDeleteUserRoleDto.PageSize, PageNumber = addOrDeleteUserRoleDto.PageSize });

        //    return new ListResponseDto
        //    {
        //        Status = ResponseStatus.Success,
        //        Data = result.Data

        //    };
        //}

        //public async Task<BaseResponseDto> DeleteUserRole(AddOrDeleteUserRoleDto addOrDeleteUserRoleDto)
        //{
        //    //var resultValid = CheckValidate.Valid<AddOrDeleteUserPermissionDto>(new UserPermissionValidation(_sharedLocalizer), addOrDeleteUserPermissionDto);
        //    //if (resultValid.Status == ResponseStatus.NotValid)
        //    //{
        //    //    return resultValid;
        //    //}

        //    var lstrolesUser = await _userRolesRepository.Where(d => d.UserId == addOrDeleteUserRoleDto.UserId).ToListAsync();

        //    //delete role by ids
        //    foreach (var id in addOrDeleteUserRoleDto.RoleIds)
        //    {
        //        var curPerm = lstrolesUser.Where(d => d.RoleId == id && d.UserId == addOrDeleteUserRoleDto.UserId).FirstOrDefault();
        //        if (curPerm != null)
        //            _userRolesRepository.Remove(curPerm);
        //    }

        //    await _unitOfWork.SaveChangesAsync();


        //    //get all permission use and not use for current  user

        //    var result = await ListRoleForUser(new RequestUserRoleDto { UserId = addOrDeleteUserRoleDto.UserId, PageSize = addOrDeleteUserRoleDto.PageSize, PageNumber = addOrDeleteUserRoleDto.PageSize });

        //    return new ListResponseDto
        //    {
        //        Status = ResponseStatus.Success,
        //        Data = result.Data

        //    };
        //}

        //public async Task<ListResponseDto> ListRoleForUser(RequestUserRoleDto requestUserRoleDto)
        //{
        //    var lstRoleForUser = await _userRolesRepository.Where(d => d.UserId == requestUserRoleDto.UserId)
        //                                .Include(d => d.Role)
        //                                .Select(g => new ResponsePermissionDto
        //                                {
        //                                    Id = g.RoleId,
        //                                    Title = g.Role.Title
        //                                }).ToListAsync();

        //    //get all id use user
        //    var ids = lstRoleForUser.Select(t => t.Id).ToList();

        //    //
        //    var lstRoleNotUse = await _roleRepository
        //        .Where(d => !ids.Contains(d.Id))
        //        .ToPagedQuery(requestUserRoleDto.PageSize, requestUserRoleDto.PageNumber).Select(g => new ResponsePermissionDto
        //        {
        //            Id = g.Id,
        //            Title = g.Title
        //        }).ToListAsync();

        //    return new ListResponseDto
        //    {
        //        Data = new { UserRole = lstRoleForUser, NotUserRole = lstRoleNotUse },
        //        Status = ResponseStatus.Success,
        //        Count = await _userRolesRepository.Where(d => d.UserId == requestUserRoleDto.UserId).CountAsync()
        //    };
        //}

        //public async Task<BaseResponseDto> UserPerson(List<int> userId)
        //{
        //    var lst = await _userRepository.Where(d => userId.Contains(d.Id))
        //        .Select(g => new UserPersonDto
        //        {
        //            UserId = g.Id,
        //            Age = g.Person.Age,
        //            BrithDate = g.Person.BirthDate.ToDateStringTry(),
        //            FullName = g.Person.FullName,
        //            Sex = g.Person.Sex != null ? g.Person.Sex.Title : "",
        //            FileNo = g.Person.Patient.Any() ? g.Person.Patient.Select(t => t.FileNo).FirstOrDefault() : "",
        //            InternalId = g.Person.Patient.Any() ? g.Person.Patient.Select(t => t.InternalId).FirstOrDefault() : null
        //        })
        //        .AsNoTracking()
        //        .ToListAsync();

        //    return new BaseResponseDto
        //    {
        //        Data = lst,
        //        Status = ResponseStatus.Success
        //    };
        //}

        //#endregion


        #region user pmethods


        private async Task CacheUserPermission(Guid token, Users curUser)
        {
            // curUser.UserPermission
            var lstPermissionForCache = curUser.UserRolePermission.Select(g => new PermissionDto
            {
                PermissionName = g.Permission.Title,
                PermissionId = g.PermissionId,
                SectionId = g.SectionId,
                UserId = g.UserId,
                RoleId = g.RoleId,
                PageAdress = g.Permission.PageAddress
            });

        }

        private async Task<List<ListSectionDto>> GetSections(int userId)
        {
            var sections = await _userRolePermissionsRepository.Where(d => d.UserId == userId)
               .GroupBy(d => new { d.RoleId, d.SectionId })
               .Select(g => new ListSectionDto
               {
                   Id = g.Key.SectionId.GetValueOrDefault(),
                   Title = $"{g.Where(t => t.RoleId == g.Key.RoleId).Select(x => x.Section.Title).FirstOrDefault()} - {g.Where(t => t.RoleId == g.Key.RoleId).Select(x => x.Role.Title).FirstOrDefault()}",
                   Section = g.Where(t => t.SectionId == g.Key.SectionId).Select(x => x.Section.Title).FirstOrDefault(),
                   Role = g.Where(t => t.RoleId == g.Key.RoleId).Select(x => x.Role.Title).FirstOrDefault(),
                   RoleId = g.Key.RoleId.GetValueOrDefault(),
                   //PageNames = tempSections.Where(d => d.SectionId == g.Id).GroupBy(d => d.Permission.PageAddress).Select(t => new PageNameDto
                   //{
                   //    PageName = t.Key,
                   //    ModuleName = t.Select(x => x.Permission.ModuleName)
                   //}).ToList()
               })
               .AsNoTracking()
               .ToListAsync();

            return sections;
        }

        private object GetUserPermission(Users curUser)
        {
            // curUser.UserPermission

            return curUser?.UserRolePermission?.Select(g => new
            {
                PermissionName = g.Permission != null ? g?.Permission.Title : "",
                PermissionId = g.Permission != null ? g?.PermissionId : null,
                ModuleName = g.Permission != null ? g?.Permission?.ModuleName : null,
                PageAdress = g.Permission != null ? g?.Permission?.PageAddress : null
            }).Where(d => d.PermissionId != null).ToList();
        }
        public string ResponseVerifyCode(string code)
        {
            return _sharedLocalizer["SendSms.Response.VerifyCode"].Value.Replace("{code}", code);
        }

        #endregion

        public async Task<BaseResponseDto> CheckLimitedVerifyCode(string mobile)
        {
            var todaysDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            var daycount = await _mobileActivationRepository.Where(d => d.Mobile == mobile && d.CreateDate.Value.Date == todaysDate).CountAsync();
            var totalcount = await _mobileActivationRepository.Where(d => d.Mobile == mobile).CountAsync();

            if (_confiSmsDto.Value.LimitedSendSmsOnDay <= daycount)
            {
                var dicError = new Dictionary<string, string>() {
                    { "VerifyCodeLimited",_sharedLocalizer["GlobalForm.Response.VerifyCodeLimitedOnDay"]}//+
                    };

                var error = Utilities.CreateErrorMessage("VerifyCodeLimited", dicError);
                return new BaseResponseDto
                {
                    Status = ResponseStatus.Fail,
                    Errors = error
                };
            }

            if (_confiSmsDto.Value.LimitedSendSmsTotal <= totalcount)
            {
                var dicError = new Dictionary<string, string>() {
                    { "VerifyCodeLimited",_sharedLocalizer["GlobalForm.Response.VerifyCodeLimitedTotal"]}//+
                    };


                var error = Utilities.CreateErrorMessage("VerifyCodeLimited", dicError);

                return new BaseResponseDto
                {
                    Status = ResponseStatus.Fail,
                    Errors = error
                };
            }

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success
            };
        }

        public async Task<BaseResponseDto> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var resultValid = CheckValidate.Valid<ChangePasswordDto>(new ChangePasswordValidation(_sharedLocalizer, _userRepository, _workContext), changePasswordDto);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                return resultValid;
            }

            var userId = _workContext.UserId;

            var curUser = await _userRepository.Where(d => d.Id == userId).FirstOrDefaultAsync();

            var hash = Utilities.ComputeHashSHA256(changePasswordDto.NewPassword);

            curUser.Password = hash;

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["ChangePasswordForm.Response.ChangeSuccess"]
            };//+
        }

        public async Task<BaseResponseDto> GetUsersByMobile(string mobile)
        {
            var lstUsers = await _userRepository.Where(d => d.UserName == mobile)
                .Select(g => new
                {
                    g.Id,
                    g.Person.FullName,
                })
                .AsNoTracking()
                .ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lstUsers
            };
        }

        public async Task<BaseResponseDto> CheckExistUser(RequestMobileVerifyDto requestMobileVerifyDto)
        {
            var cur = await _userRepository.Where(d => d.UserName == requestMobileVerifyDto.Mobile).AsNoTracking().FirstOrDefaultAsync();
            if (cur != null)
            {

                return new BaseResponseDto
                {
                    Status = ResponseStatus.Success

                };
            }
            else
            {
                var result = await CheckLimitedVerifyCode(requestMobileVerifyDto.Mobile);

                if (result.Status != ResponseStatus.Success)
                {
                    return result;
                }

                var code = Utilities.GenerateVerifyCode();

                _mobileActivationRepository.Add(new MobileActivation
                {
                    CreateDate = DateTime.Now,
                    ExpireDate = DateTime.Now.AddDays(5),
                    Mobile = requestMobileVerifyDto.Mobile,
                    VerifyCode = code
                });



                var resultSms = await _smsService.SendSms(new SendSmsDto { Mobile = requestMobileVerifyDto.Mobile, Message = ResponseVerifyCode(code) });

                if (resultSms.Status == ResponseStatus.Fail)
                {
                    var dicError = new Dictionary<string, string>() {
                    { "SmsNotSend",_sharedLocalizer["GlobalForm.Response.SmsNotSend"]}
                        };
                    var error = Utilities.CreateErrorMessage("SmsNotSend", dicError);

                    return new BaseResponseDto
                    {
                        Status = ResponseStatus.Fail,
                        Errors = error
                    };
                }

                await _unitOfWork.SaveChangesAsync();

                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotFound

                };
            }
        }

        public async Task<BaseResponseDto> CheckVerifyUser(RequestVerifyDto requestVerifyDto)
        {
            if (await _mobileActivationRepository.AnyAsync(d => d.Mobile == requestVerifyDto.Mobile && d.VerifyCode == requestVerifyDto.VerifyCode))
            {
                return new BaseResponseDto
                {
                    Status = ResponseStatus.Success
                };
            }
            else
            {
                var dicError = new Dictionary<string, string>() {
                    { "NotFound",_sharedLocalizer["GlobalForm.Response.UserNotVerify"]}
                };
                var error = Utilities.CreateErrorMessage("NotFound", dicError);

                return new LoginResponseDto
                {
                    Status = ResponseStatus.NotFound,
                    Errors = error
                };
            }


        }

        public async Task<BaseResponseDto> CurrentUser()
        {
            var cur = await _userRepository.Where(d => d.Id == _workContext.UserId)
                                            .Include(d => d.Person)
                                            .ThenInclude(d => d.Sex)
                                            .Include(d => d.Person.BirthPlace)
                                            .Include(d => d.Person.MaritalStatus)
                                            .Include(d => d.Person.Patient)
                                            .ThenInclude(d => d.BloodGroup)
                                            .Include(d => d.Person.Patient)
                                            .ThenInclude(d => d.PatientExtraInfo)
                                            .Include(d => d.UserCardCode).ThenInclude(x=>x.CardCode)
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync();

            var map = UserRegisterMapper.UserMap(cur);

            var file = await _filesService.GetFilesByFileGroupId(1, nameof(Person), cur.PersonId.ToString());

            map.FilteId = file.OrderByDescending(d => d.Id).Select(g => g.RefferKey).FirstOrDefault();
            map.FileId = file.OrderByDescending(d => d.Id).Select(g => g.RefferKey).FirstOrDefault();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = map,
            };
        }

        public async Task<BaseResponseDto> UpdateCurrentUser(UserPersonDto userPersonDto)
        {
            var resultValid = CheckValidate.Valid(new ProfileValidation(_sharedLocalizer), userPersonDto);
            if (resultValid.Status == ResponseStatus.NotValid) return resultValid;

            var cur = await _userRepository.Where(d => d.Id == _workContext.UserId)
                                            .Include(d => d.Person)
                                            .ThenInclude(d => d.Patient)
                                            .ThenInclude(d => d.PatientExtraInfo)
                                            .FirstOrDefaultAsync();

            var map = UserRegisterMapper.UserMap(cur, userPersonDto);

            _userRepository.Update(map);
            await _unitOfWork.SaveChangesAsync();


            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["ProfileUser.Response.EditSuccess"],//+
            };

        }

        public async Task<BaseResponseDto> CheckCard(CardCodeDto cardCodeDto)
        {
            var cur = await _cardCodeRepository.Where(d => d.Password == cardCodeDto.Password && d.UserName == cardCodeDto.UserName).FirstOrDefaultAsync();

            if (cur != null)
            {

                if (cur.IsUsed)
                {
                    var dicError = new Dictionary<string, string> { { "UseOther", _sharedLocalizer["CardFrom.Response.UseOtherPerson"] } };
                    var error = Utilities.CreateErrorMessage("UseOtherPerson", dicError);

                    return new LoginResponseDto
                    {
                        Status = ResponseStatus.Fail,
                        Errors = error
                    };
                }

                return new BaseResponseDto
                {
                    Status = ResponseStatus.Success
                };
            }
            else
            {
                var dicError = new Dictionary<string, string>() {
                    { "NotFound",_sharedLocalizer["CardFrom.Response.NotFound"]}//+
                    };
                var error = Utilities.CreateErrorMessage("NotFound", dicError);

                return new LoginResponseDto
                {
                    Status = ResponseStatus.NotFound,
                    Errors = error
                };
            }

        }

        public async Task<BaseResponseDto> RegisterCard(RegisterCardDto userDto)
        {
            var resultValid = CheckValidate.Valid<RegisterCardDto>(new RegisterCardUserValidation(_sharedLocalizer, _userRepository, _httpContextAccessor.HttpContext), userDto);
            if (resultValid.Status == ResponseStatus.NotValid)
            {
                return resultValid;
            }

            var curUser = await _userRepository.Where(d => d.UserName == userDto.Mobile).FirstOrDefaultAsync();

            //چک تکراری بودن کاربر
            if (curUser == null)
            {
                //چک می کنیم همچین موبایلی با کد وریفای یکی است
                if (!await _mobileActivationRepository.AnyAsync(d => d.Mobile == userDto.Mobile && d.VerifyCode == userDto.VerifyCode))
                {
                    var dicError = new Dictionary<string, string>() {
                    { "NotFound",_sharedLocalizer["GlobalForm.Response.VerifyCodeNotFound"]}
                    };
                    var error = Utilities.CreateErrorMessage("VerifyCodeNotFound", dicError);

                    return new LoginResponseDto
                    {
                        Status = ResponseStatus.NotFound,
                        Errors = error
                    };
                }


                //get current card 
                var card = await _cardCodeRepository.Where(d => d.Password == userDto.PasswordCard && d.UserName == userDto.UserNameCard).FirstOrDefaultAsync();
                if (card == null)
                {
                    var dicError = new Dictionary<string, string>() {
                    { "CardNotFound",_sharedLocalizer["GlobalForm.Response.CardNotFound"]}//
                    };
                    var error = Utilities.CreateErrorMessage("CardNotFound", dicError);

                    return new LoginResponseDto
                    {
                        Status = ResponseStatus.NotFound,

                        Errors = error
                    };
                }

                card.IsUsed = true;
                _cardCodeRepository.Update(card);

                //کاربر هم ثبت میشه هم وریفای
                var hashPassword = Utilities.ComputeHashSHA256(userDto.Password);
                userDto.Password = hashPassword;

                var curPerson = await GetPersonExist(userDto.Mobile);
                var map = UserRegisterMapper.MapCard(userDto, card.Id, curPerson);
                map.IsActive = true;
                map.IsVerify = true;

                //_mobileActivationRepository.Add(new MobileActivation
                //{
                //    CreateDate = DateTime.Now,
                //    ExpireDate = DateTime.Now.AddDays(5),
                //    Mobile = userDto.Mobile,
                //    VerifyCode = code
                //});

                _userRepository.Add(map);

                await _unitOfWork.SaveChangesAsync();

            }
            else
            {
                if (curUser.IsActive != null && !curUser.IsActive.GetValueOrDefault())
                {
                    var dicError = new Dictionary<string, string>() {
                    { "UserBlock",_sharedLocalizer["GlobalForm.Response.UserBlock"]}//+
                };
                    var error = Utilities.CreateErrorMessage("UserBlock", dicError);

                    return new LoginResponseDto
                    {
                        Status = ResponseStatus.Fail,
                        Message = "",
                        Errors = error
                    };
                }

                if (curUser.IsVerify.GetValueOrDefault())
                {
                    var dicError = new Dictionary<string, string>() {
                    { "BeforVerify",_sharedLocalizer["GlobalForm.Response.BeforVerify"]}//+
                };
                    var error = Utilities.CreateErrorMessage("BeforVerify", dicError);

                    return new LoginResponseDto
                    {
                        Status = ResponseStatus.Fail,
                        Message = "",
                        Errors = error
                    };
                }



                return new BaseResponseDto
                {
                    Status = ResponseStatus.Success,
                    //                    Message

                };
            }


            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = _sharedLocalizer["RegisterForm.Response.RegisterSuccess"]//+
            };

        }

        public async Task<Person> GetPersonExist(string mobile)
        {
            var cur = await _personRepository.Where(d => d.Mobile == mobile)
                .Include(d => d.Patient)
                .FirstOrDefaultAsync();


            return cur;
        }

        public async Task<BaseResponseDto> UploadImage(BaseUploadFileDto<long> baseUploadFile)
        {
            var result = await _fileManagerService.Upload(new FileUploadDto { PrimeryKey = baseUploadFile.Id.ToString(), File = baseUploadFile.File, TableName = nameof(Person), FileGroupId = 1 });

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
            };
        }

        public async Task<BaseResponseDto> UserCardActive(ActiveCardCodeDto activeCardCodeDto)
        {
            //var cardCode = await _cardCodeRepository.Where(d => d.Token == activeCardCodeDto.Token)
            //    .Include(d => d.Users).ThenInclude(d => d.Person).ThenInclude(d => d.Patient)
            //    .FirstOrDefaultAsync();

            var cardCode = await _cardCodeRepository.Where(d => d.Token == activeCardCodeDto.Token).FirstOrDefaultAsync();

            if (cardCode is null)
            {
                var dicError = new Dictionary<string, string> { { "NotFound", _sharedLocalizer["CardFrom.Response.NotFound"] } };
                var error = Utilities.CreateErrorMessage("NotFound", dicError);
                return new BaseResponseDto { Status = ResponseStatus.NotFound, Errors = error };
            }

            if (cardCode.IsUsed)
            {
                var dicError = new Dictionary<string, string> { { "UseOther", _sharedLocalizer["CardFrom.Response.UseOtherPerson"] } };
                var error = Utilities.CreateErrorMessage("UseOtherPerson", dicError);
                return new LoginResponseDto { Status = ResponseStatus.Fail, Errors = error };
            }

            // تغییر وضعیت کارت به استفاده شده
            cardCode.IsUsed = true;
            _cardCodeRepository.Update(cardCode);

            var userId = _workContext.UserId.GetValueOrDefault();


            var userCard = await _userCardCode.Where(x => x.CardCodeId == cardCode.Id).FirstOrDefaultAsync();
            var cardExpire = DateTime.Now.AddYears(1);

            if (userCard is null)
            {
                var map = UserRegisterMapper.MapUserCardCode(userId, cardExpire,cardCode);
                _userCardCode.Add(map);
            }

            else
            {
                userCard.ExpireDate = cardExpire;
                _userCardCode.Update(userCard);

                var userCardUserId = userCard.UserId.GetValueOrDefault();

                // یوزر جاری و یوزر کارت
                var users = await _userRepository.Include(x => x.Person).ThenInclude(x => x.Patient).ThenInclude(x=>x.PatientExtraInfo)
                    .Where(x => x.Id == userId || x.Id == userCardUserId).ToListAsync();

                // ویرایش یوزر در جدول لاگین
                var cardUser = users.Find(x => x.Id == userCardUserId);

                //var login = await _loginRepository.Where(x => x.Token == Guid.Parse(_workContext.Token)).FirstOrDefaultAsync();
                //login.UserId = cardUser.Id;
                //_loginRepository.Update(login);

                // پاک کردن اطلاعات یوزر جاری
                var oldUser = users.Find(x => x.Id == userId);
                _userRepository.Remove(oldUser);

                if (oldUser.Person.Patient.TryAny())
                {
                    _patientExtraInfoRepository.RemoveRange(oldUser.Person.Patient.First().PatientExtraInfo);
                    _patientRepository.Remove(oldUser.Person.Patient.First());
                }

                var userRolePermissions = await _userRolePermissionsRepository.Where(x => x.UserId == oldUser.Id).ToListAsync();
                userRolePermissions.ForEach(x=>x.UserId= cardUser.Id);
                _userRolePermissionsRepository.UpdateRange(userRolePermissions);

                var fileTags = await _fileTagRepository.Where(x => x.UserId == oldUser.Id).ToListAsync();
                fileTags.ForEach(x => x.UserId = cardUser.Id);
                _fileTagRepository.UpdateRange(fileTags);
                
                var recServices = await _receptionServices.Where(x => x.AnswerUserId == oldUser.Id).ToListAsync();
                recServices.ForEach(x => x.AnswerUserId= cardUser.Id);
                _receptionServices.UpdateRange(recServices);

                _personRepository.Remove(oldUser.Person);

                // آپدیت فایل منیجر
                // هنوز انجام نشده

                // تغییر اطلاعات لاگین یوزر کارت
                cardUser.Password = oldUser.Password;
                cardUser.UserName = oldUser.UserName;
                cardUser.IsSync = true;
                cardUser.IsVerify = true;
                _userRepository.Update(cardUser);
            }

            // حذف یوزر جاری و ویرایش اطلاعات لاگین یوزر سینک شده یعنی یوزر ثبت  شده برای کارت

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = cardExpire.ToDateString()
            };
        }
    }
}
