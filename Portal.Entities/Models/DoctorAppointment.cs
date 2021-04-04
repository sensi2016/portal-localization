using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class DoctorAppointment
    {
        public DoctorAppointment()
        {
            DoctorAppointmentDetail = new HashSet<DoctorAppointmentDetail>();
            DoctorDayOff = new HashSet<DoctorDayOff>();
        }

        public long Id { get; set; }
        public int? DoctorId { get; set; }
        public int? DurationTypeId { get; set; }
        public int? AppointmentTypeId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int DayQuantity { get; set; }

        public virtual AppointmentType AppointmentType { get; set; }
        public virtual Doctors Doctor { get; set; }
        public virtual DurationType DurationType { get; set; }
        public virtual ICollection<DoctorAppointmentDetail> DoctorAppointmentDetail { get; set; }
        public virtual ICollection<DoctorDayOff> DoctorDayOff { get; set; }
    }
}
