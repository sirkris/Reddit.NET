using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetCustom
    {
        [JsonProperty("css")]
        public string CSS;

        [JsonProperty("height")]
        public int Height;

        [JsonProperty("imageData")]
        public List<WidgetCustomImageData> ImageData;

        [JsonProperty("kind")]
        public string Kind;

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("styles")]
        public WidgetStyles Styles;

        [JsonProperty("text")]
        public string Text;
    }
}
