using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class BadRequest
    {
        [JsonProperty("fields")]
        public object Fields;  // TODO - Determine type.  --Kris

        [JsonProperty("explanation")]
        public string Explanation;

        [JsonProperty("message")]
        public string Message;

        [JsonProperty("reason")]
        public string Reason;
    }
}
