using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetButton2Data
    {
        [JsonProperty("json")]
        public WidgetImageProperties JSON;

        [JsonProperty("kind")]
        public string Kind;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("textColor")]
        public string TextColor;

        [JsonProperty("url")]
        public string URL;
    }
}
