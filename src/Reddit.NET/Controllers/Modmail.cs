using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Controllers.Internal;
using Reddit.NET.Controllers.Structures;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reddit.NET.Controllers
{
    /// <summary>
    /// Controller class for modmail.
    /// </summary>
    public class Modmail : Monitors
    {
        public event EventHandler<ModmailConversationsEventArgs> RecentUpdated;
        public event EventHandler<ModmailConversationsEventArgs> ModUpdated;
        public event EventHandler<ModmailConversationsEventArgs> UserUpdated;
        public event EventHandler<ModmailConversationsEventArgs> UnreadUpdated;

        private User Me
        {
            get
            {
                return (MeLastUpdated.HasValue
                    && MeLastUpdated.Value.AddMinutes(1) > DateTime.Now ? me : GetMe());
            }
            set
            {
                me = value;
                MeLastUpdated = DateTime.Now;
            }
        }
        private User me;
        private DateTime? MeLastUpdated;

        /// <summary>
        /// Recent modmail conversations.
        /// </summary>
        public RedditThings.ConversationContainer Recent
        {
            get
            {
                return (RecentLastUpdated.HasValue
                    && RecentLastUpdated.Value.AddSeconds(15) > DateTime.Now ? recent : GetRecentConversations());
            }
            private set
            {
                recent = value;
            }
        }
        private RedditThings.ConversationContainer recent;
        private DateTime? RecentLastUpdated;

        /// <summary>
        /// Mod modmail conversations.
        /// </summary>
        public RedditThings.ConversationContainer Mod
        {
            get
            {
                return (ModLastUpdated.HasValue
                    && ModLastUpdated.Value.AddSeconds(15) > DateTime.Now ? mod : GetModConversations());
            }
            private set
            {
                mod = value;
            }
        }
        private RedditThings.ConversationContainer mod;
        private DateTime? ModLastUpdated;

        /// <summary>
        /// User modmail conversations.
        /// </summary>
        public RedditThings.ConversationContainer User
        {
            get
            {
                return (UserLastUpdated.HasValue
                    && UserLastUpdated.Value.AddSeconds(15) > DateTime.Now ? user : GetUserConversations());
            }
            private set
            {
                user = value;
            }
        }
        private RedditThings.ConversationContainer user;
        private DateTime? UserLastUpdated;

        /// <summary>
        /// Unread modmail conversations.
        /// </summary>
        public RedditThings.ConversationContainer Unread
        {
            get
            {
                return (UnreadLastUpdated.HasValue
                    && UnreadLastUpdated.Value.AddSeconds(15) > DateTime.Now ? unread : GetUnreadConversations());
            }
            private set
            {
                unread = value;
            }
        }
        private RedditThings.ConversationContainer unread;
        private DateTime? UnreadLastUpdated;

        /// <summary>
        /// Unread messages count.
        /// </summary>
        public RedditThings.ModmailUnreadCount UnreadCount
        {
            get
            {
                return (UnreadCountLastUpdated.HasValue
                    && UnreadCountLastUpdated.Value.AddSeconds(15) > DateTime.Now ? unreadCount : GetUnreadCount());
            }
            private set
            {
                unreadCount = value;
            }
        }
        private RedditThings.ModmailUnreadCount unreadCount;
        private DateTime? UnreadCountLastUpdated;

        internal override ref Models.Internal.Monitor MonitorModel => ref Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;

        private Dispatch Dispatch;

        /// <summary>
        /// Create a new instance of the modmail controller.
        /// </summary>
        /// <param name="dispatch"></param>
        public Modmail(ref Dispatch dispatch) : base()
        {
            Dispatch = dispatch;
        }

        /// <summary>
        /// Returns a User instance with the data returned from a call to the "me" endpoint.
        /// </summary>
        private User GetMe()
        {
            Me = new User(ref Dispatch, Dispatch.Account.Me());
            return Me;
        }

        /// <summary>
        /// Get conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="after">base36 modmail conversation id</param>
        /// <param name="entity">comma-delimited list of subreddit names</param>
        /// <param name="sort">one of (recent, mod, user, unread)</param>
        /// <param name="state">one of (new, inprogress, mod, notifications, archived, highlighted, all)</param>
        /// <param name="limit">an integer (default: 25)</param>
        /// <returns>The requested conversations.</returns>
        public RedditThings.ConversationContainer GetConversations(string after = "", string entity = "", string sort = "unread", string state = "all", int limit = 25)
        {
            return Validate(Dispatch.Modmail.GetConversations(after, entity, sort, state, limit));
        }

        /// <summary>
        /// Get recent conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="after">base36 modmail conversation id</param>
        /// <param name="entity">comma-delimited list of subreddit names</param>
        /// <param name="state">one of (new, inprogress, mod, notifications, archived, highlighted, all)</param>
        /// <param name="limit">an integer (default: 25)</param>
        /// <returns>The requested conversations.</returns>
        public RedditThings.ConversationContainer GetRecentConversations(string after = "", string entity = "", string state = "all", int limit = 25)
        {
            Recent = GetConversations(after, entity, "recent", state, limit);
            RecentLastUpdated = DateTime.Now;

            return Recent;
        }

        /// <summary>
        /// Get mod conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="after">base36 modmail conversation id</param>
        /// <param name="entity">comma-delimited list of subreddit names</param>
        /// <param name="state">one of (new, inprogress, mod, notifications, archived, highlighted, all)</param>
        /// <param name="limit">an integer (default: 25)</param>
        /// <returns>The requested conversations.</returns>
        public RedditThings.ConversationContainer GetModConversations(string after = "", string entity = "", string state = "all", int limit = 25)
        {
            Mod = GetConversations(after, entity, "mod", state, limit);
            ModLastUpdated = DateTime.Now;

            return Mod;
        }

        /// <summary>
        /// Get user conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="after">base36 modmail conversation id</param>
        /// <param name="entity">comma-delimited list of subreddit names</param>
        /// <param name="state">one of (new, inprogress, mod, notifications, archived, highlighted, all)</param>
        /// <param name="limit">an integer (default: 25)</param>
        /// <returns>The requested conversations.</returns>
        public RedditThings.ConversationContainer GetUserConversations(string after = "", string entity = "", string state = "all", int limit = 25)
        {
            User = GetConversations(after, entity, "user", state, limit);
            UserLastUpdated = DateTime.Now;

            return User;
        }

        /// <summary>
        /// Get unread conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="after">base36 modmail conversation id</param>
        /// <param name="entity">comma-delimited list of subreddit names</param>
        /// <param name="state">one of (new, inprogress, mod, notifications, archived, highlighted, all)</param>
        /// <param name="limit">an integer (default: 25)</param>
        /// <returns>The requested conversations.</returns>
        public RedditThings.ConversationContainer GetUnreadConversations(string after = "", string entity = "", string state = "all", int limit = 25)
        {
            Unread = GetConversations(after, entity, "unread", state, limit);
            UnreadLastUpdated = DateTime.Now;

            return Unread;
        }

        /// <summary>
        /// Count the number of conversations.
        /// </summary>
        /// <param name="useRecent">Count recent conversations.</param>
        /// <param name="useMod">Count Mod conversations.</param>
        /// <param name="useUser">Count User conversations.</param>
        /// <param name="useUnread">Count Unread conversations.</param>
        /// <returns>The number of conversations.</returns>
        public int Count(bool useRecent = true, bool useMod = true, bool useUser = true, bool useUnread = true)
        {
            return (useRecent ? Recent.ConversationIds.Count : 0)
                + (useMod ? Mod.ConversationIds.Count : 0)
                + (useUser ? User.ConversationIds.Count : 0)
                + (useUnread ? Unread.ConversationIds.Count : 0);
        }

        /// <summary>
        /// Creates a new conversation for a particular SR.
        /// This endpoint will create a ModmailConversation object as well as the first ModmailMessage within the ModmailConversation object.
        /// </summary>
        /// <param name="body">raw markdown text</param>
        /// <param name="subject">a string no longer than 100 characters</param>
        /// <param name="to">Modmail conversation recipient username</param>
        /// <param name="isAuthorHidden">boolean value</param>
        /// <param name="srName">subreddit name</param>
        /// <returns>An object containing the conversation data.</returns>
        public RedditThings.ModmailConversationContainer NewConversation(string body, string subject, string srName, string to = "", bool isAuthorHidden = false)
        {
            return Validate(Dispatch.Modmail.NewConversation(body, isAuthorHidden, srName, subject, to));
        }

        /// <summary>
        /// Creates a new conversation for a particular SR asynchronously.
        /// This endpoint will create a ModmailConversation object as well as the first ModmailMessage within the ModmailConversation object.
        /// </summary>
        /// <param name="body">raw markdown text</param>
        /// <param name="subject">a string no longer than 100 characters</param>
        /// <param name="to">Modmail conversation recipient username</param>
        /// <param name="isAuthorHidden">boolean value</param>
        /// <param name="srName">subreddit name</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task NewConversationAsync(string body, string subject, string srName, string to = "", bool isAuthorHidden = false)
        {
            await Task.Run(() =>
            {
                NewConversation(body, subject, srName, to, isAuthorHidden);
            });
        }

        /// <summary>
        /// Returns all messages, mod actions and conversation metadata for a given conversation id.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="markRead">boolean value</param>
        /// <returns>An object containing the conversation data.</returns>
        public RedditThings.ModmailConversationContainer GetConversation(string conversationId, bool markRead = false)
        {
            return Validate(Dispatch.Modmail.GetConversation(conversationId, markRead));
        }

        /// <summary>
        /// Creates a new message for a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="body">raw markdown text</param>
        /// <param name="isAuthorHidden">boolean value</param>
        /// <param name="isInternal">boolean value</param>
        /// <returns>An object containing the conversation data.</returns>
        public RedditThings.ModmailConversationContainer NewMessage(string conversationId, string body, bool isAuthorHidden = false, bool isInternal = false)
        {
            return Validate(Dispatch.Modmail.NewMessage(conversationId, body, isAuthorHidden, isInternal));
        }

        /// <summary>
        /// Creates a new message for a particular conversation asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="body">raw markdown text</param>
        /// <param name="isAuthorHidden">boolean value</param>
        /// <param name="isInternal">boolean value</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task NewMessageAsync(string conversationId, string body, bool isAuthorHidden = false, bool isInternal = false)
        {
            await Task.Run(() =>
            {
                NewMessage(conversationId, body, isAuthorHidden, isInternal);
            });
        }

        /// <summary>
        /// Marks a conversation as highlighted.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public RedditThings.ModmailConversationContainer MarkHighlighted(string conversationId)
        {
            return Validate(Dispatch.Modmail.MarkHighlighted(conversationId));
        }

        /// <summary>
        /// Marks a conversation as highlighted asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task MarkHighlightedAsync(string conversationId)
        {
            await Task.Run(() =>
            {
                MarkHighlighted(conversationId);
            });
        }

        /// <summary>
        /// Removes a highlight from a conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public RedditThings.ModmailConversationContainer RemoveHighlight(string conversationId)
        {
            return Validate(Dispatch.Modmail.RemoveHighlight(conversationId));
        }

        /// <summary>
        /// Removes a highlight from a conversation asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task RemoveHighlightAsync(string conversationId)
        {
            await Task.Run(() =>
            {
                RemoveHighlight(conversationId);
            });
        }

        /// <summary>
        /// Mutes the non-mod user associated with a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public RedditThings.ModmailConversationContainer Mute(string conversationId)
        {
            return Validate(Dispatch.Modmail.Mute(conversationId));
        }

        /// <summary>
        /// Mutes the non-mod user associated with a particular conversation asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task MuteAsync(string conversationId)
        {
            await Task.Run(() =>
            {
                Mute(conversationId);
            });
        }

        /// <summary>
        /// Unmutes the non-mod user associated with a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public RedditThings.ModmailConversationContainer Unmute(string conversationId)
        {
            return Validate(Dispatch.Modmail.UnMute(conversationId));
        }

        /// <summary>
        /// Unmutes the non-mod user associated with a particular conversation asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task UnmuteAsync(string conversationId)
        {
            await Task.Run(() =>
            {
                Unmute(conversationId);
            });
        }

        /// <summary>
        /// Returns recent posts, comments and modmail conversations for the user that started this conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the user data.</returns>
        public RedditThings.ModmailUser UserHistory(string conversationId)
        {
            return Validate(Dispatch.Modmail.User(conversationId));
        }

        /// <summary>
        /// Marks conversations as read for the user.
        /// </summary>
        /// <param name="conversationIds">A comma-separated list of items</param>
        public void MarkRead(string conversationIds)
        {
            Dispatch.Modmail.MarkRead(conversationIds);
        }

        /// <summary>
        /// Marks conversations as read for the user asynchronously.
        /// </summary>
        /// <param name="conversationIds">A comma-separated list of items</param>
        public async Task MarkReadAsync(string conversationIds)
        {
            await Task.Run(() =>
            {
                MarkRead(conversationIds);
            });
        }

        /// <summary>
        /// Marks conversations as unread for the user.
        /// </summary>
        /// <param name="conversationIds">A comma-separated list of items</param>
        public void MarkUnread(string conversationIds)
        {
            Dispatch.Modmail.MarkUnread(conversationIds);
        }

        /// <summary>
        /// Marks conversations as unread for the user asynchronously.
        /// </summary>
        /// <param name="conversationIds">A comma-separated list of items</param>
        public async Task MarkUnreadAsync(string conversationIds)
        {
            await Task.Run(() =>
            {
                MarkUnread(conversationIds);
            });
        }

        /// <summary>
        /// Returns a list of srs that the user moderates that are also enrolled in the new modmail.
        /// </summary>
        /// <returns>A list of subreddits.</returns>
        public RedditThings.ModmailSubredditContainer Subreddits()
        {
            return Validate(Dispatch.Modmail.Subreddits());
        }

        /// <summary>
        /// Endpoint to retrieve the unread conversation count by conversation state.
        /// </summary>
        /// <returns>An object with the int properties: highlighted, notifications, archived, new, inprogress, and mod.</returns>
        public RedditThings.ModmailUnreadCount GetUnreadCount()
        {
            UnreadCount = Validate(Dispatch.Modmail.UnreadCount());
            UnreadCountLastUpdated = DateTime.Now;

            return UnreadCount;
        }

        /// <summary>
        /// Monitor recent modmail messages as they arrive.
        /// </summary>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorRecent()
        {
            string key = "ModmailMessagesRecent";
            return Monitor(key, new Thread(() => MonitorModmailMessagesThread(recent, key, "recent")), "ModmailMessages");
        }

        /// <summary>
        /// Monitor mod modmail messages as they arrive.
        /// </summary>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorMod()
        {
            string key = "ModmailMessagesMod";
            return Monitor(key, new Thread(() => MonitorModmailMessagesThread(mod, key, "mod")), "ModmailMessages");
        }

        /// <summary>
        /// Monitor user modmail messages as they arrive.
        /// </summary>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorUser()
        {
            string key = "ModmailMessagesUser";
            return Monitor(key, new Thread(() => MonitorModmailMessagesThread(user, key, "user")), "ModmailMessages");
        }

        /// <summary>
        /// Monitor unread modmail messages as they arrive.
        /// </summary>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorUnread()
        {
            string key = "ModmailMessagesUnread";
            return Monitor(key, new Thread(() => MonitorModmailMessagesThread(unread, key, "unread")), "ModmailMessages");
        }

        protected virtual void OnRecentUpdated(ModmailConversationsEventArgs e)
        {
            RecentUpdated?.Invoke(this, e);
        }

        protected virtual void OnModUpdated(ModmailConversationsEventArgs e)
        {
            ModUpdated?.Invoke(this, e);
        }

        protected virtual void OnUserUpdated(ModmailConversationsEventArgs e)
        {
            UserUpdated?.Invoke(this, e);
        }

        protected virtual void OnUnreadUpdated(ModmailConversationsEventArgs e)
        {
            UnreadUpdated?.Invoke(this, e);
        }

        private void MonitorModmailMessagesThread(RedditThings.ConversationContainer conversationContainer, string key, string type, int startDelayMs = 0)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            while (!Terminate
                && Monitoring.Get(key).Contains("ModmailMessages"))
            {
                Dictionary<string, RedditThings.Conversation> oldConversationList;
                Dictionary<string, RedditThings.Conversation> newConversationList;
                Dictionary<string, RedditThings.ConversationMessage> oldMessageList;
                Dictionary<string, RedditThings.ConversationMessage> newMessageList;

                RedditThings.ConversationContainer res;
                switch (type)
                {
                    default:
                        throw new RedditControllerException("Unrecognized type '" + type + "'.");
                    case "recent":
                        res = GetRecentConversations();
                        break;
                    case "mod":
                        res = GetModConversations();
                        break;
                    case "user":
                        res = GetUserConversations();
                        break;
                    case "unread":
                        res = GetUnreadConversations();
                        break;
                }

                oldConversationList = conversationContainer?.Conversations;
                newConversationList = res.Conversations;
                oldMessageList = conversationContainer?.Messages;
                newMessageList = res.Messages;

                if (Diff(oldConversationList, newConversationList, out Dictionary<string, RedditThings.Conversation> addedC, out Dictionary<string, RedditThings.Conversation> removedC))
                {
                    if (Diff(oldMessageList, newMessageList, out Dictionary<string, RedditThings.ConversationMessage> addedM, out Dictionary<string, RedditThings.ConversationMessage> removedM))
                    {
                        // Event handler to alert the calling app that the list has changed.  --Kris
                        ModmailConversationsEventArgs args = new ModmailConversationsEventArgs
                        {
                            NewConversations = newConversationList, 
                            OldConversations = oldConversationList, 
                            AddedConversations = addedC, 
                            RemovedConversations = removedC, 
                            NewMessages = newMessageList, 
                            OldMessages = oldMessageList, 
                            AddedMessages = addedM, 
                            RemovedMessages = removedM
                        };
                        TriggerUpdate(args, type);
                    }
                }

                Thread.Sleep(Monitoring.Count() * MonitoringWaitDelayMS);
            }
        }

        private bool Diff<T>(Dictionary<string, T> oldList, Dictionary<string, T> newList,
            out Dictionary<string, T> added, out Dictionary<string, T> removed)
        {
            added = new Dictionary<string, T>();
            removed = new Dictionary<string, T>();

            oldList = oldList ?? new Dictionary<string, T>();
            newList = newList ?? new Dictionary<string, T>();

            List<string> addedKeys = newList.Keys.Except(oldList.Keys).ToList();
            List<string> removedKeys = oldList.Keys.Except(newList.Keys).ToList();

            foreach (string key in addedKeys)
            {
                added.Add(key, newList[key]);
            }
            foreach (string key in removedKeys)
            {
                removed.Add(key, oldList[key]);
            }

            return !(addedKeys.Count == 0 && removedKeys.Count == 0);
        }

        private bool Diff(Dictionary<string, RedditThings.Conversation> oldList, Dictionary<string, RedditThings.Conversation> newList,
            out Dictionary<string, RedditThings.Conversation> added, out Dictionary<string, RedditThings.Conversation> removed)
        {
            return Diff<RedditThings.Conversation>(oldList, newList, out added, out removed);
        }

        private bool Diff(Dictionary<string, RedditThings.ConversationMessage> oldList, Dictionary<string, RedditThings.ConversationMessage> newList,
            out Dictionary<string, RedditThings.ConversationMessage> added, out Dictionary<string, RedditThings.ConversationMessage> removed)
        {
            return Diff<RedditThings.ConversationMessage>(oldList, newList, out added, out removed);
        }

        protected void TriggerUpdate(ModmailConversationsEventArgs args, string type)
        {
            switch (type)
            {
                case "recent":
                    OnRecentUpdated(args);
                    break;
                case "mod":
                    OnModUpdated(args);
                    break;
                case "user":
                    OnUserUpdated(args);
                    break;
                case "unread":
                    OnUnreadUpdated(args);
                    break;
            }
        }

        protected override Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "ModmailMessagesRecent":
                    return new Thread(() => MonitorModmailMessagesThread(recent, key, "recent", startDelayMs));
                case "ModmailMessagesMod":
                    return new Thread(() => MonitorModmailMessagesThread(mod, key, "mod", startDelayMs));
                case "ModmailMessagesUser":
                    return new Thread(() => MonitorModmailMessagesThread(user, key, "user", startDelayMs));
                case "ModmailMessagesUnread":
                    return new Thread(() => MonitorModmailMessagesThread(unread, key, "unread", startDelayMs));
            }
        }
    }
}
