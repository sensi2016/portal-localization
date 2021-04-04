

using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Portal.DTO;
using Portal.DTO.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Infrastructure
{
    public class Utilities
    {
        public class GroupServiceCode
        {
            public const string Labratuary = "lab";
            public const string VitalSign = "vital";
            public const string MedicalHistory = "MedicalHistory";
            public const string Radilogy = "rad";
        }

        public class SectionCode
        {
            public const string Pcr= "Pcr";
            public const string LabHayatNajaf = "HayatNajaf";
        }
    

        public static string AppendText(string a,string b,string ch)
        {
            if(!string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(a))
            {
                return $"{a} - {b}";
            }

            return a + b;
        }
        public static string GenerateUniqCode()
        {
            StringBuilder builder = new StringBuilder();
            Enumerable
               .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(10)
                .ToList().ForEach(e => builder.Append(e));
            return builder.ToString();
        }

        public static int GetRoleId(CenterTypeEnum centerTypeEnum)
        {
            switch (centerTypeEnum)
            {
                case CenterTypeEnum.Laboratory:return (int)RoleEnum.LabCenter;
                case CenterTypeEnum.Pharmacy: return (int)RoleEnum.PharmacyCenter;
                case CenterTypeEnum.Radiology:return (int)RoleEnum.RadiloCenter;
                case CenterTypeEnum.DoctorOffice:return (int)RoleEnum.Admin;
                case CenterTypeEnum.Hospital:return (int)RoleEnum.Hospital;

                default:return 0;
            }
        }
        public class HttpStatusCodeException : Exception
        {
            public HttpStatusCode StatusCode { get; set; }

            public HttpStatusCodeException(HttpStatusCode statusCode)
            {
                StatusCode = statusCode;
            }

            public HttpStatusCodeException(HttpStatusCode statusCode, string message) : base(message)
            {
                StatusCode = statusCode;
            }

        }

        public static int IcdCodeId { get; set; } = 1;
        public static T MapTowObject<T>(object obj2)
        {
            return JsonConvert.DeserializeObject<T>(JObject.FromObject(obj2).ToString());
        }

        public class Date
        {
            public static double CalcTotalTime(TimeSpan startTime, TimeSpan endTime, bool isMinute = false, bool isHour = false)
            {
                if (isMinute)
                {
                    return endTime.Subtract(startTime).TotalMinutes;
                }
                else if (isHour)
                {
                    return endTime.Subtract(startTime).TotalHours;
                }

                return 0;
            }

            public static int GetTotalWeek(DateTime startDate, DateTime endDate)
            {
                int weeks = (int)((endDate.Date - startDate.Date).TotalDays / 7);

                return weeks;
            }

            public static string GetDayOfWeekGetName(DateTime date)
            {
                var dayOfWeek = date.DayOfWeek;
                //var culture = new System.Globalization.CultureInfo("en-US");
                //var dayName = culture.DateTimeFormat.GetDayName(dayOfWeek);
                //DayOfWeekEnum dayOfWeekEnum;
                //var dd=Enum.TryParse("Sunday",out dayOfWeekEnum);
                return dayOfWeek.ToString();
            }

            //public static DayOfWeekEnum GetDayOfWeekGetName(DayOfWeek dayOfWeek)
            //{
            //    DayOfWeekEnum dayOfWeekEnum;
            //    Enum.TryParse(dayOfWeek.ToString(), out dayOfWeekEnum);
            //    return dayOfWeekEnum;
            //}
            //public static string GetDayOfWeekGetName(int day)
            //{
            //    var culture = new System.Globalization.CultureInfo("en-US");
            //    var dayName = culture.DateTimeFormat.GetDayName((DayOfWeek)day);

            //    return dayName;
            //}


            public class FormatDate
            {
                public static string Date { get; set; } = "yyyy-MM-dd";
                public static string DateForSlash { get; set; } = "yyyy/MM/dd";
                public static string DateTime { get; set; } = "yyyy-MM-dd HH:mm";
            }

            public static int DiffDateToDays(DateTime startDate, DateTime endDate)
            {
                int count = 0;

                if (endDate > DateTime.MinValue && startDate > DateTime.MinValue)
                {

                    var date = endDate - startDate;

                    count = date.Days;
                }

                return count;
            }

            public static DateTime CurrentDateWithoutTime()
            {
                var curDate = DateTime.Now;

                return new DateTime(curDate.Year, curDate.Month, curDate.Day, 0, 0, 0);
            }

            public static int CalcAge(string date)
            {
                var year = DateTime.Parse(date).Year;
                DateTime curDate = Date.CurrentDateWithoutTime();

                int age = curDate.Year - year;

                return age;
            }
        }

        public class PageSize
        {
            public static int SettingPageSize = 200;
        }

        public static string ComputeHash(string input, HashAlgorithm algorithm)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes);
        }

        public static string ComputeHashSHA256(string input)
        {
            HashAlgorithm algorithm = new SHA256CryptoServiceProvider();
            var result=  ComputeHash(input,algorithm);
            return result.Replace("-","");
        }

        public static bool IsDateValid(string date)
        {
            DateTime result = new DateTime();
            if (DateTime.TryParse(date, out result))
            {
                return true;
            }
            return false;
        }

        //public static bool IsDateAndTimeValid(string date)
        //{
        //    DateTime result = new DateTime();
        //    if (DateTime.TryParse(date, out result) && result.TimeOfDay.TotalSeconds >= 0)
        //    {
        //        return true;
        //    }
        //    return false; DateTime.TryParseExact()
        //}

        public static bool IsEqualDateAndAgeValid(object patientDto)
        {
            var year = DateTime.Parse(patientDto.GetType().GetProperty("BirthDate").GetValue(patientDto).ToString()).Year;
            DateTime curDate = Date.CurrentDateWithoutTime();

            int resultAge = curDate.Year - year;

            if (resultAge == Convert.ToInt32(patientDto.GetType().GetProperty("Age").GetValue(patientDto).ToString()))
            {
                return true;
            }

            return false;
        }

     
        public static string CreateErrorMessage(string propertyName, Dictionary<string, string> lstError)
        {
            Dictionary<string, object> dicError = new Dictionary<string, object>();//.Add("sectionId", lstError.ToList()
            dicError.Add(propertyName, lstError);

            return JsonConvert.SerializeObject(dicError);
        }

        public class Language
        {

            public static string culcure
            {
                get
                {

                    return "";//Thread.CurrentThread.CurrentCulture.Name;
                }
            }
            public static string CurrentLanguage { get; set; } = Thread.CurrentThread.CurrentCulture.Name;
            public class Culcures
            {
                public const string Fa = "fa-IR";
                public const string En = "en-US";
                public const string Ar = "ar-IQ";
            }

            public static string GetTilteByLang(object obj, string lang)
            {
                string result = string.Empty;

                if (obj is null)
                    return result;

                //defaul lang
                if (string.IsNullOrEmpty(lang))
                    lang = Utilities.Language.Culcures.Ar;

                switch (lang)
                {
                    case Utilities.Language.Culcures.Ar:
                        result = obj.GetType()?.GetProperty("Title")?.GetValue(obj)?.ToString();
                        break;

                    case Utilities.Language.Culcures.En:
                        result = obj.GetType()?.GetProperty("TitleLang2")?.GetValue(obj)?.ToString();
                        break;
                }

                return result;
            }

            public static string GetNoteByLang(object obj, string lang)
            {
                string result = string.Empty;

                if (obj is null)
                    return result;

                //defaul lang
                if (string.IsNullOrEmpty(lang))
                    lang = Utilities.Language.Culcures.En;

                switch (lang)
                {
                    case Utilities.Language.Culcures.Ar:
                        result = obj.GetType()?.GetProperty("Note")?.GetValue(obj)?.ToString();
                        break;

                    case Utilities.Language.Culcures.En:
                        result = obj.GetType()?.GetProperty("NoteLang2")?.GetValue(obj)?.ToString();
                        break;
                }

                return result;
            }
        }

        public static string GetTypeOf(string value)
        {
            string result = "string";

            if (Boolean.TryParse(value, out var r))
            {
                result = "boolean";
            }

            if (Decimal.TryParse(value, out var rd) || int.TryParse(value, out var ri) || Int64.TryParse(value, out var ri6))
            {
                result = "number";
            }

            return result;
        }

        public static async Task  FuncAddDictionary(Dictionary<string, object> dic, string key,Func<Task<BaseResponseDto>> fun)
        {
            if (!dic.ContainsKey(key))
                dic.Add(key,(await fun.Invoke()).Data);
        }

        public static async Task FuncAddDictionary(Dictionary<string, object> dic, string key, Func<Task<ListResponseDto>> fun)
        {
            if (!dic.ContainsKey(key))
                dic.Add(key, (await fun.Invoke()).Data);
        }

        public static void AddDictionary(Dictionary<string, object> dic, string key, object obj)
        {
            if (!dic.ContainsKey(key))
                dic.Add(key, obj);
        }

        public static T DictionryCastToModel<T>(string key, Dictionary<string, object> dic)
        {
            object result = new object();

            if (dic.TryGetValue(key, out var model))
                result = model;

            return (T)result;
        }


        public static string GenerateVerifyCode()
        {
            return GenerateRandomNumber(5);
        }

        public static string GenerateRandomNumber(int to)
        {
            const string Letters = "123456789";
            Random rand = new Random();
            int maxRand = Letters.Length - 1;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < to; i++)
            {
                int index = rand.Next(maxRand);
                sb.Append(Letters[index]);
            }

            return sb.ToString();
        }

        //public class MessageResponse
        //{
        //    public static string UserNotFound( SharedLocalizer) {

        //        var dicError = new Dictionary<string, string>() {
        //            { "NotFound",_sharedLocalizer["GlobalForm.Response.UserNotFound"]}//+
        //        };

        //        var error = Utilities.CreateErrorMessage("UserNotFound", dicError);

        //        return error;
        //    }
        //} 


        public static BaseResponseDto CheckIsAdminForUpdateOrDelete(object obj, string form, IStringLocalizer sharedLocalizer, bool isDelete)
        {
            var id = obj.GetType().GetProperty("Id")?.GetValue(obj);

            if (isDelete && id != null && Convert.ToInt64(id) == 0)
            {
                var dicError = new Dictionary<string, string>()
                    {
                        { "NotDeleteOrUpdate",sharedLocalizer[$"{form}.Response.NotDeleteOrUpdateSystemId"]}
                    };

                var error = CreateErrorMessage("NotDeleteOrUpdate", dicError);

                return new BaseResponseDto
                {
                    Status = ResponseStatus.NotValid,
                    Errors = error
                };
            }

            List<string> entity = new List<string>();
            entity.Add("SectionStatus");

            //check is admin importan model
            if (entity.Any(d => d == obj.GetType().Name.ToString()))
            {
                var type = obj.GetType().GetProperty("IsAdmin")?.GetValue(obj);
                if (type != null)
                {
                    var isadmin = Boolean.Parse(type.ToString());
                    if (isadmin)
                    {
                        var dicError = new Dictionary<string, string>()
                    {
                        { "NotCahnge",sharedLocalizer[$"{form}.Response.NotCahngeIsAdmin"]}
                    };

                        var error = CreateErrorMessage("NotCahnge", dicError);


                        return new BaseResponseDto
                        {
                            Status = ResponseStatus.NotValid,
                            Errors = error
                        };
                    }
                }
            }

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success
            };
        }

        public static bool IsIdSystem(object obj)
        {
            var idValue = obj.GetType().GetProperty("Id")?.GetValue(obj);
            return idValue != null && Convertor.TryToLong(idValue) == 0;
        }

        public static bool RowIsAdmin(object obj)
        {
            var isAdminValue = obj.GetType().GetProperty("IsAdmin")?.GetValue(obj);

            return isAdminValue != null && Convertor.TryToBool(isAdminValue);
        }

        public static class Convertor
        {
            public static long TryToLong(object value)
            {
                long.TryParse(value.ToString(), out var longValue);
                return longValue;
            }

            public static bool TryToBool(object value)
            {
                bool.TryParse(value.ToString(), out var boolValue);
                return boolValue;
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
  
}
