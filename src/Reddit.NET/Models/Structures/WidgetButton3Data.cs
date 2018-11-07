using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
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
