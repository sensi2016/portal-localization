using System;
using System.Collections.Generic;
using System.Text;
using Portal.DTO;
using Portal.DTO.City;
using Portal.Entities.Models;

namespace Portal.Application.Mapper
{
    public class ZoneMapper
    {
        public static ZoneDto MapToDto(Zone model)
        {
            return new ZoneDto
            {
                Id = model.Id,
                Title = model.Title,
                TitleLang2 = model.TitleLang2,
                ParentId = model.ParentId,
                ParentTitle = model.Parent?.Title,
                LevelId = model.LevelId,
                CityId = model.CityId,
                CityTitle = model.City?.Title,
                Note = model.Note,
                IsAdmin = model.IsAdmin,
                Code1 = model.Code1,
                Code2 = model.Code2,
                NoteLang2 = model.NoteLang2,
                IsActive = model.IsActive,
            };
        }

        public static Zone MapToModel(ZoneDto dto, Zone model = null)
        {
            if (model is null) model = new Zone();

            model.Id = dto.Id;
            model.Title = dto.Title;
            model.TitleLang2 = dto.TitleLang2;
            model.ParentId = dto.ParentId;
            model.LevelId = dto.LevelId;
            model.CityId = dto.CityId;
            model.Note = dto.Note;
            model.IsAdmin = dto.IsAdmin;
            model.Code1 = dto.Code1;
            model.Code2 = dto.Code2;
            model.NoteLang2 = dto.NoteLang2;
            model.IsActive = dto.IsActive;

            return model;
        }
    }
}
