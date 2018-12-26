using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class ActionResult
    {
        [JsonProperty("status")]
        public string Status;

        [JsonProperty("errors")]
        public object Errors;

        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("warnings")]
        public object Warnings;
    }
}
