using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Doctor
{
    public class ListDoctorHomeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string WorkingHours { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Expertise { get; set; }
        public string Logo { get; set; }
        public List<ListMultiResponse<int>> WorkItemType { get; set; }
        public int? ExamplaceId { get; set; }
    }
}
