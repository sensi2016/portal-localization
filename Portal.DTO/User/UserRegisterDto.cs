using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class UserRegisterDto
    {
        public int Id { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string LastName { get; set; }
        public string Email { get; set; }

        public int SexId { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string SecurityCode { get; set; }
        public string VerifyCode { get; set; }

    }

    public class UserFilterDto : BaseRequestPaging
    {
        public List<int> IdList { get; set; }
    }

    public class ActiveCardCodeDto
    {
        public string Token { get; set; }
    }

    public class ListUserDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public bool? IsActive { get; set; }
        public string UserName { get; set; }
        public string CreateDate { get; set; }
        public bool? IsLimitByIp { get; set; }
    }

    public class RequestVerifyDto{
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string VerifyCode { get; set; }
    }

    public class RequestMobileVerifyDto
    {
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Mobile { get; set; }
    }



    public class RequestForgetPasswordyDto
    {
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string VerifyCode { get; set; }
    }

    public class RequestTokenDto
    {

        public string Token { get; set; }
    }

    public class UserCenterRegisterDto
    {
        public int Id { get; set; }
        public int? CenterId { get; set; }
        public int? PersonId { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// اجباری است
        /// </summary>
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
      

    }
}
