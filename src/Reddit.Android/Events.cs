using Reddit.Controllers.EventArgs;
using System;

namespace Reddit.Android
{
    public static class Events
    {
        public static event EventHandler<MessagesUpdateEventArgs> InboxUpdated;
        public static event EventHandler<MessagesUpdateEventArgs> UnreadUpdated;
        public static event EventHandler<MessagesUpdateEventArgs> SentUpdated;

        public static event EventHandler<PostsUpdateEventArgs> BestPostsUpdated;
        public static event EventHandler<PostsUpdateEventArgs> HotPostsUpdated;
        public static event EventHandler<PostsUpdateEventArgs> NewPostsUpdated;
        public static event EventHandler<PostsUpdateEventArgs> RisingPostsUpdated;
        public static event EventHandler<PostsUpdateEventArgs> TopPostsUpdated;
        public static event EventHandler<PostsUpdateEventArgs> ControversialPostsUpdated;

        public static event EventHandler<PostsUpdateEventArgs> ModQueueUpdated;
        public static event EventHandler<PostsUpdateEventArgs> ModQueueReportsUpdated;
        public static event EventHandler<PostsUpdateEventArgs> ModQueueSpamUpdated;
        public static event EventHandler<PostsUpdateEventArgs> ModQueueUnmoderatedUpdated;
        public static event EventHandler<PostsUpdateEventArgs> ModQueueEditedUpdated;

        public static void OnMessagesUpdated(MessagesUpdateEventArgs e, string type, object sender)
        {
            switch (type.Trim().ToLower())
            {
                case "inbox":
                    InboxUpdated?.Invoke(sender, e);
                    break;
                case "sent":
                    SentUpdated?.Invoke(sender, e);
                    break;
                case "unread":
                    UnreadUpdated?.Invoke(sender, e);
                    break;
            }
        }

        public static void OnPostsUpdated(PostsUpdateEventArgs e, string type, object sender)
        {
            switch (type.Trim().ToLower())
            {
                case "best":
                    BestPostsUpdated?.Invoke(sender, e);
                    break;
                case "hot":
                    HotPostsUpdated?.Invoke(sender, e);
                    break;
                case "new":
                    NewPostsUpdated?.Invoke(sender, e);
                    break;
                case "rising":
                    RisingPostsUpdated?.Invoke(sender, e);
                    break;
                case "top":
                    TopPostsUpdated?.Invoke(sender, e);
                    break;
                case "controversial":
                    ControversialPostsUpdated?.Invoke(sender, e);
                    break;
                case "modqueue":
                    ModQueueUpdated?.Invoke(sender, e);
                    break;
                case "modqueuereports":
                    ModQueueReportsUpdated?.Invoke(sender, e);
                    break;
                case "modqueuespam":
                    ModQueueSpamUpdated?.Invoke(sender, e);
                    break;
                case "modqueueunmoderated":
                    ModQueueUnmoderatedUpdated?.Invoke(sender, e);
                    break;
                case "modqueueedited":
                    ModQueueEditedUpdated?.Invoke(sender, e);
                    break;
            }
        }
    }
}
