using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Portal.DTO
{
    
    public class FileUploadDto
    {
        public IFormFile File { get; set; }
        public string CategoryID { get; set; }
        public string Description { get; set; }
        public string MetaData { get; set; }
        public string Culture { get; set; }
        public string TableName { get; set; }
        public string PrimeryKey { get; set; }
        public int FileGroupId { get; set; }
    }
    public class FileAndTagUploadDto
    {
        public int Id { get; set; }
        public string FileGroupTitle { get; set; }
        public string RefferKey { get; set; }
        public List<string> Tags { get; set; }
        public string CategoryID { get; set; }
        public string Description { get; set; }
        public string MetaData { get; set; }
        public string Culture { get; set; }
        public string CreateDate { get; set; }
        public string TableName { get; set; }
        public string PrimeryKey { get; set; }
        public string FileName { get; set; }
        public int FileGroupId { get; set; }
    }

    public class MultiUploadFilesDto
    {
        public List<IFormFile> Files { get; set; }
        public string FileGroupCode { get; set; }
        public string Tags { get; set; }
        public string CategoryID { get; set; }
        public string Description { get; set; }
        public string MetaData { get; set; }
        public string Culture { get; set; }
        public string TableName { get; set; }
        public string PrimeryKey { get; set; }
        //public List<FileTagDto> Tags { get; set; }
    }

    public class RequestUploadFilesDto
    {
        public List<IFormFile> Files { get; set; }
        public string FileGroupCode { get; set; }
        public string Tags { get; set; }
   
        //public List<FileTagDto> Tags { get; set; }
    }

    public class FileTagDto
    {
        public string FileName { get; set; }
        public List<string> Tags { get; set; }
    }
}
