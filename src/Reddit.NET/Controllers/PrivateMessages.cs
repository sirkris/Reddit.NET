using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Controllers.Structures;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Reddit.NET.Models.Internal;

namespace Reddit.NET.Controllers
{
    public class PrivateMessages : BaseController
    {
        public event EventHandler<MessagesUpdateEventArgs> InboxUpdated;
        public event EventHandler<MessagesUpdateEventArgs> UnreadUpdated;
        public event EventHandler<MessagesUpdateEventArgs> SentUpdated;

        internal override ref Models.Internal.Monitor MonitorModel => ref Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;

        public List<RedditThings.Message> Inbox
        {
            get
            {
                return (InboxLastUpdated.HasValue
                    && InboxLastUpdated.Value.AddSeconds(15) > DateTime.Now ? inbox : GetMessagesInbox(false));
            }
            private set
            {
                inbox = value;
            }
        }
        internal List<RedditThings.Message> inbox;

        public List<RedditThings.Message> Unread
        {
            get
            {
                return (UnreadLastUpdated.HasValue
                    && UnreadLastUpdated.Value.AddSeconds(15) > DateTime.Now ? unread : GetMessagesUnread(false));
            }
            private set
            {
                unread = value;
            }
        }
        internal List<RedditThings.Message> unread;

        public List<RedditThings.Message> Sent
        {
            get
            {
                return (SentLastUpdated.HasValue
                    && SentLastUpdated.Value.AddSeconds(15) > DateTime.Now ? sent : GetMessagesSent(false));
            }
            private set
            {
                sent = value;
            }
        }
        internal List<RedditThings.Message> sent;

        private DateTime? InboxLastUpdated;
        private DateTime? UnreadLastUpdated;
        private DateTime? SentLastUpdated;

        private readonly Dispatch Dispatch;

        public PrivateMessages(Dispatch dispatch, List<RedditThings.Message> inbox = null, List<RedditThings.Message> unread = null,
            List<RedditThings.Message> sent = null) 
            : base()
        {
            Inbox = inbox ?? new List<RedditThings.Message>();
            Unread = unread ?? new List<RedditThings.Message>();
            Sent = sent ?? new List<RedditThings.Message>();

            Threads = new Dictionary<string, Thread>();

            Dispatch = dispatch;
        }

        /// <summary>
        /// Retrieve private messages for the current user.
        /// </summary>
        /// <param name="where">One of (inbox, unread, sent)</param>
        /// <param name="mark">one of (true, false)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="mid"></param>
        /// <returns>A list of messages.</returns>
        public List<RedditThings.Message> GetMessages(string where, bool mark = true, int limit = 25, string after = "", string before = "",
            string show = "all", bool srDetail = false, bool includeCategories = false, int count = 0, string mid = "")
        {
            RedditThings.MessageContainer messageContainer = Dispatch.PrivateMessages.GetMessages(where, mark, mid, after, before, includeCategories,
                count, limit, show, srDetail);

            List<RedditThings.Message> res = new List<RedditThings.Message>();
            if (messageContainer != null && messageContainer.Data != null && messageContainer.Data.Children != null)
            {
                foreach (RedditThings.MessageChild messageChild in messageContainer.Data.Children)
                {
                    res.Add(messageChild.Data);
                }
            }

            switch (where)
            {
                case "inbox":
                    InboxLastUpdated = DateTime.Now;
                    Inbox = res;
                    break;
                case "unread":
                    UnreadLastUpdated = DateTime.Now;
                    Unread = res;
                    break;
                case "sent":
                    SentLastUpdated = DateTime.Now;
                    Sent = res;
                    break;
            }

            return res;
        }

        /// <summary>
        /// Retrieve private inbox messages for the current user.
        /// </summary>
        /// <param name="mark">one of (true, false)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="mid"></param>
        /// <returns>A list of messages.</returns>
        public List<RedditThings.Message> GetMessagesInbox(bool mark = true, int limit = 25, string after = "", string before = "",
            string show = "all", bool srDetail = false, bool includeCategories = false, int count = 0, string mid = "")
        {
            return GetMessages("inbox", mark, limit, after, before, show, srDetail, includeCategories, count, mid);
        }

        /// <summary>
        /// Retrieve private unread messages for the current user.
        /// </summary>
        /// <param name="mark">one of (true, false)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="mid"></param>
        /// <returns>A list of messages.</returns>
        public List<RedditThings.Message> GetMessagesUnread(bool mark = true, int limit = 25, string after = "", string before = "",
            string show = "all", bool srDetail = false, bool includeCategories = false, int count = 0, string mid = "")
        {
            return GetMessages("unread", mark, limit, after, before, show, srDetail, includeCategories, count, mid);
        }

        /// <summary>
        /// Retrieve private sent messages for the current user.
        /// </summary>
        /// <param name="mark">one of (true, false)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="mid"></param>
        /// <returns>A list of messages.</returns>
        public List<RedditThings.Message> GetMessagesSent(bool mark = true, int limit = 25, string after = "", string before = "",
            string show = "all", bool srDetail = false, bool includeCategories = false, int count = 0, string mid = "")
        {
            return GetMessages("sent", mark, limit, after, before, show, srDetail, includeCategories, count, mid);
        }

        /// <summary>
        /// Queue up marking all messages for a user as read.
        /// This may take some time, and returns 202 to acknowledge acceptance of the request.
        /// </summary>
        /// <param name="filterTypes">A comma-separated list of items</param>
        public void MarkAllRead(string filterTypes = "")
        {
            Dispatch.PrivateMessages.ReadAllMessages(filterTypes);
        }

