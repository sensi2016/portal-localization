using Portal.DTO;
using Portal.DTO.Center;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Portal.DAL.Extensions;

namespace Portal.Application.Mapper
{
    public class CenterMapper
    {
        public static ResponseCenterDto Map(Center center)
        {
            var dto = new ResponseCenterDto
            {
                Id = center.Id,
                IsFreeDelivery = center.IsFreeDelivery,
                Address = center.Address,
                Title = center.Title,
                AnswerTime = center.AnswerTime,
                AreaCode = center.AreaCode,
                Boss = center.Boss,
                BossPhone = center.BossPhone,
                CityId = center.CityId,
                CountryId = center.CountryId,
                ProvinceId = center.ProvinceId,
                VisitorPhone = center.VisitorPhone,
                VisitorOrManaging = center.VisitorOrManaging,
                WorkingHourse = center.WorkingHours,
                ZoneId = center.ZoneId,
                EconomicalId = center.EconomicalId,
                Phone = center.Phone,
                Mobile = center.Mobile,
                Note = center.Note,
                PostalCode = center.PostalCode,
                Latitude = center.Latitude,
                Longitude = center.Latitude,
                Fax = center.Fax,
                IsActive = center.IsActive,
                ExamTime = center.ExamTime,
                IsHomeDelivery = center.IsHomeDelivery,
                CenterTypeId = center.CenterTypeId,
                OwnershipTypeId = center.OwnershipTypeId,
                Email = center.Email,
                PhoneResponseHourse = center.PhoneResponseHourse,
                ExamPlaces = center?.CenterExamPlace?.Select(g => new ListMultiResponse<int> {Id = g.ExamplaceId.GetValueOrDefault(), Title = g?.Examplace?.Title}).ToList(),
                SellingTypes = center?.CenterSellingType?.Select(g => new ListMultiResponse<int> {Id = g.SellingTypeId.GetValueOrDefault(), Title = g?.SellingType?.Title}).ToList(),
                CenterServices = center?.CenterServices?.Select(g => new ListMultiResponse<int> {Id = g.ServiceId.GetValueOrDefault(), Title = g?.Service?.Title}).ToList(),
                WorkTimeTypeId = center?.CenterWorkItem?.Select(g => new ListMultiResponse<int> {Id = g.WorkItemId.GetValueOrDefault(), Title = g?.WorkItem?.Title}).ToList()
            };

            return dto;
        }

        public static Center Map(CenterDto dto)
        {
            var model = new Center
            {
                Address = dto.Address,
                IsFreeDelivery = dto.IsFreeDelivery,
                Title = dto.Title,
                AnswerTime = dto.AnswerTime,
                AreaCode = dto.AreaCode,
                Boss = dto.Boss,
                BossPhone = dto.BossPhone,
                CityId = dto.CityId,
                CountryId = dto.CountryId,
                ProvinceId = dto.ProvinceId,
                VisitorPhone = dto.VisitorPhone,
                VisitorOrManaging = dto.VisitorOrManaging,
                WorkingHours = dto.WorkingHourse,
                ZoneId = dto.ZoneId,
                EconomicalId = dto.EconomicalId,
                Phone = dto.Phone,
                Mobile = dto.Mobile,
                Note = dto.Note,
                PostalCode = dto.PostalCode,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Fax = dto.Fax,
                IsActive = true,
                ExamTime = dto.ExamTime,
                CenterTypeId = dto.CenterTypeId,
                IsHomeDelivery = dto.IsHomeDelivery,
                OwnershipTypeId = dto.OwnershipTypeId,
                Email = dto.Email,
                PhoneResponseHourse = dto.PhoneResponseHourse
            };

            MapListProperties(dto, model);

            var section = SectionMap(dto);
            model.Section.Add(section);

            return model;
        }

        public static Center Map(Center model, CenterDto dto)
        {
            model.Address = dto.Address;
            model.IsFreeDelivery = dto.IsFreeDelivery;
            model.Title = dto.Title;
            model.AnswerTime = dto.AnswerTime;
            model.AreaCode = dto.AreaCode;
            model.Boss = dto.Boss;
            model.BossPhone = dto.BossPhone;
            model.CityId = dto.CityId;
            model.CountryId = dto.CountryId;
            model.ProvinceId = dto.ProvinceId;
            model.VisitorPhone = dto.VisitorPhone;
            model.VisitorOrManaging = dto.VisitorOrManaging;
            model.WorkingHours = dto.WorkingHourse;
            model.ZoneId = dto.ZoneId;
            model.EconomicalId = dto.EconomicalId;
            model.Phone = dto.Phone;
            model.Mobile = dto.Mobile;
            model.Note = dto.Note;
            model.PostalCode = dto.PostalCode;
            model.CenterTypeId = dto.CenterTypeId;
            model.IsHomeDelivery = dto.IsHomeDelivery;
            model.Latitude = dto.Latitude;
            model.Longitude = dto.Longitude;
            model.Fax = dto.Fax;
            model.IsActive = true;
            model.ExamTime = dto.ExamTime;
            model.OwnershipTypeId = dto.OwnershipTypeId;
            model.Email = dto.Email;
            model.PhoneResponseHourse = dto.PhoneResponseHourse;

            MapListProperties(dto, model);

            //ویرایش بخش که رابط یک به یک است
            SectionMap(model.Section.FirstOrDefault(), dto);

            return model;
        }

