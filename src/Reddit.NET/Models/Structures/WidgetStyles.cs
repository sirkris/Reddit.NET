using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetStyles
    {
        [JsonProperty("backgroundColor")]
        public string BackgroundColor;

        [JsonProperty("headerColor")]
        public string HeaderColor;

        public WidgetStyles(string backgroundColor = "#FFFFFF", string headerColor = "#0000FF")
        {
            BackgroundColor = backgroundColor;
            HeaderColor = headerColor;
        }
    }
}
