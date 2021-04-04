using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DAL.Extensions
{
    public static class ObjectExtension
    {
        public static string GetStringProperty(this object value, string propertyName)
        {
            return Convert.ToString(value.GetType().GetProperty(propertyName)?.GetValue(value) ?? "");
        }

        public static string TryToString(this object obj) =>  obj is null?"":obj.ToString();

        public static long TryToLong(this object obj)
        {
            long.TryParse(obj.TryToString(), out var longValue);
            return longValue;
        }

        public static int GetIntProperty(this object value, string propertyName)
        {
            return Convert.ToInt32(value.GetType().GetProperty(propertyName)?.GetValue(value) ?? 0);
        }

        public static long GetLongProperty(this object value, string propertyName)
        {
            return Convert.ToInt64(value.GetType().GetProperty(propertyName)?.GetValue(value) ?? 0);
        }

        public static DateTime GetDateTimeProperty(this object value, string propertyName)
        {
            return Convert.ToDateTime(value.GetType().GetProperty(propertyName)?.GetValue(value) ?? DateTime.MinValue);
        }

        public static void SetStringProperty(this object value, string propertyName, string setValue)
        {
            value.GetType().GetProperty(propertyName)?.SetValue(value, setValue);
        }

        public static void SetIntProperty(this object value, string propertyName,int setValue)
        {
            value.GetType().GetProperty(propertyName)?.SetValue(value,setValue);
        }

        public static void SetLongProperty(this object value, string propertyName, long setValue)
        {
            value.GetType().GetProperty(propertyName)?.SetValue(value, setValue);
        }
    }
}
