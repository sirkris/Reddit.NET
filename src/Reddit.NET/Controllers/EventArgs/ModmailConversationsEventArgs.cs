using Reddit.Things;
using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class ModmailConversationsEventArgs
    {
        public Dictionary<string, Conversation> OldConversations { get; set; }
        public Dictionary<string, Conversation> NewConversations { get; set; }
        public Dictionary<string, Conversation> AddedConversations { get; set; }
        public Dictionary<string, Conversation> RemovedConversations { get; set; }

        public Dictionary<string, ConversationMessage> OldMessages { get; set; }
        public Dictionary<string, ConversationMessage> NewMessages { get; set; }
        public Dictionary<string, ConversationMessage> AddedMessages { get; set; }
        public Dictionary<string, ConversationMessage> RemovedMessages { get; set; }
    }
}
