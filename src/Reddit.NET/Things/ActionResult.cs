using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ActionResult
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("errors")]
        public object Errors { get; set; }

        [JsonProperty("ok")]
        public bool Ok { get; set; }

        [JsonProperty("warnings")]
        public object Warnings { get; set; }
    }
}
