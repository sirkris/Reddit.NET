using Reddit.Controllers.EventArgs;
using Reddit.Controllers.Internal;
using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using Reddit.Inputs.LinksAndComments;
using Reddit.Inputs.PrivateMessages;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for private messages.
    /// </summary>
    public class PrivateMessages : Monitors
    {
        public event EventHandler<MessagesUpdateEventArgs> InboxUpdated;
        public event EventHandler<MessagesUpdateEventArgs> UnreadUpdated;
        public event EventHandler<MessagesUpdateEventArgs> SentUpdated;

        internal override Models.Internal.Monitor MonitorModel => Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;
        internal override bool BreakOnFailure { get; set; }
        internal override List<MonitoringSchedule> MonitoringSchedule { get; set; }
        internal override DateTime? MonitoringExpiration { get; set; }

        /// <summary>
        /// List of inbox messages.
        /// </summary>
        public List<Message> Inbox
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
        internal List<Message> inbox;

        /// <summary>
        /// List of unread messages.
        /// </summary>
        public List<Message> Unread
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
        internal List<Message> unread;

        /// <summary>
        /// List of sent messages.
        /// </summary>
        public List<Message> Sent
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
        internal List<Message> sent;

        private DateTime? InboxLastUpdated { get; set; }
        private DateTime? UnreadLastUpdated { get; set; }
        private DateTime? SentLastUpdated { get; set; }

        private Dispatch Dispatch;

        /// <summary>
        /// Create a new instance of the private messages controller.
        /// </summary>
        /// <param name="dispatch"></param>
        /// <param name="inbox"></param>
        /// <param name="unread"></param>
        /// <param name="sent"></param>
        public PrivateMessages(Dispatch dispatch, List<Message> inbox = null, List<Message> unread = null,
            List<Message> sent = null) 
            : base()
        {
            Inbox = inbox ?? new List<Message>();
            Unread = unread ?? new List<Message>();
            Sent = sent ?? new List<Message>();

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
        public List<Message> GetMessages(string where, bool mark = true, int limit = 25, string after = "", string before = "",
            string show = "all", bool srDetail = false, bool includeCategories = false, int count = 0, string mid = "")
        {
            return GetMessages(where, new PrivateMessagesGetMessagesInput(mark, mid, after, before, includeCategories,
                count, limit, show, srDetail));
        }

        /// <summary>
        /// Retrieve private messages for the current user.
        /// </summary>
        /// <param name="where">One of (inbox, unread, sent)</param>
        /// <param name="privateMessagesGetMessagesInput">A valid PrivateMessagesGetMessagesInput instance</param>
        /// <returns>A list of messages.</returns>
        public List<Message> GetMessages(string where, PrivateMessagesGetMessagesInput privateMessagesGetMessagesInput)
        {
            MessageContainer messageContainer = Dispatch.PrivateMessages.GetMessages(where, privateMessagesGetMessagesInput);

            List<Message> res = new List<Message>();
            if (messageContainer != null && messageContainer.Data != null && messageContainer.Data.Children != null)
            {
                foreach (MessageChild messageChild in messageContainer.Data.Children)
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
        public List<Message> GetMessagesInbox(bool mark = true, int limit = 25, string after = "", string before = "",
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
        public List<Message> GetMessagesUnread(bool mark = true, int limit = 25, string after = "", string before = "",
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
        public List<Message> GetMessagesSent(bool mark = true, int limit = 25, string after = "", string before = "",
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
            await Dispatch.PrivateMessages.ReadAllMessagesAsync(filterTypes);
        }

        /// <summary>
        /// Collapse a message.
        /// </summary>
        /// <param name="ids">A comma-separated list of thing fullnames</param>
        public void CollapseMessage(string ids)
        {
            Dispatch.PrivateMessages.CollapseMessage(ids);
        }

        /// <summary>
        /// Collapse a message asynchronously.
        /// </summary>
        /// <param name="ids">A comma-separated list of thing fullnames</param>
        public async Task CollapseMessageAsync(string ids)
        {
            await Dispatch.PrivateMessages.CollapseMessageAsync(ids);
        }

        /// <summary>
        /// Delete messages from the recipient's view of their inbox.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public void DeleteMessage(string id)
        {
            Dispatch.PrivateMessages.DelMsg(id);
        }

        /// <summary>
        /// Delete messages from the recipient's view of their inbox asynchronously.
        /// </summary>
        /// <param name="id">fullname of a thing</param>
        public async Task DeleteMessageAsync(string id)
        {
            await Dispatch.PrivateMessages.DelMsgAsync(id);
        }

        /// <summary>
        /// Mark a message as read.
        /// </summary>
        /// <param name="ids">A comma-separated list of thing fullnames</param>
        public void ReadMessage(string ids)
        {
            Dispatch.PrivateMessages.ReadMessage(ids);
        }

        /// <summary>
        /// Mark a message as read asynchronously.
        /// </summary>
        /// <param name="ids">A comma-separated list of thing fullnames</param>
        public async Task ReadMessageAsync(string ids)
        {
            await Dispatch.PrivateMessages.ReadMessageAsync(ids);
        }

        /// <summary>
        /// Uncollapse a message.
        /// </summary>
        /// <param name="ids">A comma-separated list of thing fullnames</param>
        public void UncollapseMessage(string ids)
        {
            Dispatch.PrivateMessages.UncollapseMessage(ids);
        }

        /// <summary>
        /// Uncollapse a message asynchronously.
        /// </summary>
        /// <param name="ids">A comma-separated list of thing fullnames</param>
        public async Task UncollapseMessageAsync(string ids)
        {
            await Dispatch.PrivateMessages.UncollapseMessageAsync(ids);
        }

        /// <summary>
        /// Mark a message as unread.
        /// </summary>
        /// <param name="ids">A comma-separated list of thing fullnames</param>
        public void UnreadMessage(string ids)
        {
            Dispatch.PrivateMessages.UnreadMessage(ids);
        }

        /// <summary>
        /// Mark a message as unread asynchronously.
        /// </summary>
        /// <param name="ids">A comma-separated list of thing fullnames</param>
        public async Task UnreadMessageAsync(string ids)
        {
            await Dispatch.PrivateMessages.UnreadMessageAsync(ids);
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
            Validate(Dispatch.PrivateMessages.Compose(new PrivateMessagesComposeInput(fromSr, subject, text, to), gRecaptchaResponse));
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
            Validate(await Dispatch.PrivateMessages.ComposeAsync(new PrivateMessagesComposeInput(fromSr, subject, text, to), gRecaptchaResponse));
        }

        /// <summary>
        /// Send a private message.
        /// </summary>
        /// <param name="privateMessagesComposeInput">A valid PrivateMessagesComposeInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        public void Compose(PrivateMessagesComposeInput privateMessagesComposeInput, string gRecaptchaResponse = "")
        {
            Validate(Dispatch.PrivateMessages.Compose(privateMessagesComposeInput, gRecaptchaResponse));
        }

        /// <summary>
        /// Send a private message asynchronously.
        /// </summary>
        /// <param name="privateMessagesComposeInput">A valid PrivateMessagesComposeInput instance</param>
        /// <param name="gRecaptchaResponse"></param>
        public async Task ComposeAsync(PrivateMessagesComposeInput privateMessagesComposeInput, string gRecaptchaResponse = "")
        {
            Validate(await Dispatch.PrivateMessages.ComposeAsync(privateMessagesComposeInput, gRecaptchaResponse));
        }

        /// <summary>
        /// Reply to a private message.
        /// </summary>
        /// <param name="linksAndCommentsThingInput">A valid LinksAndCommentsThingInput instance</param>
        /// <returns>The created message reply.</returns>
        public MessageContainer Reply(LinksAndCommentsThingInput linksAndCommentsThingInput)
        {
            return Validate(Dispatch.LinksAndComments.Comment<MessageContainer>(linksAndCommentsThingInput));
        }

        /// <summary>
        /// Reply to a private message asynchronously.
        /// </summary>
        /// <param name="linksAndCommentsThingInput">A valid LinksAndCommentsThingInput instance</param>
        /// <returns>The created message reply.</returns>
        public async Task<MessageContainer> ReplyAsync(LinksAndCommentsThingInput linksAndCommentsThingInput)
        {
            return Validate(await Dispatch.LinksAndComments.CommentAsync<MessageContainer>(linksAndCommentsThingInput));
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

        /// <summary>
        /// Monitor inbox messages.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorInbox(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null)
        {
            if (breakOnFailure.HasValue)
            {
                BreakOnFailure = breakOnFailure.Value;
            }

            if (schedule != null)
            {
                MonitoringSchedule = schedule;
            }

            if (monitoringBaseDelayMs.HasValue)
            {
                MonitoringWaitDelayMS = monitoringBaseDelayMs.Value;
            }

            if (monitoringExpiration.HasValue)
            {
                MonitoringExpiration = monitoringExpiration;
            }

            string key = "PrivateMessagesInbox";
            return Monitor(key, new Thread(() => MonitorInboxThread(key, monitoringDelayMs)), "PrivateMessages");
        }

        private void MonitorInboxThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPrivateMessagesThread(key, "inbox", monitoringDelayMs: monitoringDelayMs);
        }

        /// <summary>
        /// Monitor unread messages.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorUnread(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null)
        {
            if (breakOnFailure.HasValue)
            {
                BreakOnFailure = breakOnFailure.Value;
            }

            if (schedule != null)
            {
                MonitoringSchedule = schedule;
            }

            if (monitoringBaseDelayMs.HasValue)
            {
                MonitoringWaitDelayMS = monitoringBaseDelayMs.Value;
            }

            if (monitoringExpiration.HasValue)
            {
                MonitoringExpiration = monitoringExpiration;
            }

            string key = "PrivateMessagesUnread";
            return Monitor(key, new Thread(() => MonitorUnreadThread(key, monitoringDelayMs)), "PrivateMessages");
        }

        private void MonitorUnreadThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPrivateMessagesThread(key, "unread", monitoringDelayMs: monitoringDelayMs);
        }

        /// <summary>
        /// Monitor sent messages.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorSent(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
            DateTime? monitoringExpiration = null)
        {
            if (breakOnFailure.HasValue)
            {
                BreakOnFailure = breakOnFailure.Value;
            }

            if (schedule != null)
            {
                MonitoringSchedule = schedule;
            }

            if (monitoringBaseDelayMs.HasValue)
            {
                MonitoringWaitDelayMS = monitoringBaseDelayMs.Value;
            }

            if (monitoringExpiration.HasValue)
            {
                MonitoringExpiration = monitoringExpiration;
            }

            string key = "PrivateMessagesSent";
            return Monitor(key, new Thread(() => MonitorSentThread(key, monitoringDelayMs)), "PrivateMessages");
        }

        private void MonitorSentThread(string key, int? monitoringDelayMs = null)
        {
            MonitorPrivateMessagesThread(key, "sent", monitoringDelayMs: monitoringDelayMs);
        }

        private void MonitorPrivateMessagesThread(string key, string type, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            monitoringDelayMs = (monitoringDelayMs.HasValue ? monitoringDelayMs : Monitoring.Count() * MonitoringWaitDelayMS);

            while (!Terminate
                && Monitoring.Get(key).Contains("PrivateMessages"))
            {
                if (MonitoringExpiration.HasValue
                    && DateTime.Now > MonitoringExpiration.Value)
                {
                    MonitorModel.RemoveMonitoringKey(key, "PrivateMessages", ref Monitoring);
                    Threads.Remove(key);

                    break;
                }

                while (!IsScheduled())
                {
                    if (Terminate)
                    {
                        break;
                    }

                    Thread.Sleep(15000);
                }

                if (Terminate)
                {
                    break;
                }

                List<Message> oldList;
                List<Message> newList;
                try
                {
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

                    if (Lists.ListDiff(oldList, newList, out List<Message> added, out List<Message> removed))
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
                }
                catch (Exception) when (!BreakOnFailure) { }

                Wait(monitoringDelayMs.Value);
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

        public bool PrivateMessagesInboxIsMonitored()
        {
            return IsMonitored("PrivateMessagesInbox", "PrivateMessages");
        }

        public bool PrivateMessagesUnreadIsMonitored()
        {
            return IsMonitored("PrivateMessagesUnread", "PrivateMessages");
        }

        public bool PrivateMessagesSentIsMonitored()
        {
            return IsMonitored("PrivateMessagesSent", "PrivateMessages");
        }

        protected override Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "PrivateMessagesInbox":
                    return new Thread(() => MonitorPrivateMessagesThread(key, "inbox", startDelayMs, monitoringDelayMs));
                case "PrivateMessagesUnread":
                    return new Thread(() => MonitorPrivateMessagesThread(key, "unread", startDelayMs, monitoringDelayMs));
                case "PrivateMessagesSent":
                    return new Thread(() => MonitorPrivateMessagesThread(key, "sent", startDelayMs, monitoringDelayMs));
            }
        }
    }
}
