using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetImage : BaseContainer
    {
        [JsonProperty("data")]
        public List<WidgetImageData> Data;

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("styles")]
        public WidgetStyles Styles;
    }
}
