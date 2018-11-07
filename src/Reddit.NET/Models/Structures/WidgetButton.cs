using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public abstract class WidgetButton
    {
        [JsonProperty("description")]
        public string Description;

        [JsonProperty("kind")]
        public string Kind;

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("styles")]
        public WidgetStyles Styles;
    }
}
