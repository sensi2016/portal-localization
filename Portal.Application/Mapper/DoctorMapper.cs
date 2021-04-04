using Portal.DAL.Extensions;
using Portal.DTO;
using Portal.DTO.Doctor;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Portal.Application.Mapper
{
    public class DoctorMapper
    {
        public static Doctors Map(DoctorDto doctorDto)
        {
            var doctors = new Doctors();
            var center = new Center();
            var person = new Person();

            doctors.ExpertiseId = doctorDto.ExpertiseId;
            doctors.MedicalSystemNo = doctorDto.MedicalSystemNo;
            doctors.CostVisit = doctorDto.CostVisit;
            doctors.ScientificlevelId = doctorDto.ScientificlevelId;
            doctors.IsActive = doctorDto.IsActive;

            //center
            center.Address = doctorDto.Address;
            center.Title = $"{doctorDto.FirstName} {doctorDto.LastName}";
            center.AreaCode = doctorDto.AreaCode;
            center.CityId = doctorDto.CityId;
            center.CountryId = doctorDto.CountryId;
            center.ProvinceId = doctorDto.ProvinceId;
            center.WorkingHours = doctorDto.WorkingHourse;
            center.ZoneId = doctorDto.ZoneId;
            center.Phone = doctorDto.Phone;
            center.Mobile = doctorDto.Mobile;
            center.Note = doctorDto.Note;
            center.PostalCode = doctorDto.PostalCode;
            center.Latitude = doctorDto.Latitude;
            center.Longitude = doctorDto.Longitude;
            center.Fax = doctorDto.Fax;
            center.IsActive = true;
            center.PhoneResponseHourse = doctorDto.PhoneResponseHourse;
            center.CenterTypeId = doctorDto.CenterTypeId;

            //person
            person.Mobile = doctorDto.Mobile;
            person.FirstName = doctorDto.FirstName;
            person.LastName = doctorDto.LastName;
            person.Age = doctorDto.Age;
            person.SexId = doctorDto.SexId;
            person.NationalCode = doctorDto.NationalCode;
            person.Email = doctorDto.Email;

            if (doctorDto.Certificates.TryAny())
            {
                doctors.DoctorCertificate = new List<DoctorCertificate>();
                foreach (var certificate in doctorDto.Certificates) 
                    doctors.DoctorCertificate.Add(new DoctorCertificate { CertificateId = certificate.Id });
            }

            if (doctorDto.Visits.TryAny())
            {
                doctors.DoctorVisitType = new List<DoctorVisitType>();
                foreach (var item in doctorDto.Visits) doctors.DoctorVisitType.Add(new DoctorVisitType { VisitTypeId = item.Id });
            }

            if (doctorDto.ExamPlaces.TryAny())
            {
                center.CenterExamPlace = new List<CenterExamPlace>();
                foreach (var item in doctorDto.ExamPlaces) center.CenterExamPlace.Add(new CenterExamPlace { ExamplaceId = item.Id });
            }

            if (doctorDto.WorkTimeTypeId.TryAny())
            {
                center.CenterWorkItem = new List<CenterWorkItem>();
                foreach (var item in doctorDto.WorkTimeTypeId) center.CenterWorkItem.Add(new CenterWorkItem { WorkItemId = item.Id });
            }

            if (doctorDto.Certificates.TryAny())
            {
                doctors.DoctorCertificate= new List<DoctorCertificate>();
                foreach (var certificate in doctorDto.Certificates) doctors.DoctorCertificate.Add(new DoctorCertificate { CertificateId = certificate.Id});
            }

            var centerDto = new CenterDto
            {
                Title = center.Title,
                Address = doctorDto.Address,
                Phone = doctorDto.Phone,
                CenterTypeId = center.CenterTypeId
            };

            var section = CenterMapper.SectionMap(centerDto);
            center.Section.Add(section);

            doctors.Center = center;
            doctors.Person = person;

            return doctors;
        }

        public static Doctors Map(Doctors doctors, DoctorDto doctorDto)
        {
            doctors.ExpertiseId = doctorDto.ExpertiseId;
            doctors.MedicalSystemNo = doctorDto.MedicalSystemNo;
            doctors.CostVisit = doctorDto.CostVisit;
            doctors.ScientificlevelId = doctorDto.ScientificlevelId;
            doctors.IsActive = doctorDto.IsActive;

            //center
            var center = doctors.Center;
            center.Address = doctorDto.Address;
            center.Title = $"{doctorDto.FirstName} {doctorDto.LastName}";
            center.AreaCode = doctorDto.AreaCode;
            center.CityId = doctorDto.CityId;
            center.CountryId = doctorDto.CountryId;
            center.ProvinceId = doctorDto.ProvinceId;
            center.PhoneResponseHourse = doctorDto.PhoneResponseHourse;
            center.WorkingHours = doctorDto.WorkingHourse;
            center.ZoneId = doctorDto.ZoneId;
            center.Phone = doctorDto.Phone;
            center.Mobile = doctorDto.Mobile;
            center.Note = doctorDto.Note;
            center.PostalCode = doctorDto.PostalCode;
            center.Latitude = doctorDto.Latitude;
            center.Longitude = doctorDto.Longitude;
            center.Fax = doctorDto.Fax;
            center.CenterTypeId = doctorDto.CenterTypeId;

            //person
            var person = doctors.Person;
            person.Mobile = doctorDto.Mobile;
            person.FirstName = doctorDto.FirstName;
            person.LastName = doctorDto.LastName;
            person.Age = doctorDto.Age;
            person.SexId = doctorDto.SexId;
            person.NationalCode = doctorDto.NationalCode;
            person.Email = doctorDto.Email;

            if (doctorDto.Visits.TryAny())
            {
                doctors.DoctorVisitType = new List<DoctorVisitType>();
                foreach (var item in doctorDto.Visits) doctors.DoctorVisitType.Add(new DoctorVisitType { VisitTypeId = item.Id });
            }

            if (doctorDto.ExamPlaces.TryAny())
            {
                center.CenterExamPlace = new List<CenterExamPlace>();
                foreach (var item in doctorDto.ExamPlaces) center.CenterExamPlace.Add(new CenterExamPlace { ExamplaceId = item.Id });
            }

            if (doctorDto.WorkTimeTypeId.TryAny())
            {
                center.CenterWorkItem = new List<CenterWorkItem>();
                foreach (var item in doctorDto.WorkTimeTypeId) center.CenterWorkItem.Add(new CenterWorkItem { WorkItemId = item.Id });
            }

            if (doctorDto.Certificates.TryAny())
            {
                doctors.DoctorCertificate = new List<DoctorCertificate>();
                foreach (var certificate in doctorDto.Certificates) doctors.DoctorCertificate.Add(new DoctorCertificate { CertificateId = certificate.Id });
            }

            var centerDto = new CenterDto
            {
                Title = center.Title,
                Address = doctorDto.Address,
                Phone = doctorDto.Phone,
                CenterTypeId = center.CenterTypeId
            };
            CenterMapper.SectionMap(center.Section.FirstOrDefault(), centerDto);

            return doctors;
        }

        public static ResponseDoctorDto Map(Doctors doctors)
        {
            var responseDoctorDto = new ResponseDoctorDto();

            responseDoctorDto.Id = doctors.Id;
            responseDoctorDto.ExpertiseId = doctors.ExpertiseId;
            responseDoctorDto.ScientificlevelId = doctors.ScientificlevelId;
            responseDoctorDto.MedicalSystemNo = doctors.MedicalSystemNo;
            responseDoctorDto.CostVisit = doctors.CostVisit;

            if(doctors.DoctorCertificate.TryAny()) responseDoctorDto.Certificates = doctors.DoctorCertificate.Select(x => new IdTitle<int>{Id = x.CertificateId.GetValueOrDefault(),Title = x.Certificate?.Title}).ToList();
            responseDoctorDto.Visits = doctors.DoctorVisitType.Select(g => new ListMultiRequest<int>{Id = g.VisitTypeId.GetValueOrDefault()}).ToList();
            responseDoctorDto.WorkTimeTypeId = doctors.Center.CenterWorkItem.Select(g => new ListMultiResponse<int> {Id = g.WorkItemId.GetValueOrDefault()}).ToList();
            responseDoctorDto.ExamPlaces = doctors.Center.CenterExamPlace.Select(g => new ListMultiResponse<int> {Id = g.ExamplaceId.GetValueOrDefault()}).ToList();

            //person
            responseDoctorDto.FirstName = doctors?.Person?.FirstName;
            responseDoctorDto.LastName = doctors?.Person?.LastName;
            responseDoctorDto.Mobile = doctors?.Person?.Mobile;
            responseDoctorDto.NationalCode = doctors?.Person?.NationalCode;
            responseDoctorDto.Age = doctors?.Person?.Age;
            responseDoctorDto.SexId = doctors?.Person?.SexId;
            responseDoctorDto.PersonId = doctors?.PersonId;
            responseDoctorDto.Email = doctors?.Person?.Email;

            //center
            responseDoctorDto.Phone = doctors?.Center?.Phone;
            responseDoctorDto.Address = doctors?.Center?.Address;
            responseDoctorDto.Fax = doctors?.Center?.Fax;
            responseDoctorDto.ProvinceId = doctors?.Center?.ProvinceId;
            responseDoctorDto.CityId = doctors?.Center?.CityId;
            responseDoctorDto.CountryId = doctors?.Center?.CountryId;
            responseDoctorDto.CenterTypeId = doctors?.Center?.CenterTypeId;
            responseDoctorDto.ZoneId = doctors?.Center?.ZoneId;
            responseDoctorDto.AreaCode = doctors?.Center?.AreaCode;
            responseDoctorDto.PostalCode = doctors?.Center?.PostalCode;
            responseDoctorDto.Latitude = doctors?.Center?.Latitude;
            responseDoctorDto.Longitude = doctors?.Center?.Longitude;
            responseDoctorDto.Note = doctors?.Center?.Note;
            responseDoctorDto.WorkingHourse = doctors?.Center?.WorkingHours;
            responseDoctorDto.PhoneResponseHourse = doctors?.Center?.PhoneResponseHourse;
            responseDoctorDto.CenterId = doctors?.Center?.Id;
            responseDoctorDto.IsActive = doctors.IsActive;
            responseDoctorDto.UserId = doctors.Person?.Users.Select(x=>x.Id).FirstOrDefault();
            responseDoctorDto.UserName = doctors.Person?.Users.Select(x=>x.UserName).FirstOrDefault();

            return responseDoctorDto;
        }

        public static DoctorInfoDto MapInfo(Doctors doctors)
        {
            return new DoctorInfoDto
            {
                Id = doctors.Id,

                //person
                FirstName = doctors.Person?.FirstName,
                FullName = doctors.Person?.FullName,
                LastName = doctors.Person?.LastName,
                Mobile = doctors.Person?.Mobile,
                NationalCode = doctors.Person?.NationalCode,
                Age = doctors.Person?.Age,
                CostVisit = doctors.CostVisit,
                ExpertiseTitle = doctors.Expertise?.Title,
                ExpertiseTitleLang2 = doctors.Expertise?.TitleLang2,

                //center
                Phone = doctors.Center?.Phone,
                Address = doctors.Center?.Address,
                Email = doctors.Person?.Email,
                Note = doctors.Note,
                Latitude = doctors.Center?.Latitude,
                Longitude = doctors.Center?.Longitude,
                ExamPlaces = doctors.Center?.CenterExamPlace.Select(x => new ExamPlaceDto { Id = x.ExamplaceId.GetValueOrDefault(), Title = x.Examplace?.Title }).ToList(),
                WorkItemTypes = doctors.Center?.CenterWorkItem.Select(x => new WorkItemTypeDto { Id = x.WorkItemId.GetValueOrDefault(), Title = x.WorkItem?.Title }).ToList(),
                Certificates = doctors.DoctorCertificate?.Select(x => new CertificateDto
                {
                    Id = x.CertificateId.GetValueOrDefault(),
                    Title = x.Certificate?.Title,
                    TitleLang2 = x.Certificate?.TitleLang2
                }).ToList()
            };
        }

        public static Expression<Func<Doctors, ListDoctorDto>> MapList
        {
            get
            {
                return x => new ListDoctorDto
                {
                    Id = x.Id,
                    UserId = x.Person.Users != null? x.Person.Users.First().Id:0,
                    Age = x.Person.Age,
                    Sex = x.Person.Sex != null ? x.Person.Sex.Title : "",
                    MedicalSystemNo = x.MedicalSystemNo,
                    Name = x.Person != null ? x.Person.FullName : "",
                    NationalCode = x.Person.NationalCode,
                    Scientificlevel = x.Scientificlevel.Title,
                    CityAndZone = x.Center.City.Title,
                    Mobile = x.Person.Mobile,
                    Center = x.Center != null ? x.Center.Title : "",
                    PhoneClinic = x.PhoneClinic,
                    AddressClinic = x.AddressClinic,
                    CostVisit = x.CostVisit,
                    Expertise = x.Expertise != null ? x.Expertise.Title : "",
                    Scientificleve = x.Scientificlevel != null ? x.Scientificlevel.Title : "",
                    IsActive = x.IsActive,
                    IsHospitalization = x.IsHospitalization,
                    IsEmergency = x.IsEmergency,
                    IsOutpatient = x.IsOutpatient,
                    IsSurgeryRoom = x.IsSurgeryRoom,
                    CooperationDate = x.CooperationDate.ToDateStringTry()
                };
            }
        }

        public static Expression<Func<Doctors, ListDoctorHomeDto>> MapListHome
        {
            get
            {
                return x => new ListDoctorHomeDto
                {
                    Id = x.Id,
                    Address = x.Center.Address,
                    Mobile = x.Person.Mobile,
                    WorkingHours = x.Center.WorkingHours,
                    FullName = x.Person != null ? x.Person.FullName : "",
                    Expertise = x.Expertise != null ? x.Expertise.Title : "",
                    ExamplaceId = x.Center.CenterExamPlace != null ? x.Center.CenterExamPlace.Select(g => g.ExamplaceId).FirstOrDefault() : null,
                    WorkItemType = x.Center.CenterExamPlace != null ? x.Center.CenterWorkItem.Select(g => new ListMultiResponse<int> { Id = g.Id, Title = g.WorkItem.Title }).ToList() : null
                };
            }
        }
    }
}
