using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetButton2Data : BaseContainer
    {
        [JsonProperty("json")]
        public WidgetImageProperties JSON;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("textColor")]
        public string TextColor;

        [JsonProperty("url")]
        public string URL;
    }
}
