using Newtonsoft.Json;
using System;

namespace Reddit.Things
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
