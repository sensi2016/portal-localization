using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class RequestDetail
    {
        public long Id { get; set; }
        public long? RequestId { get; set; }
        public int? GenericDrugId { get; set; }
        public int? DrugFormId { get; set; }
        public int? DrugId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ActionDate { get; set; }
        public int? StatusId { get; set; }
        public int? QuantityConfirm { get; set; }
        public int? PrescriptionInstructionId { get; set; }
        public int? WayOfPrescriptionId { get; set; }

        public virtual Drugs Drug { get; set; }
        public virtual DrugForm DrugForm { get; set; }
        public virtual GenericDrug GenericDrug { get; set; }
        public virtual PrescriptionInstruction PrescriptionInstruction { get; set; }
        public virtual RequestStatus Status { get; set; }
        public virtual WayOfPrescription WayOfPrescription { get; set; }
    }
}
