using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class CoreBaseInfoFilterDto
    {
        public List<int> UserIds { get; set; }
        public List<int> EmployeeIds { get; set; }
        public List<int> SectionIds { get; set; }
    }

    public class DrugBaseInfoFilterDto
    {
        public List<int> DrugIds { get; set; }
        public List<int> GenericDrugIds { get; set; }
        public List<int> DrugFormIds { get; set; }
        public List<int> WayOfPrescriptionIds { get; set; }
    }
}
