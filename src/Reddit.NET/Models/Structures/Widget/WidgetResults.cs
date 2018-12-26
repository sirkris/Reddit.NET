using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WidgetResults
    {
        [JsonProperty("items")]
        public Dictionary<string, dynamic> Items;

        [JsonProperty("layout")]
        public WidgetLayout Layout;
    }
}
