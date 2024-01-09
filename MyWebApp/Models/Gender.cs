using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyWebApp.Models
{
    public enum Gender
    {
        Male,
        Female
    }

   /* public class GenderConverter : JsonConverter<Gender>
    {
        public override void Write(Utf8JsonWriter writer, Gender value, JsonSerializerOptions options)
            => writer.WriteStringValue(value.ToString());

        public override Gender Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
        {
            var str = reader.GetString();

            return string.IsNullOrEmpty(str) || !Gender.TryParse(objectType, str, out var result) ? default : (Gender)result!;

        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }*/
}

