using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Room
    {
        public Room()
        {
            Bed = new HashSet<Bed>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleLang2 { get; set; }
        public string LocalCode { get; set; }
        public string RoomNo { get; set; }
        public int? Floor { get; set; }
        public int? RoomTypeId { get; set; }
        public int? SectionId { get; set; }
        public string Note { get; set; }
        public string Phone { get; set; }
        public int? ServiceId { get; set; }
        public int? ServiceIdEscort { get; set; }

        public virtual RoomType RoomType { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<Bed> Bed { get; set; }
    }
}
