using Newtonsoft.Json;
using System;

namespace Reddit.Things
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
