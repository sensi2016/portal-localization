using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class StoreEntry
    {
        public StoreEntry()
        {
            StoreEntryDetails = new HashSet<StoreEntryDetails>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UserId { get; set; }
        public int? PurchaseInvoiceId { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? StoreId { get; set; }
        public int? RoleId { get; set; }

        public virtual PurchaseInvoice PurchaseInvoice { get; set; }
        public virtual Role Role { get; set; }
        public virtual Section Store { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<StoreEntryDetails> StoreEntryDetails { get; set; }
    }
}
