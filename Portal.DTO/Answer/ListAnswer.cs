using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class ListAnswerDto
    {
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Type { get; set; }
        public int TotalFile { get; set; }
        public string Link { get; set; }
    }

    public class DetailAnswerDto
    {
        public string Link { get; set; }
        public string Date { get; set; }
      
    }

    public class ListAnswerPatientDto
    {
        public long Id { get; set; }
        public string Note { get; set; }
        public string Date { get; set; }
        public string FileId { get; set; }
        public string Center { get; set; }
        public string FileExtension  { get; set; }
    }
}
