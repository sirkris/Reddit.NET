using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetButton4Data : BaseContainer
    {
        [JsonProperty("json")]
        public WidgetImageProperties JSON { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageURL { get; set; }

        [JsonProperty("linkUrl")]
        public string LinkURL { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }
    }
}
