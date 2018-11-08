using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetButton1 : WidgetButton
    {
        [JsonProperty("buttons")]
        public List<WidgetButton1Data> Buttons;
    }
}
