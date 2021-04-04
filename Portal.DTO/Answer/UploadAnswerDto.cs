using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Answer
{
    public class UploadAnswerDto
    {
        public int? UserId { get; set; }
        public IFormFile File { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public int? PatientId { get; set; }
        public string Note { get; set; }
    }


    public class FastUploadAnswerDto
    {
        public IFormFile File { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string NHSNumber { get; set; }
        public int? SectionId { get; set; }

        public string Note { get; set; }
    }

    public class ExcelAnswerDto
    {
        public string AnswerDate { get; set; }
        public List<ExcelAnswerDetailsDto> Details { get; set; }

    }

    public class ExcelAnswerDetailsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Result { get; set; }
        public string Mobile { get; set; }
        public string RefferFrom { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Notice { get; set; }
        public int? Age { get; set; }
    }
    public class RequestAnswerExcelDto
    {
        public string Answers { get; set; }
    }

    public class FilterAnswerExcelDto : IPaging
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string FromAnswerDate { get; set; }
        public string ToAnswerDate { get; set; }
        public bool? IsPositive { get; set; }
        public bool? IsNegative { get; set; }
        public List<int> RefferFromId { get; set; }
        public List<int> PatientStatusId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
