using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetCustomImageData
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string URL { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}
