using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetModerators : BaseContainer
    {
        [JsonProperty("styles")]
        public WidgetStyles Styles;
    }
}
