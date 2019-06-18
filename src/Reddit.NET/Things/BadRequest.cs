using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class BadRequest
    {
        [JsonProperty("fields")]
        public object Fields { get; set; }  // TODO - Determine type.  --Kris

        [JsonProperty("explanation")]
        public string Explanation { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }
    }
}
