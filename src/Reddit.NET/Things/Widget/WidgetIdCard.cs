using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetIdCard : BaseContainer
    {
        [JsonProperty("currentlyViewingText")]
        public string CurrentlyViewingText { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("styles")]
        public WidgetStyles Styles { get; set; }

        [JsonProperty("subscribersText")]
        public string SubscribersText { get; set; }
    }
}
