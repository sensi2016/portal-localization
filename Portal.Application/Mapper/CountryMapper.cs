using Portal.DTO;
using Portal.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Mapper
{
    public class ProvinceMapper
    {
        public static ProvinceDto MapToDto(Province model)
        {
            return new ProvinceDto
            {
                Id = model.Id,
                Title = model.Title,
                TitleLang2 = model.TitleLang2,
                Note = model.Note,
                IsAdmin = model.IsAdmin,
                Code1 = model.Code1,
                Code2 = model.Code2,
                NoteLang2 = model.NoteLang2,
                IsActive = model.IsActive,
                ConteryId = model.CountryId,
                ConteryTitle = model.Country?.Title
            };
        }

        public static Province MapToModel(ProvinceDto dto, Province model = null)
        {
            if (model is null) model = new Province();

            model.Id = dto.Id;
            model.Title = dto.Title;
            model.TitleLang2 = dto.TitleLang2;
            model.Note = dto.Note;
            model.IsAdmin = dto.IsAdmin;
            model.Code1 = dto.Code1;
            model.Code2 = dto.Code2;
            model.NoteLang2 = dto.NoteLang2;
            model.IsActive = dto.IsActive;
            model.CountryId = dto.ConteryId;

            return model;
        }
    }
}
