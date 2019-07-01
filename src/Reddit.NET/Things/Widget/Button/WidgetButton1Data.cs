using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetButton1Data
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("fillColor")]
        public string FillColor { get; set; }

        [JsonProperty("hoverState")]
        public WidgetHoverState HoverState { get; set; }
    }
}
