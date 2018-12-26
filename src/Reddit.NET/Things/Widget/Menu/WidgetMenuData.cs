using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetMenuData
    {
        [JsonProperty("text")]
        public string Text;

        [JsonProperty("url")]
        public string URL;
    }
}