        private static string GetTitle(string title, CenterTypeEnum centerTypeEnum)
        {

            switch (centerTypeEnum)
            {
                case CenterTypeEnum.Laboratory:

                    return $"Labratory - {title}";

                case CenterTypeEnum.Pharmacy:

                    return $"Pharmacy - {title}";

                case CenterTypeEnum.Radiology:

                    return $"Radiology - {title}";
                
                case CenterTypeEnum.DoctorOffice:
                    return $"DoctorOffice - {title}";

                case CenterTypeEnum.Hospital:
                    return $"Hospital - {title}";

                default: return null;
            }

        }

        private static int? GetSectionFieldId(CenterTypeEnum centerTypeEnum)
        {

            switch (centerTypeEnum)
            {
                case CenterTypeEnum.Laboratory:
                    return (int)SectionFieldEnum.Lab;

                case CenterTypeEnum.Pharmacy:
                    return (int)SectionFieldEnum.Pharmacy;

                case CenterTypeEnum.DoctorOffice:
                    return (int)SectionFieldEnum.DoctorOffice;

                case CenterTypeEnum.Hospital:
                    return (int)SectionFieldEnum.Hospital;
               
                default: return null;
            }


        }

        public static Section SectionMap(CenterDto centerDto)
        {
            return new Section
            {
                Title = centerDto.CenterTypeId!=null? GetTitle(centerDto.Title, (CenterTypeEnum)centerDto.CenterTypeId.GetValueOrDefault()):"",
                Address = centerDto.Address,
                Phone = centerDto.Phone,
                SectionFieldId = centerDto.CenterTypeId != null ? GetSectionFieldId((CenterTypeEnum)centerDto.CenterTypeId):null,
                SectionKindId = 1,
                IsActive = true
            };
        }

        public static void SectionMap(Section section, CenterDto centerDto)
        {
            section.Title = GetTitle(centerDto.Title, (CenterTypeEnum)centerDto.CenterTypeId.GetValueOrDefault());
            section.Address = centerDto.Address;
            section.Phone = centerDto.Phone;
            section.SectionFieldId = GetSectionFieldId((CenterTypeEnum)centerDto.CenterTypeId);
            section.SectionKindId = 1;
            section.IsActive = true;

        }

        public static Expression<Func<Center,ListCenterDto>> MapList
        {
            get
            {
                return x => new ListCenterDto 
                {
                    Id = x.Id,
                    Address = x.Address,
                    Phone = x.Phone,
                    Title = x.Title,
                    Place =(x.Province != null ? x.Province.Title + (x.City != null ? "-" : "") : "") + (x.City != null ? x.City.Title + (x.Zone != null ? "-" : "") :"") +( x.Zone != null ? x.Zone.Title : ""),
                   
                };
            }
            
        }

        public static Expression<Func<Center, ListHomeCenterDto>> MapListHome
        {
            get
            {
                return x => new ListHomeCenterDto
                {
                    Id = x.Id,
                    Address = x.Address,
                    Phone = x.Phone,
                    Title = x.Title,
                    //Place = (x.Province != null ? x.Province.Title + (x.City != null ? "-" : "") : "") + (x.City != null ? x.City.Title + (x.Zone != null ? "-" : "") : "") + (x.Zone != null ? x.Zone.Title : ""),
                    Examplaces = x.CenterExamPlace.Select(t => new
                    {
                        t.ExamplaceId,
                        t.Examplace.Title
                    }).ToList(),
                    SellTypes = x.CenterSellingType.Select(t => new
                    {
                        t.SellingTypeId,
                        t.SellingType.Title
                    }).ToList(),
                    CenterService=x.CenterServices.Select(t => new
                    {
                        t.ServiceId,
                        t.Service.Title
                    }).ToList(),
                    WorkItemType = x.CenterWorkItem.Select(t => new
                    {
                        t.WorkItemId,
                        t.WorkItem.Title
                    }).ToList(),
                    OwnershipType=x.OwnershipType !=null ?  x.OwnershipType.Title :"",
                    IsHomeDelivery=x.IsHomeDelivery
                };
            }

        }

        private static void MapListProperties(CenterDto dto, Center model)
        {
            if (dto.ExamPlaces.TryAny())
            {
                model.CenterExamPlace = dto.ExamPlaces
                    .Select(x => new CenterExamPlace { ExamplaceId = x.Id }).ToList();
            }

            if (dto.SellingTypes.TryAny())
            {
                model.CenterSellingType = dto.SellingTypes
                    .Select(x => new CenterSellingType { SellingTypeId = x.Id }).ToList();
            }

            if (dto.CenterServices.TryAny())
            {
                model.CenterServices = dto.CenterServices
                    .Select(x => new CenterServices { ServiceId = x.Id }).ToList();
            }

            if (dto.WorkTimeTypeId.TryAny())
            {
                model.CenterWorkItem = dto.WorkTimeTypeId
                    .Select(x => new CenterWorkItem { WorkItemId = x.Id }).ToList();
            }
        }
    }
}
