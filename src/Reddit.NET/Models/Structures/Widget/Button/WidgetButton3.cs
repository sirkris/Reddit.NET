using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class WidgetButton3 : WidgetButton
    {
        [JsonProperty("buttons")]
        public List<WidgetButton3Data> Buttons;
    }
}
