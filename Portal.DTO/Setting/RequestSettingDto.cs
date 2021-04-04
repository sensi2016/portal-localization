using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class RequestSettingDto
    {
        public List<SettingItemDto> Data { get; set; }
    }

    public class SettingItemDto {
        public string Value { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}

