using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.User
{
    public class RegisterCardDto
    {
        /// <summary>
        /// اجباری  است
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// اجباری  است
        /// </summary>
        public string VerifyCode { get; set; }
        /// <summary>
        /// اجباری  است
        /// </summary>
        public string UserNameCard { get; set; }
        /// <summary>
        /// اجباری  است
        /// </summary>
        public string PasswordCard { get; set; }
        /// <summary>
        /// اجباری  است
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// اجباری  است
        /// </summary>
        public string RepeatPassword { get; set; }
        public string HealthNumber { get; set; }
    }
}
