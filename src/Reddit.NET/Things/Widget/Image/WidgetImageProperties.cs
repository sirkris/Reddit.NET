using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetImageProperties : BaseContainer
    {
        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageURL { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}
