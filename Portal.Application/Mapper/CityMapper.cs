using System;
using System.Collections.Generic;
using System.Text;
using Portal.DTO;
using Portal.Entities.Models;

namespace Portal.Application.Mapper
{
    public class CityMapper
    {
        public static CityDto MapToDto(City model)
        {
            return new CityDto
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
                ProvinceId = model.ProvinceId,
                ProvinceTitle = model.Province?.Title
            };
        }

        public static City MapToModel(CityDto dto, City model = null)
        {
            if (model is null) model = new City();

            model.Id = dto.Id;
            model.Title = dto.Title;
            model.TitleLang2 = dto.TitleLang2;
            model.Note = dto.Note;
            model.IsAdmin = dto.IsAdmin;
            model.Code1 = dto.Code1;
            model.Code2 = dto.Code2;
            model.NoteLang2 = dto.NoteLang2;
            model.IsActive = dto.IsActive;
            model.ProvinceId = dto.ProvinceId;

            return model;
        }
    }
}
