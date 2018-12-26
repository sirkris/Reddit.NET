using Newtonsoft.Json;
using System;

namespace Reddit.Things
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
