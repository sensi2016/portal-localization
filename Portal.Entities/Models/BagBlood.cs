using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class BagBlood
    {
        public BagBlood()
        {
            BloodStatusHistory = new HashSet<BloodStatusHistory>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public int? BloodGroupId { get; set; }
        public int? BloodProductTypeId { get; set; }
        public DateTime? DonationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? EnterDate { get; set; }
        public string DonationCode { get; set; }
        public string Doner { get; set; }
        public int? ProceedFromBagNumberId { get; set; }
        public int? PrintCount { get; set; }
        public decimal? Volume { get; set; }
        public decimal? Price { get; set; }
        public string Note { get; set; }

        public virtual BloodProductType BloodProductType { get; set; }
        public virtual ProceedFromBagNumber ProceedFromBagNumber { get; set; }
        public virtual ICollection<BloodStatusHistory> BloodStatusHistory { get; set; }
    }
}
