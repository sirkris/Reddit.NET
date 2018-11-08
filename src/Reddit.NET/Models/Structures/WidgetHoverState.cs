using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetHoverState : BaseContainer
    {
        [JsonProperty("color")]
        public string Color;

        [JsonProperty("fillColor")]
        public string FillColor;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("textColor")]
        public string TextColor;
    }
}
