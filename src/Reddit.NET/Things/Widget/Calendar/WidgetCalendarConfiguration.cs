using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetCalendarConfiguration
    {
        [JsonProperty("numEvents")]
        public int NumEvents { get; set; }

        [JsonProperty("showDate")]
        public bool ShowDate { get; set; }

        [JsonProperty("showDescription")]
        public bool ShowDescription { get; set; }

        [JsonProperty("showLocation")]
        public bool ShowLocation { get; set; }

        [JsonProperty("showTime")]
        public bool ShowTime { get; set; }

        [JsonProperty("showTitle")]
        public bool ShowTitle { get; set; }

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
