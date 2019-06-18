using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class ModmailConversationContainer
    {
        [JsonProperty("conversation")]
        public Conversation Conversation { get; set; }

        [JsonProperty("conversations")]
        private Conversation Conversations
        {
            get
            {
                return Conversation;
            }
            set
            {
                Conversation = value;
            }
        }

        [JsonProperty("messages")]
        public Dictionary<string, ConversationMessage> Messages { get; set; }

        [JsonProperty("modActions")]
        public Dictionary<string, ModActionShort> ModActions { get; set; }

        [JsonProperty("user")]
        public object User { get; set; }  // TODO - Determine specific type.  --Kris
    }
}
