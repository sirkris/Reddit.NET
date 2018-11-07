using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class WidgetPostFlair
    {
        [JsonProperty("display")]
        public string Display;

        [JsonProperty("kind")]
        public string Kind;

        [JsonProperty("order")]
        public List<string> Order;  // List of flair template ids.  --Kris

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("styles")]
        public WidgetStyles Styles;
    }
}
