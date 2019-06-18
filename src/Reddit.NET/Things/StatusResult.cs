using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class StatusResult
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}
