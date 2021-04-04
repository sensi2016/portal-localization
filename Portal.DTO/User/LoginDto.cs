using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.User
{
    public class LoginDto
    {
        /// <summary>
        /// اجباری است
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// اجباری است
        /// </summary>
        public string Password { get; set; }
        public string Language { get; set; }
    }
}
