using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class UploadLogoDto
    {
        public int CenterId { get; set; }
        public IFormFile File { get; set; }
    }

    public class UploadLogoDoctorDto
    {
        public int DoctorId { get; set; }
        public IFormFile File { get; set; }
    }
}
