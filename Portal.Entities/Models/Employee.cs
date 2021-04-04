using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Employee
    {
        public Employee()
        {
            PurchaseInvoicePurchaser = new HashSet<PurchaseInvoice>();
            PurchaseInvoiceTransferee = new HashSet<PurchaseInvoice>();
            RequestHistoryDelivery = new HashSet<RequestHistory>();
            RequestHistoryReceive = new HashSet<RequestHistory>();
            SectionBoss = new HashSet<Section>();
            SectionSuperVisorPersonel = new HashSet<Section>();
            StoreTransferDeliveryUser = new HashSet<StoreTransfer>();
            StoreTransferReceiveUser = new HashSet<StoreTransfer>();
        }

        public int Id { get; set; }
        public int? PersonId { get; set; }
        public int? HospitalRoleTypeId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? EmploymentStartDate { get; set; }
        public string Note { get; set; }
        public bool? IsAdmin { get; set; }
        public string Code1 { get; set; }
        public string Code2 { get; set; }
        public string NoteLang2 { get; set; }

        public virtual Person Person { get; set; }
        public virtual ICollection<PurchaseInvoice> PurchaseInvoicePurchaser { get; set; }
        public virtual ICollection<PurchaseInvoice> PurchaseInvoiceTransferee { get; set; }
        public virtual ICollection<RequestHistory> RequestHistoryDelivery { get; set; }
        public virtual ICollection<RequestHistory> RequestHistoryReceive { get; set; }
        public virtual ICollection<Section> SectionBoss { get; set; }
        public virtual ICollection<Section> SectionSuperVisorPersonel { get; set; }
        public virtual ICollection<StoreTransfer> StoreTransferDeliveryUser { get; set; }
        public virtual ICollection<StoreTransfer> StoreTransferReceiveUser { get; set; }
    }
}
