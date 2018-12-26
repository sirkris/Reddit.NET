using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
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
