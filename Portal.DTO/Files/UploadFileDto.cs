using System;
using System.Collections.Generic;

namespace Portal.DTO
{
    public class UploadFileResponseDto
    {
        public DataDTO Data { get; set; }
        //{"Data":{"FID":"08043276-38e9-4a25-9e0a-32f6e67bdce9","UploadStatus":0,"Message":null},"State":1,"Message":null,"Exception":null}
    }
    public class DataDTO
    {
        public Guid FID { get; set; }
        public int UploadStatus { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public string CreateDate { get; set; }
    }

    public class UploadFilesResponseDto
    {
        public List<DataDTO> Data { get; set; }
        //{"Data":{"FID":"08043276-38e9-4a25-9e0a-32f6e67bdce9","UploadStatus":0,"Message":null},"State":1,"Message":null,"Exception":null}
    }
}