        /// <summary>
        /// Asynchronously queue up marking all messages for a user as read.
        /// This may take some time, and returns 202 to acknowledge acceptance of the request.
        /// </summary>
        /// <param name="filterTypes">A comma-separated list of items</param>
        public async Task MarkAllReadAsync(string filterTypes = "")
        {
            await Task.Run(() =>
            {
                MarkAllRead(filterTypes);
            });
        }

        /// <summary>
        /// Send a private message.
        /// </summary>
        /// <param name="to">the name of an existing user</param>
        /// <param name="subject">a string no longer than 100 characters</param>
        /// <param name="text">raw markdown text</param>
        /// <param name="fromSr">subreddit name</param>
        /// <param name="gRecaptchaResponse"></param>
        public void Compose(string to, string subject, string text, string fromSr = "", string gRecaptchaResponse = "")
        {
            Validate(Dispatch.PrivateMessages.Compose(fromSr, gRecaptchaResponse, subject, text, to));
        }

        /// <summary>
        /// Send a private message asynchronously.
        /// </summary>
        /// <param name="to">the name of an existing user</param>
        /// <param name="subject">a string no longer than 100 characters</param>
        /// <param name="text">raw markdown text</param>
        /// <param name="fromSr">subreddit name</param>
        /// <param name="gRecaptchaResponse"></param>
        public async Task ComposeAsync(string to, string subject, string text, string fromSr = "", string gRecaptchaResponse = "")
        {
            await Task.Run(() =>
            {
                Compose(to, subject, text, fromSr, gRecaptchaResponse);
            });
        }

        internal virtual void OnInboxUpdated(MessagesUpdateEventArgs e)
        {
            InboxUpdated?.Invoke(this, e);
        }

        internal virtual void OnUnreadUpdated(MessagesUpdateEventArgs e)
        {
            UnreadUpdated?.Invoke(this, e);
        }

        internal virtual void OnSentUpdated(MessagesUpdateEventArgs e)
        {
            SentUpdated?.Invoke(this, e);
        }

        public bool MonitorInbox()
        {
            string key = "PrivateMessagesInbox";
            return Monitor(key, new Thread(() => MonitorInboxThread(key)));
        }

        private void MonitorInboxThread(string key)
        {
            MonitorPrivateMessagesThread(key, "inbox");
        }

        public bool MonitorUnread()
        {
            string key = "PrivateMessagesUnread";
            return Monitor(key, new Thread(() => MonitorUnreadThread(key)));
        }

        private void MonitorUnreadThread(string key)
        {
            MonitorPrivateMessagesThread(key, "unread");
        }

        public bool MonitorSent()
        {
            string key = "PrivateMessagesSent";
            return Monitor(key, new Thread(() => MonitorSentThread(key)));
        }

        private void MonitorSentThread(string key)
        {
            MonitorPrivateMessagesThread(key, "sent");
        }

        internal void MonitorPrivateMessagesThread(string key, string type, int startDelayMs = 0)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            while (!Terminate
                && Monitoring.Get(key).Contains("PrivateMessages"))
            {
                List<RedditThings.Message> oldList;
                List<RedditThings.Message> newList;
                switch (type)
                {
                    default:
                        throw new RedditControllerException("Unrecognized type '" + type + "'.");
                    case "inbox":
                        oldList = inbox;
                        newList = GetMessagesInbox();
                        break;
                    case "unread":
                        oldList = unread;
                        newList = GetMessagesUnread();
                        break;
                    case "sent":
                        oldList = sent;
                        newList = GetMessagesSent();
                        break;
                }

                if (ListDiff(oldList, newList, out List<RedditThings.Message> added, out List<RedditThings.Message> removed))
                {
                    // Event handler to alert the calling app that the list has changed.  --Kris
                    MessagesUpdateEventArgs args = new MessagesUpdateEventArgs
                    {
                        NewMessages = newList,
                        OldMessages = oldList,
                        Added = added,
                        Removed = removed
                    };
                    TriggerUpdate(args, type);
                }

                Thread.Sleep(Monitoring.Count() * MonitoringWaitDelayMS);
            }
        }

        protected void TriggerUpdate(MessagesUpdateEventArgs args, string type)
        {
            switch (type)
            {
                case "inbox":
                    OnInboxUpdated(args);
                    break;
                case "unread":
                    OnUnreadUpdated(args);
                    break;
                case "sent":
                    OnSentUpdated(args);
                    break;
            }
        }

        private Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "PrivateMessagesInbox":
                    return new Thread(() => MonitorPrivateMessagesThread(key, "inbox", startDelayMs));
                case "PrivateMessagesUnread":
                    return new Thread(() => MonitorPrivateMessagesThread(key, "unread", startDelayMs));
                case "PrivateMessagesSent":
                    return new Thread(() => MonitorPrivateMessagesThread(key, "sent", startDelayMs));
            }
        }

        internal void RebuildThreads()
        {
            Dictionary<string, Thread> oldThreads = Threads;
            KillThreads(oldThreads);

            int i = 0;
            foreach (KeyValuePair<string, Thread> pair in oldThreads)
            {
                Threads.Add(pair.Key, CreateMonitoringThread(pair.Key, "PrivateMessages", (i * MonitoringWaitDelayMS)));
                i++;
            }
        }

        private bool Monitor(string key, Thread thread)
        {
            bool res = Monitor(key, thread, "PrivateMessages", out Thread newThread);

            RebuildThreads();
            LaunchThreadIfNotNull(key, newThread);

            return res;
        }
    }
}
