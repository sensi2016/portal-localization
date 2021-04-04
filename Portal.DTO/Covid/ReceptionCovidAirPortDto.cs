using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO.Covid
{
    public class ReceptionCovidAirPortDto
    {

        public long Id { get; set; }
        public string Code { get; set; }
        public int? RelationId { get; set; }
        // public string RelationTitle { get; set; }
        public string Mobile { get; set; }
        public short? Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string NationalCode { get; set; }
        //    public int? CountryId { get; set; }
        public int? SexId { get; set; }
        //public int? ProvinceId { get; set; }
        //public int? ZoneId { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AnswerDate { get; set; }
        public string Note { get; set; }

    }

    public class FilterCovidAirPortDto : IPaging
    {
        public string Mobile { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }

    public class ListCovidAirPortDto
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string ReceptionCode { get; set; }
        public string ReceptionDate { get; set; }
        public string AnswerDate { get; set; }
        public string RelationShip { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string GrandFatherName { get; set; }
        public string Sex { get; set; }
        public int? Age { get; set; }
        public string Mobile { get; set; }
        public string FileId { get; set; }
        public bool? Result  { get; set; }

    }
}
