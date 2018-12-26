using Reddit.Things;
using System.Collections.Generic;

namespace Reddit.Controllers.EventArgs
{
    public class ModmailConversationsEventArgs
    {
        public Dictionary<string, Things.Conversation> OldConversations { get; set; }
        public Dictionary<string, Things.Conversation> NewConversations { get; set; }
        public Dictionary<string, Things.Conversation> AddedConversations { get; set; }
        public Dictionary<string, Things.Conversation> RemovedConversations { get; set; }

        public Dictionary<string, Things.ConversationMessage> OldMessages { get; set; }
        public Dictionary<string, Things.ConversationMessage> NewMessages { get; set; }
        public Dictionary<string, Things.ConversationMessage> AddedMessages { get; set; }
        public Dictionary<string, Things.ConversationMessage> RemovedMessages { get; set; }
    }
}
