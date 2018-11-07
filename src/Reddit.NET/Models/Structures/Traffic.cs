using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class Traffic
    {
        [JsonProperty("day")]
        public List<List<int>> Day;

        [JsonProperty("hour")]
        public List<List<int>> Hour;

        [JsonProperty("month")]
        public List<List<int>> Month;
    }
}
