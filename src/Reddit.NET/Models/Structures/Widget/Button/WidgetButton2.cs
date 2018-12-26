using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WidgetButton2 : WidgetButton
    {
        [JsonProperty("buttons")]
        public List<WidgetButton2Data> Buttons;
    }
}
