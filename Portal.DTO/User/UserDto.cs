﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public bool? IsActive { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string CreateDate { get; set; }
        public bool? IsLimitByIp { get; set; }
    }
}
