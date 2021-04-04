using System;
using System.Collections.Generic;

namespace Portal.Mapper
{
    public static class MyMapper<TSource, TSourceDto> where TSource : class where TSourceDto : class
    {

        public static TSourceDto Copy(TSource source, TSourceDto dto)
        {
            if (source == null)
            {
                return null;
            }

            var sourceProperties = source.GetType().GetProperties();
            var dtoProperties = dto.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var dtoProperty in dtoProperties)
                {
                    if (sourceProperty.Name == dtoProperty.Name && sourceProperty.PropertyType == dtoProperty.PropertyType)
                    {
                        dtoProperty.SetValue(dto, sourceProperty.GetValue(source));
                        break;
                    }
                }
            }

            return dto;
        }

        public static TSourceDto Update(TSource source, TSourceDto dto)
        {
            if (source == null)
            {
                return null;

            }

            var sourceProperties = source.GetType().GetProperties();
            var dtoProperties = dto.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                if (sourceProperty.GetValue(source) != null && sourceProperty.Name.ToLower() != "id")
                {
                    foreach (var dtoProperty in dtoProperties)
                    {
                        if (sourceProperty.Name == dtoProperty.Name && sourceProperty.PropertyType == dtoProperty.PropertyType)
                        {
                            dtoProperty.SetValue(dto, sourceProperty.GetValue(source));
                            break;
                        }
                    }
                }
            }

            return dto;
        }

        public static TSourceDto Copy(TSource source)
        {
            if (source == null)
            {
                return null;
            }

            TSourceDto dto = (TSourceDto)Activator.CreateInstance(typeof(TSourceDto));
             Copy(source, dto);

            return dto;
        }

        public static List<TSourceDto> Copy(List<TSource> source)
        {
            List<TSourceDto> list = (List<TSourceDto>)Activator.CreateInstance(typeof(List<TSourceDto>));
            if (source == null)
            {
                return list;
            }

            foreach (TSource item in source)
            {
                list.Add(Copy(item));
            }

            return list;
        }
    }
}
