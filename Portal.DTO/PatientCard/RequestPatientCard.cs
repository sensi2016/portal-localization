using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class RequestPatientCardDto:IPaging 
    {
       // public int PatientId { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string PrescriptionFromDate { get; set; }
        public string PrescriptionToDate { get; set; }
    }

}
