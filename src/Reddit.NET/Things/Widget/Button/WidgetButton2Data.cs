using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetButton2Data : BaseContainer
    {
        [JsonProperty("json")]
        public WidgetImageProperties JSON { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("textColor")]
        public string TextColor { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }
    }
}
