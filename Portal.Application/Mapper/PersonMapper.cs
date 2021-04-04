using Portal.DTO;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Mapper
{
    public class PersonMapper
    {
        public static PersonDto  Map(Person person)
        {
            return new PersonDto
            {

                Id = person.Id,
                FirstName=person.FirstName,
                LastName=person.LastName,
                FatherName=person.FatherName,
                Mobile=person.Mobile,
                NationalCode=person.NationalCode,
                GrandFatherName = person.GrandFatherName,
                SexId=person.SexId,
                Address=person.Address,
                Email=person.Email,
                Phone=person.Phone,
                
            };
        }
    }
}
