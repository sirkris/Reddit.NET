using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class WikiPageSettings
    {
        [JsonProperty("permlevel")]
        public int PermLevel;

        [JsonProperty("editors")]
        public object Editors;

        [JsonProperty("listed")]
        public bool Listed;
    }
}
