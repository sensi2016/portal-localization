using System;

namespace Portal.DTO
{
    public partial class FileManagerDto
    {
        public FileManagerDataDto[] Data { get; set; }
        public long State { get; set; }
        public object Message { get; set; }
        public object Exception { get; set; }
    }
     
    public partial class FileManagerDataDto
    {
        public Guid Fid { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Format { get; set; }
        public long Size { get; set; }
        public string Metadata { get; set; }
        public bool IsActive { get; set; }
        public bool IsConfidential { get; set; }
    }
}
