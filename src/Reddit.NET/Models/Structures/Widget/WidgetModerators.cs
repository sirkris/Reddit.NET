using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WidgetModerators : BaseContainer
    {
        [JsonProperty("styles")]
        public WidgetStyles Styles;
    }
}
