using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetCalendar
    {
        [JsonProperty("configuration")]
        public WidgetCalendarConfiguration Configuration;

        [JsonProperty("googleCalendarId")]
        public string GoogleCalendarId;

        [JsonProperty("kind")]
        public string Kind;

        [JsonProperty("requiresSync")]
        public bool RequiresSync;

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("styles")]
        public WidgetStyles Styles;

        public WidgetCalendar(WidgetCalendarConfiguration configuration, string googleCalendarId, bool requiresSync, string shortName, WidgetStyles styles)
        {
            Configuration = configuration;
            GoogleCalendarId = googleCalendarId;
            RequiresSync = requiresSync;
            ShortName = shortName;
            Styles = styles;
            Kind = "calendar";
        }

        public WidgetCalendar() { }
    }
}
