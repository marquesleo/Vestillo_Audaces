using System.Text.Json;
using TemplateAudacesApi.Models;

namespace TemplateAudacesApi.Utils
{
    public static class UtilService
    {
        public static string GetTokenWithouBearer(string tokenIn)
        {
            string tokenOut;
            tokenOut = tokenIn.Remove(0, 7); // "bearer ", size 7
            return tokenOut;
        }
        
        public static string? GetCustomFieldString(
            Dictionary<string, CustomFieldValue> fields, string chave)
        {
            return fields.TryGetValue(chave, out var campo) &&
                   campo.value.ValueKind == JsonValueKind.String
                ? campo.value.GetString()
                : null;
        }
    }
}
