using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetCalendar : BaseContainer
    {
        [JsonProperty("configuration")]
        public WidgetCalendarConfiguration Configuration { get; set; }

        [JsonProperty("googleCalendarId")]
        public string GoogleCalendarId { get; set; }

        [JsonProperty("requiresSync")]
        public bool RequiresSync { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("styles")]
        public WidgetStyles Styles { get; set; }

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
