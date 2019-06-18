using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetStyles
    {
        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("headerColor")]
        public string HeaderColor { get; set; }

        public WidgetStyles(string backgroundColor = "#FFFFFF", string headerColor = "#0000FF")
        {
            BackgroundColor = backgroundColor;
            HeaderColor = headerColor;
        }
    }
}
