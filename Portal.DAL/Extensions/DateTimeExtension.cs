using Portal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.DAL.Extensions
{
  public static class DateTimeExtension
    {
        public static string ToShortDateTimeString(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm");
        }

        public static string ToShortDateStringExtend(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
        }

        public static bool IsValidDateTime(this DateTime value)
        {
            return value > DateTime.MinValue;
        }

        public static bool IsValidDateTime(this DateTime? value)
        {
            return value!=null && value > DateTime.MinValue;
        }

        public static string ToDateStringTry(this DateTime? dateTime)
        {
            return dateTime !=null ? dateTime.GetValueOrDefault().ToString(Utilities.Date.FormatDate.Date) :"";
        }

        public static string ToDateStringForSlashTry(this DateTime? dateTime)
        {
            return dateTime != null ? dateTime.GetValueOrDefault().ToString(Utilities.Date.FormatDate.DateForSlash) : "";
        }

        public static string ToDateTimeStringTry(this DateTime? dateTime)
        {
            return dateTime != null ? dateTime.GetValueOrDefault().ToString(Utilities.Date.FormatDate.DateTime) : "";
        }

        public static string TryToDateString(this DateTime? dateTime)=>dateTime != null ? dateTime.GetValueOrDefault().ToDateString() : "";

        public static string ToDateString(this DateTime dateTime)=>dateTime != DateTime.MinValue ? dateTime.ToString(Utilities.Date.FormatDate.Date) : "";

        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime != null ? dateTime.ToString(Utilities.Date.FormatDate.DateTime) : "";
        }

        public static int CompareInDays(this DateTime firstDateTime, DateTime secondDateTime) => Convert.ToInt32(secondDateTime.Subtract(firstDateTime).TotalDays);

        public static DateTime StartWeek(this DateTime value, bool thisMonth)
        {
            var startWeek = value;
            var dayOfWeek = (int)value.DayOfWeek + 1;
            if (dayOfWeek < 7) startWeek = value.AddDays(dayOfWeek * -1);

            if (thisMonth && value.Month != startWeek.Month)
            {
                for (int i = 1; i <= 6; i++)
                {
                    startWeek = startWeek.AddDays(1);
                    if (value.Month == startWeek.Month) break;
                }
            }

            return startWeek.Date;
        }

        public static DateTime EndWeek(this DateTime value, bool thisMonth)
        {
            var endWeek = value;

            var dayOfWeek = (int)value.DayOfWeek + 1;
            if (dayOfWeek <= 6) endWeek = value.AddDays(6 - dayOfWeek);
            if (dayOfWeek == 7) endWeek = value.AddDays(6);

            if (thisMonth && value.Month != endWeek.Month)
            {
                for (int i = 1; i <= 6; i++)
                {
                    endWeek = endWeek.AddDays(-1);
                    if (value.Month == endWeek.Month) break;
                }
            }

            return endWeek.Date;
        }
    }
}
