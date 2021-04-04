using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Portal.DAL.Extensions
{
   public static class ListExtension
    {
        public static bool TryAny<T>(this List<T> value)
        {
            return value?.Count > 0;
        }

        public static bool TryAny<T>(this ICollection<T> value)
        {
            return value?.Count > 0;
            //if (value == null)
            //{
            //    return true;
            //}

            //return value.Any();
        }

        public static int TryCount<T>(this List<T> list) => list.TryAny() ? list.Count : 0;


        public static List<int> DistinctIntColumn<T>(this List<T> value,string propertyName)
        {
            return value.Select(x => x.GetIntProperty(propertyName)).Distinct().ToList();
        }

        public static List<long> DistinctLongColumn<T>(this List<T> value, string propertyName)
        {
            return value.Select(x => x.GetLongProperty(propertyName)).Distinct().ToList();
        }

        public static List<string> DistinctStringColumn<T>(this List<T> value, string propertyName)
        {
            return value.Where(x=> x.GetStringProperty(propertyName)!=string.Empty)
                .Select(x => x.GetStringProperty(propertyName)).Distinct().ToList();
        }

        public static List<T> ToPaging<T>(this List<T> value, int pageSize, int pageNumber)
        {
            return value.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }
    }
}
