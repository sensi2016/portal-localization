using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Bed
    {
        public Bed()
        {
            BedReception = new HashSet<BedReception>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string LocalCode { get; set; }
        public int? BedTypeId { get; set; }
        public string BedNo { get; set; }
        public int? RoomId { get; set; }
        public string Note { get; set; }
        public string Phone { get; set; }
        public bool? IsActive { get; set; }

        public virtual BedType BedType { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<BedReception> BedReception { get; set; }
    }
}
