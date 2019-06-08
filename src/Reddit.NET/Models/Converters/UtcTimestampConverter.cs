using System;

namespace Reddit.Models.Converters
{
    class UtcTimestampConverter : TimestampConverterBase
    {
        public override long ConvertToSeconds(DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeSeconds();
        }

        public override DateTime ParseDateFromSeconds(long seconds)
        {
            return DateTimeOffset.FromUnixTimeSeconds(seconds).UtcDateTime;
        }
    }
}
