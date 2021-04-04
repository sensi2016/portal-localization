using Portal.DTO;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Mapper
{
    public class MobileActivationMapper
    {
        public static MobileActivation Map(RequestVerifyDto requestVerifyDto)
        {
            return new MobileActivation
            {
                Mobile = requestVerifyDto.Mobile,
                VerifyCode = requestVerifyDto.VerifyCode,
                CreateDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddDays(5)
            };
        }
    }
}
