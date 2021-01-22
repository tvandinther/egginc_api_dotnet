using System.Text.Json;

namespace HttpFunction
{
    public static class Extensions
    {
        public static string ToJSON(this object value)
        {
            return JsonSerializer.Serialize(value);
        }
    }
}