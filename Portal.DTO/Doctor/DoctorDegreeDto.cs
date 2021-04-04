using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class DoctorDegreeDto
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public string DoctorFulName { get; set; }
        public int DegreeId { get; set; }
        public string DegreeTitle { get; set; }
        public DateTime? DateOfIssue { get; set; }
    }
}
