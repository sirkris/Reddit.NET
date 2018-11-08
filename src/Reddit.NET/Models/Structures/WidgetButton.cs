using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public abstract class WidgetButton : BaseContainer
    {
        [JsonProperty("description")]
        public string Description;

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("styles")]
        public WidgetStyles Styles;
    }
}
