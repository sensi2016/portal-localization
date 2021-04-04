using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    
    public class SyncDto
    {
        public Guid UserId { get; set; }
        public long LocalUserId { get; set; }
        public SyncDataDto Data { get; set; }
    }
    
    public class SyncDoctorDto
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string UserName{ get; set; }
        public string Mac{ get; set; }
    }

    public class SyncTestDto
    {
        public long localUserId { get; set; }
    }

}
