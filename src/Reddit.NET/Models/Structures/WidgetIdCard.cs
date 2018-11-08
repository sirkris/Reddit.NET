using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetIdCard : BaseContainer
    {
        [JsonProperty("currentlyViewingText")]
        public string CurrentlyViewingText;

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("styles")]
        public WidgetStyles Styles;

        [JsonProperty("subscribersText")]
        public string SubscribersText;
    }
}
