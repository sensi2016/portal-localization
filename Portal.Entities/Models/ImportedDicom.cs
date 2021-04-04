using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class ImportedDicom
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public Guid? FileId { get; set; }
        public int? FileNumber { get; set; }
        public int? FileSize { get; set; }
    }
}
