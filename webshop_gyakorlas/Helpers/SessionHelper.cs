using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webshop_gyakorlas.Models;

namespace webshop_gyakorlas.Helpers
{
    public static class SessionHelper
    {
        public static void ObjectToJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T ObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return (value == null) ? default : JsonConvert.DeserializeObject<T>(value);
        }

        //TODO: Altalanosabb helper osztalyba atrakni
        public static T WatchesFromOrder<T>(Order order)
        {
            return JsonConvert.DeserializeObject<T>(order.WatchesJson);
        }

        public static void TruncateSession(this ISession session)
        {
            session.Clear();
        }
    }
}
