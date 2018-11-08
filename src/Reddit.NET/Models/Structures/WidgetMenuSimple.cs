using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetMenuSimple : BaseContainer
    {
        [JsonProperty("data")]
        public List<WidgetMenuData> Data;
    }
}
