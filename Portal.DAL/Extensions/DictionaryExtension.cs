using System;
using System.Collections.Generic;

namespace Portal.DAL.Extensions
{
    public static class DictionaryExtension
    {
        public static int? GetInt(this Dictionary<string, string> dic,string key,int? defult=null)
        {
            return dic.ContainsKey(key) ? dic[key].TryToInt() : defult;
        }

        public static string GetString(this Dictionary<string, string> dic, string key, string defult = null)
        {
            return dic.ContainsKey(key) ? dic[key] : defult;
        }

        public static DateTime? GetDateTime(this Dictionary<string, string> dic, string key, DateTime? defult = null)
        {
            return dic.ContainsKey(key) ? dic[key].TryToDateTime() : defult;
        }
    }
}
