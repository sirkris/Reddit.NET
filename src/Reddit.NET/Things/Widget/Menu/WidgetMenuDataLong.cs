using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetMenuDataLong
    {
        [JsonProperty("children")]
        public List<WidgetMenuData> Children { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
