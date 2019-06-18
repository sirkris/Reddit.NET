using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetResults
    {
        [JsonProperty("items")]
        public Dictionary<string, dynamic> Items { get; set; }

        [JsonProperty("layout")]
        public WidgetLayout Layout { get; set; }
    }
}
