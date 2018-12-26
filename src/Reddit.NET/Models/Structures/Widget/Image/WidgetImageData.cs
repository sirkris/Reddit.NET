using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WidgetImageData
    {
        [JsonProperty("height")]
        public int Height;

        [JsonProperty("linkUrl")]
        public string LinkURL;

        [JsonProperty("url")]
        public string URL;

        [JsonProperty("width")]
        public int Width;
    }
}
