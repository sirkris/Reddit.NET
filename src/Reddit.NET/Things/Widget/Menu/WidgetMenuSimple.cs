using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetMenuSimple : BaseContainer
    {
        [JsonProperty("data")]
        public List<WidgetMenuData> Data { get; set; }
    }
}
