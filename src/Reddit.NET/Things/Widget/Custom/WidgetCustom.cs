using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetCustom : BaseContainer
    {
        [JsonProperty("css")]
        public string CSS { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("imageData")]
        public List<WidgetCustomImageData> ImageData { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("styles")]
        public WidgetStyles Styles { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
