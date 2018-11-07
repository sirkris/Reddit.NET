using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetMenuData
    {
        [JsonProperty("text")]
        public string Text;

        [JsonProperty("url")]
        public string URL;
    }
}
