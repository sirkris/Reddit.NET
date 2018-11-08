using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetButton4Data : BaseContainer
    {
        [JsonProperty("json")]
        public WidgetImageProperties JSON;

        [JsonProperty("imageUrl")]
        public string ImageURL;

        [JsonProperty("linkUrl")]
        public string LinkURL;

        [JsonProperty("text")]
        public string Text;

        [JsonProperty("width")]
        public int Width;
    }
}
