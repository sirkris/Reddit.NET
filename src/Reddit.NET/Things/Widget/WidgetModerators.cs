using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetModerators : BaseContainer
    {
        [JsonProperty("styles")]
        public WidgetStyles Styles { get; set; }
    }
}
