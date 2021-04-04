
using Portal.DTO;
using Portal.DTO.User;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Interface
{
    public interface IUserManagerService
    {
        Task<BaseResponseDto> Login(LoginDto loginDto);
       // Task<string> RefreshToken(string RefreshId);
       // Task<string> ValidToken(string token);
       // Task<Login> GetLoginByToken(Guid token);
        Task<Login> CheckToken(Guid token);
        Task<BaseResponseDto> Logout(Guid token);
        Task<BaseResponseDto> UserCardActive(ActiveCardCodeDto activeCardCodeDto);
        Task<BaseResponseDto> RegisterAsync(UserRegisterDto userDto);
        Task<BaseResponseDto> RegisterForCenter(UserCenterRegisterDto userDto);
        Task<BaseResponseDto> Verify(RequestVerifyDto requestVerifyDto);
        Task<BaseResponseDto> GenerateVerify(RequestMobileVerifyDto requestVerifyDto);
        Task<BaseResponseDto> ForgetPassword(RequestForgetPasswordyDto requestVerifyDto);
        Task<BaseResponseDto> GetCaptcha();
        Task<BaseResponseDto> ChangePassword(ChangePasswordDto changePasswordDto);
        Task<BaseResponseDto> GetUsersByMobile(string mobile);
        Task<BaseResponseDto> CheckExistUser(RequestMobileVerifyDto   requestMobileVerifyDto);
        Task<BaseResponseDto> CheckVerifyUser(RequestVerifyDto   requestVerifyDto);
        Task<BaseResponseDto> CurrentUser();
        Task<BaseResponseDto> GetAllbyCurrentSection();
        Task<BaseResponseDto> UpdateCurrentUser(UserPersonDto userPersonDto);

        #region card
        Task<BaseResponseDto> CheckCard(CardCodeDto cardCodeDto);
        Task<BaseResponseDto> RegisterCard(RegisterCardDto userDto);

        #endregion
        //Task<BaseResponseDto> GetAll();
        //Task<ListResponseDto> Search(UserFilterDto userFilterDto);

        //Task<BaseResponseDto> EditAsync(UserRegisterDto userDto);
        //Task<BaseResponseDto> GetById(int userId);
        //Task<ListResponseDto> ListUser(BaseRequestPost<int> baseRequestPost);
        Task<BaseResponseDto> SetLanguage(Guid token,string language);
        Task<BaseResponseDto> SetSection(Guid token, SetSectionDto setSectionDto );
        //Task<List<PermissionDto>> GetPermissionsAsync(string token);
        //Task<BaseResponseDto> UserPerson(List<int> userId);


        //Task<ListResponseDto> ListPermission(BaseRequestPost<int> baseRequestPost);
        //Task<ListResponseDto> ListPermissionForUser(RequestUserPermissionDto requestUserPermissionDto);
        //Task<BaseResponseDto> AddUserPermission(AddOrDeleteUserPermissionDto addOrDeleteUserPermissionDto);
        //Task<BaseResponseDto> DeleteUserPermission(AddOrDeleteUserPermissionDto addOrDeleteUserPermissionDto);

        //Task<BaseResponseDto> AddUserRole(AddOrDeleteUserRoleDto addOrDeleteUserRoleDto);
        //Task<BaseResponseDto> DeleteUserRole(AddOrDeleteUserRoleDto addOrDeleteUserRoleDto);
        //Task<ListResponseDto> ListRoleForUser(RequestUserRoleDto requestUserRoleDto);
        Task<List<PermissionDto>> GetPermissionsAsync(int? userid);
        Task<BaseResponseDto> UploadImage(BaseUploadFileDto<long> baseUploadFile);

    }
}
