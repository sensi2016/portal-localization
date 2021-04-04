using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Portal.Infrastructure.Mapper
{
    public class JsonMapper
    {
        // json to model
        public static T Map<T>(string json)
        {
            //if(json is null || json==string.Empty)return  default;
            return JsonConvert.DeserializeObject<T>(json);
        }
        
        public static T Map<T>(object obj)
            => JsonConvert.DeserializeObject<T>(Map(obj));

        // object to json
        public static string Map(object obj)
            => JsonConvert.SerializeObject(obj);

        // object to json
        public static JObject ToJson(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            if (json == "[]") return new JObject();
            return JObject.Parse(json);
        }

    }

    public class AutoMapper
    {
        public static T MapTo<T>(object obj)
        {
            var stringObj = obj.ToString();
            if (stringObj == string.Empty) return default;

            var jsonObj=JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(jsonObj);
        }
    }
}
