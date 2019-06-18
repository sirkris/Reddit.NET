using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetLayoutOrder
    {
        [JsonProperty("order")]
        public List<string> Order { get; set; }
    }
}
