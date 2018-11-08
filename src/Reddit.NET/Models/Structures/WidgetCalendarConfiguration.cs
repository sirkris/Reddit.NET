using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetCalendarConfiguration
    {
        [JsonProperty("numEvents")]
        public int NumEvents;

        [JsonProperty("showDate")]
        public bool ShowDate;

        [JsonProperty("showDescription")]
        public bool ShowDescription;

        [JsonProperty("showLocation")]
        public bool ShowLocation;

        [JsonProperty("showTime")]
        public bool ShowTime;

        [JsonProperty("showTitle")]
        public bool ShowTitle;

        public WidgetCalendarConfiguration(int numEvents, bool showDate, bool showDescription, bool showLocation, bool showTime, bool showTitle)
        {
            NumEvents = numEvents;
            ShowDate = showDate;
            ShowDescription = showDescription;
            ShowLocation = showLocation;
            ShowTime = showTime;
            ShowTitle = showTitle;
        }

        public WidgetCalendarConfiguration() { }
    }
}
