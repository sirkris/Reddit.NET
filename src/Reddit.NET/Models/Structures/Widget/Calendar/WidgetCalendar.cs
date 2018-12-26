using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WidgetCalendar : BaseContainer
    {
        [JsonProperty("configuration")]
        public WidgetCalendarConfiguration Configuration;

        [JsonProperty("googleCalendarId")]
        public string GoogleCalendarId;

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
