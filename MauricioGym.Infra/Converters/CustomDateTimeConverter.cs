using Newtonsoft.Json;
using System.Globalization;

namespace MauricioGym.Infra.Converters
{
    public class CustomDateTimeConverter : JsonConverter<DateTime?>
    {
        private readonly string[] formats = new[]
        {
            "dd/MM/yyyy HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
            "dd/MM/yyyy",
            "yyyy-MM-dd"
        };

        public CustomDateTimeConverter() { }

        public override void WriteJson(JsonWriter writer, DateTime? value, JsonSerializer serializer)
        {
            if (value.HasValue)
                writer.WriteValue(value.Value.ToString(formats[0]));
            else
                writer.WriteNull();
        }

        public override DateTime? ReadJson(JsonReader reader, Type objectType, DateTime? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;

            var dateString = reader.Value.ToString();

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                    return date;
            }

            // Fallback para parse padrão
            if (DateTime.TryParse(dateString, out DateTime fallbackDate))
                return fallbackDate;

            return null;
        }
    }
}
