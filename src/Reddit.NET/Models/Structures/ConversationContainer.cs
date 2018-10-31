using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ConversationContainer
    {
        [JsonProperty("conversations")]
        public Dictionary<string, Conversation> Conversations;

        [JsonProperty("messages")]
        public Dictionary<string, ConversationMessage> Messages;

        [JsonProperty("viewerId")]
        public string ViewerId;

        [JsonProperty("conversationIds")]
        public List<string> ConversationIds;
    }
}
