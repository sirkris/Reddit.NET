using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
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
