using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class BaseUploadFileDto<T>
    {
        /// <summary>
        /// ایدی جدول مربوط
        /// </summary>
        public T Id { get; set; }
        public IFormFile File { get; set; }
    }
}
