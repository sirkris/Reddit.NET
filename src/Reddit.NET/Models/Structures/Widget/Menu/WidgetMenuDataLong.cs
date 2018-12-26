using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WidgetMenuDataLong
    {
        [JsonProperty("children")]
        public List<WidgetMenuData> Children;

        [JsonProperty("text")]
        public string Text;
    }
}
