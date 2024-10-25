using System.Reflection;
using System.Text.Json.Serialization;

namespace CNSMarketing.Service.Helpers
{
    public static class ApiHelper
    {
        public static Dictionary<string, string> ConvertObjectToDictionary(object request)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            TypeInfo typeInfo = request.GetType().GetTypeInfo();

            foreach (var prop in typeInfo.DeclaredProperties)
            {
                // JsonPropertyName niteliğini al
                var jsonPropertyAttribute = prop.GetCustomAttribute<JsonPropertyNameAttribute>();
                var key = jsonPropertyAttribute != null ? jsonPropertyAttribute.Name : prop.Name; // Eğer JsonPropertyName varsa kullan, yoksa normal adını kullan.

                // Değerini al ve boş olup olmadığını kontrol et
                var value = prop.GetValue(request)?.ToString();
                if (value != null)
                {
                    keyValuePairs.Add(key, value);
                }
            }
            return keyValuePairs;
        }
    }
}
