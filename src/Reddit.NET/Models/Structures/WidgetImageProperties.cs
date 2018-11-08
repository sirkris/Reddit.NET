using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetImageProperties : BaseContainer
    {
        [JsonProperty("height")]
        public int Height;

        [JsonProperty("imageUrl")]
        public string ImageURL;

        [JsonProperty("width")]
        public int Width;
    }
}
