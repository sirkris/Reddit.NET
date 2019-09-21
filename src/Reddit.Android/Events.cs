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
                    OnInboxUpdated(e, sender);
                    break;
                case "sent":
                    OnSentUpdated(e, sender);
                    break;
                case "unread":
                    OnUnreadUpdated(e, sender);
                    break;
            }
        }

        public static void OnPostsUpdated(PostsUpdateEventArgs e, string type, object sender)
        {
            switch (type.Trim().ToLower())
            {
                case "best":
                    OnBestPostsUpdated(e, sender);
                    break;
                case "hot":
                    OnHotPostsUpdated(e, sender);
                    break;
                case "new":
                    OnNewPostsUpdated(e, sender);
                    break;
                case "rising":
                    OnRisingPostsUpdated(e, sender);
                    break;
                case "top":
                    OnTopPostsUpdated(e, sender);
                    break;
                case "controversial":
                    OnControversialPostsUpdated(e, sender);
                    break;
                case "modqueue":
                    OnModQueueUpdated(e, sender);
                    break;
                case "modqueuereports":
                    OnModQueueReportsUpdated(e, sender);
                    break;
                case "modqueuespam":
                    OnModQueueSpamUpdated(e, sender);
                    break;
                case "modqueueunmoderated":
                    OnModQueueUnmoderatedUpdated(e, sender);
                    break;
                case "modqueueedited":
                    OnModQueueEditedUpdated(e, sender);
                    break;
            }
        }

        public static void OnBestPostsUpdated(PostsUpdateEventArgs e, object sender)
        {
            BestPostsUpdated?.Invoke(sender, e);
        }

        public static void OnHotPostsUpdated(PostsUpdateEventArgs e, object sender)
        {
            HotPostsUpdated?.Invoke(sender, e);
        }

        public static void OnNewPostsUpdated(PostsUpdateEventArgs e, object sender)
        {
            NewPostsUpdated?.Invoke(sender, e);
        }

        public static void OnRisingPostsUpdated(PostsUpdateEventArgs e, object sender)
        {
            RisingPostsUpdated?.Invoke(sender, e);
        }

        public static void OnTopPostsUpdated(PostsUpdateEventArgs e, object sender)
        {
            TopPostsUpdated?.Invoke(sender, e);
        }

        public static void OnControversialPostsUpdated(PostsUpdateEventArgs e, object sender)
        {
            ControversialPostsUpdated?.Invoke(sender, e);
        }

        public static void OnModQueueUpdated(PostsUpdateEventArgs e, object sender)
        {
            ModQueueUpdated?.Invoke(sender, e);
        }

        public static void OnModQueueReportsUpdated(PostsUpdateEventArgs e, object sender)
        {
            ModQueueReportsUpdated?.Invoke(sender, e);
        }

        public static void OnModQueueSpamUpdated(PostsUpdateEventArgs e, object sender)
        {
            ModQueueSpamUpdated?.Invoke(sender, e);
        }

        public static void OnModQueueUnmoderatedUpdated(PostsUpdateEventArgs e, object sender)
        {
            ModQueueUnmoderatedUpdated?.Invoke(sender, e);
        }

        public static void OnModQueueEditedUpdated(PostsUpdateEventArgs e, object sender)
        {
            ModQueueEditedUpdated?.Invoke(sender, e);
        }

        public static void OnInboxUpdated(MessagesUpdateEventArgs e, object sender)
        {
            InboxUpdated?.Invoke(sender, e);
        }

        public static void OnUnreadUpdated(MessagesUpdateEventArgs e, object sender)
        {
            UnreadUpdated?.Invoke(sender, e);
        }

        public static void OnSentUpdated(MessagesUpdateEventArgs e, object sender)
        {
            SentUpdated?.Invoke(sender, e);
        }
    }
}
