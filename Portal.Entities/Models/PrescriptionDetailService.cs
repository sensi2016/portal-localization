using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PrescriptionDetailService
    {
        public PrescriptionDetailService()
        {
            PrescriptionDetailServiceHistory = new HashSet<PrescriptionDetailServiceHistory>();
            PrescriptionServiceChart = new HashSet<PrescriptionServiceChart>();
            ReceptionService = new HashSet<ReceptionService>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public long? PrescriptionId { get; set; }
        public int? PrescriberNurseId { get; set; }
        public int? ServiceId { get; set; }
        public int? PrescriptionTypeId { get; set; }
        public int? Quantity { get; set; }
        public int? RequestFromSectionId { get; set; }
        public int? PrescriptionInstructionId { get; set; }
        public DateTime? ActionDate { get; set; }
        public int? PriorityId { get; set; }
        public int? Period { get; set; }
        public long? PanelId { get; set; }
        public DateTime? StartDate { get; set; }
        public bool? IsJustOnTime { get; set; }
        public string Note { get; set; }
        public int? FrequencyId { get; set; }

        public virtual Frequency Frequency { get; set; }
        public virtual PrescriptionPanel Panel { get; set; }
        public virtual Users PrescriberNurse { get; set; }
        public virtual Prescription Prescription { get; set; }
        public virtual PrescriptionType PrescriptionType { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Section RequestFromSection { get; set; }
        public virtual Services Service { get; set; }
        public virtual ICollection<PrescriptionDetailServiceHistory> PrescriptionDetailServiceHistory { get; set; }
        public virtual ICollection<PrescriptionServiceChart> PrescriptionServiceChart { get; set; }
        public virtual ICollection<ReceptionService> ReceptionService { get; set; }
    }
}
