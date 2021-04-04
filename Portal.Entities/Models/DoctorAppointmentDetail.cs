using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DoctorAppointmentDetail
    {
        public long Id { get; set; }
        public long? DoctorAppointmentId { get; set; }
        public int? DoctorAppointmentDetailStatusId { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public TimeSpan? AppointmentTime { get; set; }
        public long? ReceptionId { get; set; }

        public virtual DoctorAppointment DoctorAppointment { get; set; }
        public virtual DoctorAppointmentDetailStatus DoctorAppointmentDetailStatus { get; set; }
        public virtual Receptions Reception { get; set; }
    }
}
