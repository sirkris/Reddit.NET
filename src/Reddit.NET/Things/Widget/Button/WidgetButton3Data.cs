using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetButton3Data
    {
        [JsonProperty("height")]
        public int Height;

        [JsonProperty("hoverState")]
        public WidgetHoverState HoverState;
    }
}
