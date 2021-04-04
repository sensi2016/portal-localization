using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class ResourceDto
    {
        public string Language { get; set; }
        public List<string> Actions { get; set; }
        public bool IsKeyValue { get; set; }
    }

    public class NewResourceDto
    {
        public string Language { get; set; }
        public string ResourceName { get; set; }
        public Dictionary<string, string> Translates { get; set; }
    }
}
