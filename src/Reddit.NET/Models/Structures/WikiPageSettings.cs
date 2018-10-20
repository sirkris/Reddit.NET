using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
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
