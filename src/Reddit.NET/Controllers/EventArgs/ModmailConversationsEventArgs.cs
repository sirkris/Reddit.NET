using RedditThings = Reddit.NET.Models.Structures;
using System.Collections.Generic;

namespace Reddit.NET.Controllers.EventArgs
{
    public class ModmailConversationsEventArgs
    {
        public Dictionary<string, RedditThings.Conversation> OldConversations { get; set; }
        public Dictionary<string, RedditThings.Conversation> NewConversations { get; set; }
        public Dictionary<string, RedditThings.Conversation> AddedConversations { get; set; }
        public Dictionary<string, RedditThings.Conversation> RemovedConversations { get; set; }

        public Dictionary<string, RedditThings.ConversationMessage> OldMessages { get; set; }
        public Dictionary<string, RedditThings.ConversationMessage> NewMessages { get; set; }
        public Dictionary<string, RedditThings.ConversationMessage> AddedMessages { get; set; }
        public Dictionary<string, RedditThings.ConversationMessage> RemovedMessages { get; set; }
    }
}
