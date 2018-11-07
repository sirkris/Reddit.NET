using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetModerators
    {
        [JsonProperty("kind")]
        public string Kind;

        [JsonProperty("styles")]
        public WidgetStyles Styles;
    }
}
