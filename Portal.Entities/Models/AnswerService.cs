using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class AnswerService
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public int? PatientId { get; set; }
        public string FileId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? InsertUserId { get; set; }
        public string Mobile { get; set; }
        public int? SectionId { get; set; }
        public bool? IsOutSystem { get; set; }
        public string Note { get; set; }
        public string Nhsnumber { get; set; }
        public string Title { get; set; }
        public string Result { get; set; }
        public int? Age { get; set; }
        public string PatientStatus { get; set; }
        public string RefferFrom { get; set; }
        public int? SendsmsStatusId { get; set; }
        public bool? IsExcel { get; set; }
        public string FileName { get; set; }

        public virtual Users InsertUser { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Section Section { get; set; }
        public virtual SendSmsStatus SendsmsStatus { get; set; }
        public virtual Users User { get; set; }
    }
}
