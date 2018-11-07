using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetImageProperties
    {
        [JsonProperty("height")]
        public int Height;

        [JsonProperty("imageUrl")]
        public string ImageURL;

        [JsonProperty("kind")]
        public string Kind;

        [JsonProperty("width")]
        public int Width;
    }
}
