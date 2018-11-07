using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetMenuSimple
    {
        [JsonProperty("data")]
        public List<WidgetMenuData> Data;

        [JsonProperty("kind")]
        public string Kind;
    }
}
