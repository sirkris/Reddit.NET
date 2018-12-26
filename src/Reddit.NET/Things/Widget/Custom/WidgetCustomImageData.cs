using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetCustomImageData
    {
        [JsonProperty("height")]
        public int Height;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("url")]
        public string URL;

        [JsonProperty("width")]
        public int Width;
    }
}
