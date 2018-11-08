using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class JQueryReturn
    {
        [JsonProperty("jquery")]
        public dynamic JQuery;

        [JsonProperty("success")]
        public bool Success;
    }
}
