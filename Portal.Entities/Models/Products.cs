using System;
using System.Collections.Generic;

namespace Portal.Entities.Models
{
    public partial class Products
    {
        public long Id { get; set; }
        public long? OnlineId { get; set; }
        public string Enname { get; set; }
        public string Fullname { get; set; }
    }
}
