using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class ConversationContainer
    {
        [JsonProperty("conversations")]
        public Dictionary<string, Conversation> Conversations { get; set; }

        [JsonProperty("messages")]
        public Dictionary<string, ConversationMessage> Messages { get; set; }

        [JsonProperty("viewerId")]
        public string ViewerId { get; set; }

        [JsonProperty("conversationIds")]
        public List<string> ConversationIds { get; set; }
    }
}
