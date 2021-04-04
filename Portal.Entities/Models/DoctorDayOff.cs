using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DoctorDayOff
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public long? DoctorAppointmentId { get; set; }

        public virtual DoctorAppointment DoctorAppointment { get; set; }
    }
}
