using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
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
