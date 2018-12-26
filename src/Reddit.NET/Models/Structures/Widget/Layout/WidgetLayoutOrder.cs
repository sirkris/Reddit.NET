using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WidgetLayoutOrder
    {
        [JsonProperty("order")]
        public List<string> Order;
    }
}
