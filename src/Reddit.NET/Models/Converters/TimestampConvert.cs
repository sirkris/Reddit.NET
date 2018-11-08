using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Reddit.NET.Models.Converters
{
    class TimestampConvert : DateTimeConverterBase
    {
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null && (DateTime)value != default(DateTime))
            {
                writer.WriteRawValue(((DateTime)value - Epoch).TotalSeconds.ToString());
            }
            else
            {
                writer.WriteNull();
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && DateTime.TryParse(reader.Value.ToString(), out DateTime dump2))
            {
                return (DateTime)reader.Value;
            }
            else
            {
                return (reader.Value != null
                    && !bool.TryParse(reader.Value.ToString(), out bool dump) // Edited returns long timestamp of last edit or bool false if there are no edits.  --Kris
                    && reader.Value.ToString().Length > 0 ? Epoch.AddSeconds(Convert.ToDouble(reader.Value)) : default(DateTime));
            }
        }
    }
}
