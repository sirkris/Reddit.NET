using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Reddit.NET.Controllers
{
    public class PrivateMessages : BaseController
    {
        public event EventHandler<MessagesUpdateEventArgs> InboxUpdated;
        public event EventHandler<MessagesUpdateEventArgs> UnreadUpdated;
        public event EventHandler<MessagesUpdateEventArgs> SentUpdated;

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
        private List<RedditThings.Message> inbox;

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
        private List<RedditThings.Message> unread;

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
        private List<RedditThings.Message> sent;

        private DateTime? InboxLastUpdated;
        private DateTime? UnreadLastUpdated;
        private DateTime? SentLastUpdated;

        private Dictionary<string, Thread> Threads;

        private readonly Dispatch Dispatch;

        public PrivateMessages(Dispatch dispatch, List<RedditThings.Message> inbox = null, List<RedditThings.Message> unread = null,
            List<RedditThings.Message> sent = null)
        {
            Inbox = inbox ?? new List<RedditThings.Message>();
            Unread = unread ?? new List<RedditThings.Message>();
            Sent = sent ?? new List<RedditThings.Message>();

            Threads = new Dictionary<string, Thread>();

            Dispatch = dispatch;

            MonitoringUpdated += C_MonitoringUpdated;
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

        private bool Monitor(string key, Thread thread)
        {
            if (Monitoring.ContainsKey(key)
                && Monitoring[key].Contains("PrivateMessages"))
            {
                // Stop monitoring.  --Kris
                RemoveMonitoringKey(key, "PrivateMessages", ref Monitoring);
                WaitOrDie(Threads[key]);

                return false;
            }
            else
            {
                // Start monitoring.  --Kris
                AddMonitoringKey(key, "PrivateMessages", ref Monitoring);

                Threads.Add(key, thread);
                Threads[key].Start();
                while (!Threads[key].IsAlive) { }

                return true;
            }
        }

        private void MonitorThread(string key, string type)
        {
            while (Monitoring.ContainsKey(key)
                && Monitoring[key].Contains("PrivateMessages"))
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

                Thread.Sleep(MonitoringCount() * MonitoringWaitDelayMS);
            }
        }

        protected virtual void OnInboxUpdated(MessagesUpdateEventArgs e)
        {
            InboxUpdated?.Invoke(this, e);
        }

        protected virtual void OnUnreadUpdated(MessagesUpdateEventArgs e)
        {
            UnreadUpdated?.Invoke(this, e);
        }

        protected virtual void OnSentUpdated(MessagesUpdateEventArgs e)
        {
            SentUpdated?.Invoke(this, e);
        }

        public bool MonitorInbox()
        {
            string key = "PrivateMessages_Inbox";
            return Monitor(key, new Thread(() => MonitorInboxThread(key)));
        }

        private void MonitorInboxThread(string key)
        {
            MonitorThread(key, "inbox");
        }

        public bool MonitorUnread()
        {
            string key = "PrivateMessages_Unread";
            return Monitor(key, new Thread(() => MonitorUnreadThread(key)));
        }

        private void MonitorUnreadThread(string key)
        {
            MonitorThread(key, "unread");
        }

        public bool MonitorSent()
        {
            string key = "PrivateMessages_Sent";
            return Monitor(key, new Thread(() => MonitorSentThread(key)));
        }

        private void MonitorSentThread(string key)
        {
            MonitorThread(key, "sent");
        }
    }
}
