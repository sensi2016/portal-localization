using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class FileTag
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public int? UserId { get; set; }
        public int? FileId { get; set; }
        public string TagName { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual Files File { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Users User { get; set; }
    }
}
