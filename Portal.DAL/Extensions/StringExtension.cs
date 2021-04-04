using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace Portal.DAL.Extensions
{
    public static class StringExtension
    {
        public static int TryToInt(this string value)
        {
            int.TryParse(value, out var intValue);
            return intValue;
        }

        public static long TryToLong(this string value)
        {
            long.TryParse(value, out var longValue);
            return longValue;
        }

        public static string NormalizeForCompare(this string value) => value.TryToString().ToLower().Replace(" ", string.Empty);

        public static bool CompareString(this string value, string compareValue) => value.NormalizeForCompare() == compareValue.NormalizeForCompare();

        public static string TryToLower(this string value) => string.IsNullOrEmpty(value) ? "" : value.ToLower();


        public static decimal TryToDecimal(this string value)
        {
            decimal.TryParse(value, out var longValue);
            return longValue;
        }

        public static double TryToDouble(this string value)
        {
            double.TryParse(value, out var longValue);
            return longValue;
        }

        public static DateTime? TryToDateTime(this string value)
        {
            DateTime.TryParse(value, out var dateTimeValue);

            if (dateTimeValue == DateTime.MinValue) return null;

            return dateTimeValue;
        }

        public static bool IsValidDateTime(this string value)
        {
            return DateTime.TryParse(value, out var longValue);
        }

        public static bool IsValidateJSON(this string s)
        {
            try
            {
                JToken.Parse(s);
                return true;
            }
            catch (JsonReaderException ex)
            {
                return false;
            }
        }

        public static string FirstCharToUpper(this string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }

        public static string FirstCharToLower(this string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToLower() + input.Substring(1);
        }
    }
}
