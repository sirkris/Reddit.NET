using System;

namespace Reddit.Models.Converters
{
    class LocalTimestampConverter : TimestampConverterBase
    {
        public override long ConvertToSeconds(DateTime dateTime)
        {
            var dateTimeOffset = new DateTimeOffset(dateTime);
            return new DateTimeOffset(dateTime + dateTimeOffset.Offset).ToUnixTimeSeconds();
        }

        public override DateTime ParseDateFromSeconds(long seconds)
        {
            return DateTimeOffset.FromUnixTimeSeconds(seconds + (long)DateTimeOffset.Now.Offset.TotalSeconds).LocalDateTime;
        }
    }
}
