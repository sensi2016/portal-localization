using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class RequestHistory
    {
        public long Id { get; set; }
        public long? RequestId { get; set; }
        public int? RequestStatusId { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UserId { get; set; }
        public bool? IsCurrent { get; set; }
        public int? DeliveryId { get; set; }
        public int? ReceiveId { get; set; }
        public int? RoleId { get; set; }

        public virtual Employee Delivery { get; set; }
        public virtual Employee Receive { get; set; }
        public virtual Request Request { get; set; }
        public virtual RequestStatus RequestStatus { get; set; }
        public virtual Role Role { get; set; }
        public virtual Users User { get; set; }
    }
}
