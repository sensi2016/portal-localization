using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionServiceChart
    {
        public long Id { get; set; }
        public DateTime? PrescriptionChartDate { get; set; }
        public int? UserId { get; set; }
        public int? FunctorId { get; set; }
        public int? WitnessId { get; set; }
        public long? PrescriptionDetailServiceId { get; set; }
        public int? UnitId { get; set; }
        public int? PrescriptionChartCancelationReasonId { get; set; }
        public int? PrescriptionChartActionTypeId { get; set; }
        public DateTime? DoneDate { get; set; }
        public string Result { get; set; }
        public DateTime? ResultDate { get; set; }
        public string Note { get; set; }
        public int? RoleId { get; set; }

        public virtual Users Functor { get; set; }
        public virtual PrescriptionChartActionType PrescriptionChartActionType { get; set; }
        public virtual PrescriptionChartCancelationReason PrescriptionChartCancelationReason { get; set; }
        public virtual PrescriptionDetailService PrescriptionDetailService { get; set; }
        public virtual Role Role { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Users User { get; set; }
        public virtual Users Witness { get; set; }
    }
}
