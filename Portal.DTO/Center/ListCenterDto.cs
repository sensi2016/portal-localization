using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Center
{
    public class ListCenterDto
    {
        public int Id { get; set; }
       // public object Examplaces { get; set; }
        public string Title { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }

    public class ListHomeCenterDto
    {
        public int Id { get; set; }
        public object Examplaces { get; set; }
        public object SellTypes { get; set; }
        public object WorkItemType { get; set; }
        public object CenterService { get; set; }
        public string OwnershipType { get; set; }
        public string Title { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Logo { get; set; }
        public bool? IsHomeDelivery  { get; set; }
    }
}
