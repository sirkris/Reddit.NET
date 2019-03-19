using Reddit;
using Reddit.Controllers.EventArgs;
using Reddit.Things;
using System.Collections.Generic;

namespace MonitorModmail
{
    class Program
    {
        public static List<ConversationMessage> NewMessages;

        static void Main(string[] args)
        {
            var reddit = new RedditAPI("YourRedditAppID", "YourBotUserRefreshToken");

            NewMessages = new List<ConversationMessage>();

            // Start monitoring unread modmail messages and register the callback function.  --Kris
            reddit.Account.Modmail.MonitorUnread();
            reddit.Account.Modmail.UnreadUpdated += C_UnreadMessagesUpdated;

            while (true) { } // Replace this with whatever you've got for a program loop.  The monitoring will run asynchronously in a separate thread.  --Kris

            // Stop monitoring and unregister the callback function.  --Kris
            reddit.Account.Modmail.MonitorUnread();
            reddit.Account.Modmail.UnreadUpdated -= C_UnreadMessagesUpdated;
        }

        private static void C_UnreadMessagesUpdated(object sender, ModmailConversationsEventArgs e)
        {
            foreach (KeyValuePair<string, ConversationMessage> pair in e.AddedMessages)
            {
                NewMessages.Add(pair.Value);
            }
        }
    }
}