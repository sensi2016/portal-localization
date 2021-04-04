using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DTO
{
    public class UserPersonDto
    {
        public int? UserId { get; set; }
        public int? PersonId { get; set; }
        /// <summary>
        /// این فیلد اجباری است
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// این فیلد اجباری است       
        /// </summary>
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string NhsNumber { get; set; }
        public string GrandFatherName { get; set; }
        public string GreatGrandFatherName { get; set; }
        public string LatinName { get; set; }
        // public string Country { get; set; }
        public string NationalCode { get; set; }
        public string Email { get; set; }
        public short? Age { get; set; }
        public int? SexId { get; set; }
        public int? BirthPlaceId { get; set; }
        public string Sex { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string MaritalStatus { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? BloodGroupId { get; set; }
        public int? RhId { get; set; }
        public string Rh { get; set; }
        public string BloodGroup { get; set; }
        public string FilteId { get; set; }
        public string FileId { get; set; }
        public string Note { get; set; }
        public bool? IsSmooking { get; set; }
        public bool? IsDrinking { get; set; }

        //public int? InternalId { get; set; }
        //public string FileNo { get; set; }
    }
}
