using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.User
{
    public class ChangePasswordDto
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatPassword { get; set; }
    }
}
