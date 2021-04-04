using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class StoreEntryDetails
    {
        public long Id { get; set; }
        public int? StoreEntryId { get; set; }
        public long? PurchaseInvoiceDetailsId { get; set; }
        public int? DrugId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? PackagingTypeId { get; set; }
        public int? PackagingQuantity { get; set; }
        public int? Quantity { get; set; }
        public int? Returned { get; set; }

        public virtual Drugs Drug { get; set; }
        public virtual PackagingType PackagingType { get; set; }
        public virtual PurchaseInvoiceDetails PurchaseInvoiceDetails { get; set; }
        public virtual StoreEntry StoreEntry { get; set; }
    }
}
