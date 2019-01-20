using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetButton1Data
    {
        [JsonProperty("color")]
        public string Color;

        [JsonProperty("fillColor")]
        public string FillColor;

        [JsonProperty("hoverState")]
        public WidgetHoverState HoverState;
    }
}
