using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class PurchaseInvoiceDetails
    {
        public PurchaseInvoiceDetails()
        {
            StoreEntryDetails = new HashSet<StoreEntryDetails>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public int? PurchaseInvoiceId { get; set; }
        public int? DrugId { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public int? PackagingTypeId { get; set; }
        public int? PackagingQuantity { get; set; }
        public int? Quantity { get; set; }
        public int? Award { get; set; }
        public decimal? AwardProfit { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? SalesPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Tax { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? Returned { get; set; }

        public virtual Drugs Drug { get; set; }
        public virtual PackagingType PackagingType { get; set; }
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }
        public virtual ICollection<StoreEntryDetails> StoreEntryDetails { get; set; }
    }
}
