using Portal.DTO;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Mapper
{
    public static class SettingMapper
    {
        public static SettingItemDto Map(Setting setting)
        {
          
            return new SettingItemDto
            {
                Name = setting.KeyName,
                Value = setting.KeyValue,
                Type = Utilities.GetTypeOf(setting.KeyValue)

            };
        }
    }
}
