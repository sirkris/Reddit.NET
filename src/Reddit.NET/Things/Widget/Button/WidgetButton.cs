using Newtonsoft.Json;
using System;

namespace Reddit.Things
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
