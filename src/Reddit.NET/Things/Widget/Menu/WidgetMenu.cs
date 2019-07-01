using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetMenu : BaseContainer
    {
        [JsonProperty("data")]
        public List<WidgetMenuDataLong> Data { get; set; }
    }
}
