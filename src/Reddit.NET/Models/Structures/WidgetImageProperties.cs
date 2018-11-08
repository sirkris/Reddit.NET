using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
