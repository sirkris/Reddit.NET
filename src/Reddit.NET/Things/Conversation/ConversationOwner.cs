using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ConversationOwner
    {
        [JsonProperty("displayName")]
        public string DisplayName;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public string Id;
    }
}
