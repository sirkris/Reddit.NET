using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class ConversationOwner
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
