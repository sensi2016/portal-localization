using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PackagingType
    {
        public PackagingType()
        {
            PurchaseInvoiceDetails = new HashSet<PurchaseInvoiceDetails>();
            StoreEntryDetails = new HashSet<StoreEntryDetails>();
            StoreTransferDetails = new HashSet<StoreTransferDetails>();
        }

        public int Id { get; set; }
        public int? DrugFormId { get; set; }
        public string Title { get; set; }
        public int? ContentCount { get; set; }

        public virtual DrugForm DrugForm { get; set; }
        public virtual ICollection<PurchaseInvoiceDetails> PurchaseInvoiceDetails { get; set; }
        public virtual ICollection<StoreEntryDetails> StoreEntryDetails { get; set; }
        public virtual ICollection<StoreTransferDetails> StoreTransferDetails { get; set; }
    }
}
