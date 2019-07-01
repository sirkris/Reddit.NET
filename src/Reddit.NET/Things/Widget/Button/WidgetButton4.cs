using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetButton4 : WidgetButton
    {
        [JsonProperty("buttons")]
        public List<WidgetButton4Data> Buttons { get; set; }
    }
}
