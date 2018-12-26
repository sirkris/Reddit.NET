using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
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
