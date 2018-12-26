using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
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
