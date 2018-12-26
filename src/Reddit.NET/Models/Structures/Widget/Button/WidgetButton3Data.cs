using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
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
