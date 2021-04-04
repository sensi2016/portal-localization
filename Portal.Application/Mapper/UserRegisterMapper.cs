using Portal.DTO;
using Portal.DTO.Enum;
using Portal.Entities.Models;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using Portal.DAL.Extensions;
using Portal.DTO.User;
using System.Linq;

namespace Portal.Application.Mapper
{
    public class UserRegisterMapper
    {
        public static Users MapCard(RegisterCardDto userDto, long cardId, Person curPerson)
        {
            Users users = new Users();
            Person person = new Person();
            Patient patient = new Patient();

            patient.CreateDate = DateTime.Now;
            users.CreateDate = DateTime.Now;
            users.UserName = userDto.Mobile;
            users.Password = userDto.Password;
            person.Mobile = userDto.Mobile;
            users.CardCodeId = cardId;

            if (curPerson == null)
            {
                person.Patient.Add(patient);
                users.Person = person;
            }
            else
            {
                users.PersonId = curPerson.Id;
            }

            users.UserRolePermission.Add(new UserRolePermission { RoleId = (int)RoleEnum.Patient, SectionId = 1, PermissionId = null });


            return users;
        }
        public static Users Map(UserRegisterDto userDto, Person curPerson)
        {
            Users users = new Users();
            Person person = new Person();
            Patient patient = new Patient();

            patient.CreateDate = DateTime.Now;
            users.CreateDate = DateTime.Now;
            users.UserName = userDto.Mobile;
            users.Password = userDto.Password;
            person.Mobile = userDto.Mobile;
            person.FirstName = userDto.FirstName;
            person.LastName = userDto.LastName;
            person.SexId = userDto.SexId;
            person.Email = userDto.Email;

            if (curPerson == null)
            {
                person.Patient.Add(patient);
                users.Person = person;
            }
            else
            {
                users.PersonId = curPerson.Id;
            }

            users.UserRolePermission.Add(new UserRolePermission { RoleId = (int)RoleEnum.Patient, SectionId = 1, PermissionId = null });


            return users;
        }


        public static Users MapUserForCenter(UserCenterRegisterDto userDto, int? roleId, int? sectionId, int? permissionId)
        {
            Users users = new Users();
            Person person = new Person();

            users.CreateDate = DateTime.Now;
            users.UserName = userDto.UserName;
            users.Password = Utilities.ComputeHashSHA256(userDto.Password);

            if (userDto.PersonId != null && userDto.PersonId > 0)
            {
                users.PersonId = userDto.PersonId;
            }
            else
            {

                person.Mobile = userDto.Mobile;
                person.FirstName = userDto.Name;
                users.Person = person;
            }

            users.UserRolePermission.Add(new UserRolePermission { RoleId = roleId, SectionId = sectionId, PermissionId = permissionId });


            return users;
        }

        public static UserPersonDto UserMap(Users users)
        {
            return new UserPersonDto
            {
                PersonId=users.PersonId,
                Age = users.Person.Age,
                BirthDate = users.Person.BirthDate.ToDateStringTry(),
                FirstName = users.Person.FirstName,
                LastName = users.Person.LastName,
                FatherName = users.Person.FatherName,
                GrandFatherName = users.Person.GrandFatherName,
                Sex = users.Person.SexId != null ? users.Person.Sex.Title : "",
                SexId = users.Person.SexId,
                BirthPlaceId = users.Person.BirthPlaceId,
                UserId = users.Id,
                Email = users.Person.Email,
                NationalCode = users.Person.NationalCode,
                Phone = users.Person.Phone,
                BirthPlace = users.Person.BirthPlaceId != null ? users.Person.BirthPlace.Title : "",
                GreatGrandFatherName = users.Person.GreatGrandFatherName,
                LatinName = users.Person.LatinName,
                Mobile = users.Person.Mobile,
                Note = users.Person.Note,
                BloodGroup = users.Person?.Patient?.Select(g => g.BloodGroup != null ? g.BloodGroup.Title : "").FirstOrDefault(),
                BloodGroupId = users.Person?.Patient?.Select(g => g?.BloodGroupId).FirstOrDefault(),
                MaritalStatus = users.Person?.MaritalStatus?.Title,
                MaritalStatusId = users.Person?.MaritalStatus?.Id,
                NhsNumber = users.UserCardCode.Where(x=>x.IsCurrent==true).Select(x=>x.CardCode.HealthNumber).FirstOrDefault(),
                IsSmooking= users.Person?.Patient?.Select(g => g.PatientExtraInfo != null ? g.PatientExtraInfo.Select(y=>y.IsSmoking).FirstOrDefault() : null).FirstOrDefault(),
                IsDrinking= users.Person?.Patient?.Select(g => g.PatientExtraInfo != null ? g.PatientExtraInfo.Select(y=>y.IsDrinking).FirstOrDefault() : null).FirstOrDefault(),
                Rh= users.Person?.Patient?.Select(g => g.PatientExtraInfo != null ? g.PatientExtraInfo.Select(y=>y.Rh!=null? y.Rh.Title:null).FirstOrDefault() : null).FirstOrDefault(),
                RhId= users.Person?.Patient?.Select(g => g.PatientExtraInfo != null ? g.PatientExtraInfo.Select(y=> y.RhId).FirstOrDefault() : null).FirstOrDefault(),
            };
        }

        public static Users UserMap(Users users, UserPersonDto userPersonDto)
        {
            users.Person.Age = userPersonDto.Age;
            users.Person.BirthDate = userPersonDto.BirthDate.TryToDateTime();
            users.Person.FirstName = userPersonDto.FirstName;
            users.Person.LastName = userPersonDto.LastName;
            users.Person.FatherName = userPersonDto.FatherName;
            users.Person.GrandFatherName = userPersonDto.GrandFatherName;
            users.Person.SexId = userPersonDto.SexId;
            users.Person.Email = userPersonDto.Email;
            users.Person.NationalCode = userPersonDto.NationalCode;
            users.Person.Phone = userPersonDto.Phone;
            users.Person.Mobile = userPersonDto.Mobile;
            users.Person.BirthPlaceId = userPersonDto.BirthPlaceId;
            users.Person.Note = userPersonDto.Note;


            users.Person.LatinName = userPersonDto.LatinName;
            users.Person.GreatGrandFatherName = userPersonDto.GreatGrandFatherName;
            users.Person.BirthPlaceId = userPersonDto.BirthPlaceId;
            users.Person.MaritalStatusId = userPersonDto.MaritalStatusId;

            if(users.Person.Patient.TryAny())
            {
                users.Person.Patient.FirstOrDefault().BloodGroupId = userPersonDto.BloodGroupId;
                if (users.Person.Patient.FirstOrDefault().PatientExtraInfo?.Count>0)
                {
                    users.Person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().IsDrinking = userPersonDto.IsDrinking;
                    users.Person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().IsSmoking = userPersonDto.IsSmooking;
                    users.Person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().RhId = userPersonDto.RhId;
                }
                else
                {
                    users.Person.Patient.FirstOrDefault().PatientExtraInfo.Add(new PatientExtraInfo
                    {
                        IsDrinking = userPersonDto.IsDrinking,
                        IsSmoking = userPersonDto.IsSmooking,
                        RhId = userPersonDto.RhId,
                    });
                }
            }
          

            return users;
        }

        public static UserCardCode MapUserCardCode(int userId, DateTime cardExpire,CardCode cardCode) => new UserCardCode
        {
            UserId = userId,
            CardCodeId = cardCode.Id,
            CreateDate = DateTime.Now,
            ExpireDate = cardExpire,
            IsCurrent = true
        };

    }
}
