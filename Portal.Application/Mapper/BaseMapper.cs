using His.Reception.DTO;
using Portal.DTO;
using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.Mapper
{
    public class BaseMapper 
    {
        public static string Language { get; set; }
        public static BaseDto Map(object obj)
        {
            return new BaseDto
            {
                Id = Convert.ToInt32(obj.GetType().GetProperty("Id")?.GetValue(obj) ?? 0),
                //Title = obj.GetType().GetProperty("Title")?.GetValue(obj)?.ToString(),
                 TitleLang2 = obj.GetType().GetProperty("TitleLang2")?.GetValue(obj)?.ToString(),
                //tranlate title by language
                Title = Utilities.Language.GetTilteByLang(obj, Utilities.Language.CurrentLanguage),
                Note = Utilities.Language.GetNoteByLang(obj, Utilities.Language.CurrentLanguage),
                Code1 = obj.GetType().GetProperty("Code1")?.GetValue(obj)?.ToString(),
                Code2 = obj.GetType().GetProperty("Code2")?.GetValue(obj)?.ToString(),
                IsAdmin = Boolean.Parse((string)(obj.GetType().GetProperty("IsAdmin")?.GetValue(obj) ?? false).ToString()),
                IsActive = Boolean.Parse((string)(obj.GetType().GetProperty("IsActive")?.GetValue(obj) ?? false).ToString()),
                NoteLang2 = obj.GetType().GetProperty("NoteLang2")?.GetValue(obj)?.ToString(),
                Arrange = obj.GetType()?.GetProperty("Arrange")?.GetValue(obj)?.ToString()
            };
        }

        public static object Map(object obj,BaseDto baseDto)
        {
            obj.GetType().GetProperty("Id")?.SetValue(obj, baseDto.Id,null);
            obj.GetType().GetProperty("Title")?.SetValue(obj, baseDto.Title);
            obj.GetType().GetProperty("TitleLang2")?.SetValue(obj, baseDto.TitleLang2);
            obj.GetType().GetProperty("Note")?.SetValue(obj, baseDto.Note);
            obj.GetType().GetProperty("NoteLang2")?.SetValue(obj, baseDto.NoteLang2);
            obj.GetType().GetProperty("Code1")?.SetValue(obj, baseDto.Code1);
            obj.GetType().GetProperty("Code2")?.SetValue(obj, baseDto.Code2);
            obj.GetType().GetProperty("IsAdmin")?.SetValue(obj, baseDto.IsAdmin);
            obj.GetType().GetProperty("IsActive")?.SetValue(obj, baseDto.IsActive);

            return obj;
        }

        public static object MapColSystem(object obj, BaseDto baseDto)
        {
            // obj.GetType().GetProperty("Id")?.SetValue(obj, baseDto.Id, null);
            obj.GetType().GetProperty("Title")?.SetValue(obj, baseDto.Title);
            obj.GetType().GetProperty("TitleLang2")?.SetValue(obj, baseDto.TitleLang2);
            obj.GetType().GetProperty("Note")?.SetValue(obj, baseDto.Note);
            obj.GetType().GetProperty("NoteLang2")?.SetValue(obj, baseDto.NoteLang2);
            obj.GetType().GetProperty("Code1")?.SetValue(obj, baseDto.Code1);
            obj.GetType().GetProperty("Code2")?.SetValue(obj, baseDto.Code2);
            obj.GetType().GetProperty("IsAdmin")?.SetValue(obj, baseDto.IsAdmin);
            obj.GetType().GetProperty("IsActive")?.SetValue(obj, baseDto.IsActive);

            return obj;
        }
    }
}
