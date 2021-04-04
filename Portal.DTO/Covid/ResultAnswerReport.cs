using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Covid
{
    public class ResultAnswerReportDto
    {
        public string FullName { get; set; }
        public string ReceptionCode { get; set; }
        public string Barcode { get; set; }
        public string ReceptionDate { get; set; }
        public string AnswerDate { get; set; }
        public string Sex { get; set; }
        public int? Age { get; set; }
        public int? CenterId { get; set; }
        public string Email  { get; set; }
        public string Mobile  { get; set; }
        public string FileId  { get; set; }
        public ResultAnswerItemReportDto Answers { get; set; }
    }

    public class ResultAnswerItemReportDto
    {
        public string FullName { get; set; }
        public string Result { get; set; }
        public string Unit { get; set; }
        public string NormalRange { get; set; }
        public string Flag { get; set; }
        public string Comment { get; set; }
    }
}
