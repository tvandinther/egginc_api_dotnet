using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HttpFunction
{
    public static class Extensions
    {
        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
    }
}