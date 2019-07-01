using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WikiPageSettings
    {
        [JsonProperty("permlevel")]
        public int PermLevel { get; set; }

        [JsonProperty("editors")]
        public object Editors { get; set; }

        [JsonProperty("listed")]
        public bool Listed { get; set; }
    }
}
