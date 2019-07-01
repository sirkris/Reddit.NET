using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetButton2 : WidgetButton
    {
        [JsonProperty("buttons")]
        public List<WidgetButton2Data> Buttons { get; set; }
    }
}
