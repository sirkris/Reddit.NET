using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class ModmailConversationContainer
    {
        [JsonProperty("conversation")]
        public Conversation Conversation;

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
        public Dictionary<string, ConversationMessage> Messages;

        [JsonProperty("modActions")]
        public Dictionary<string, ModActionShort> ModActions;

        [JsonProperty("user")]
        public object User;  // TODO - Determine specific type.  --Kris
    }
}
