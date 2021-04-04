using System;

namespace Portal.DTO
{
    public class FilesDto
    {
        public int Id { get; set; }
        public int FileGroupId { get; set; }
        public string FileGroupTitle { get; set; }
        public string RefferKey { get; set; }
        public string FileName { get; set; }
        public string TableName { get; set; }
        public string PrimeryKey { get; set; }
        public string CreateDate { get; set; }
    }

    public class SearchFileDto:IPaging
    {
        public string GroupCode { get; set; }
        public string Name { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class ChangeFileGroupDto
    {
        public int FileId { get; set; }
        public int GroupId { get; set; }
    }
}
