using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Reddit.Models.Converters
{
    abstract class TimestampConverterBase : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null && value is DateTime date && date != default(DateTime))
            {
                writer.WriteRawValue(ConvertToSeconds(date).ToString());
            }
            else
            {
                writer.WriteNull();
            }
        }

        public abstract long ConvertToSeconds(DateTime dateTime);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                var valueString = reader.Value.ToString();
                if (valueString.Length > 0 &&
                    // Edited returns long timestamp of last edit or bool false if there are no edits.  --Kris
                    !bool.TryParse(valueString, out bool parsedBool))
                {
                    if (DateTime.TryParse(valueString, out DateTime parsedDate))
                    {
                        return parsedDate;
                    }

                    return ParseDateFromSeconds((long)Convert.ToDouble(valueString));
                }
            }

            return default(DateTime);
        }

        public abstract DateTime ParseDateFromSeconds(long seconds);
    }
}
