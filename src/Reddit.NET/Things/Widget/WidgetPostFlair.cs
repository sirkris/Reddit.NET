using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class WidgetPostFlair : BaseContainer
    {
        [JsonProperty("display")]
        public string Display;

        [JsonProperty("order")]
        public List<string> Order;  // List of flair template ids.  --Kris

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("styles")]
        public WidgetStyles Styles;
    }
}
