using Reddit.Controllers.EventArgs;
using Reddit.Controllers.Internal;
using Reddit.Controllers.Structures;
using Reddit.Exceptions;
using Reddit.Inputs.LinksAndComments;
using Reddit.Inputs.Modmail;
using Reddit.Inputs.PrivateMessages;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Reddit.Controllers
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
        private DateTime? MeLastUpdated { get; set; }

        /// <summary>
        /// Recent modmail conversations.
        /// </summary>
        public ConversationContainer Recent
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
        private ConversationContainer recent;
        private DateTime? RecentLastUpdated { get; set; }

        /// <summary>
        /// Mod modmail conversations.
        /// </summary>
        public ConversationContainer Mod
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
        private ConversationContainer mod;
        private DateTime? ModLastUpdated { get; set; }

        /// <summary>
        /// User modmail conversations.
        /// </summary>
        public ConversationContainer User
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
        private ConversationContainer user;
        private DateTime? UserLastUpdated { get; set; }

        /// <summary>
        /// Unread modmail conversations.
        /// </summary>
        public ConversationContainer Unread
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
        private ConversationContainer unread;
        private DateTime? UnreadLastUpdated { get; set; }

        /// <summary>
        /// Unread messages count.
        /// </summary>
        public ModmailUnreadCount UnreadCount
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
        private ModmailUnreadCount unreadCount;
        private DateTime? UnreadCountLastUpdated { get; set; }

        internal override Models.Internal.Monitor MonitorModel => Dispatch.Monitor;
        internal override ref MonitoringSnapshot Monitoring => ref MonitorModel.Monitoring;
        internal override bool BreakOnFailure { get; set; }
        internal override List<MonitoringSchedule> MonitoringSchedule { get; set; }
        internal override DateTime? MonitoringExpiration { get; set; }

        private Dispatch Dispatch;

        /// <summary>
        /// Create a new instance of the modmail controller.
        /// </summary>
        /// <param name="dispatch"></param>
        public Modmail(Dispatch dispatch) : base()
        {
            Dispatch = dispatch;
        }

        /// <summary>
        /// Returns a User instance with the data returned from a call to the "me" endpoint.
        /// </summary>
        private User GetMe()
        {
            Me = new User(Dispatch, Dispatch.Account.Me());
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
        public ConversationContainer GetConversations(string after = "", string entity = "", string sort = "unread", string state = "all", int limit = 25)
        {
            return GetConversations(new ModmailGetConversationsInput(after, entity, sort, state, limit));
        }

        /// <summary>
        /// Get conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="modmailGetConversationsInput">A valid ModmailGetConversationsInput instance</param>
        /// <returns>The requested conversations.</returns>
        public ConversationContainer GetConversations(ModmailGetConversationsInput modmailGetConversationsInput)
        {
            return Validate(Dispatch.Modmail.GetConversations(modmailGetConversationsInput));
        }

        /// <summary>
        /// Get recent conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="after">base36 modmail conversation id</param>
        /// <param name="entity">comma-delimited list of subreddit names</param>
        /// <param name="state">one of (new, inprogress, mod, notifications, archived, highlighted, all)</param>
        /// <param name="limit">an integer (default: 25)</param>
        /// <returns>The requested conversations.</returns>
        public ConversationContainer GetRecentConversations(string after = "", string entity = "", string state = "all", int limit = 25)
        {
            return GetRecentConversations(new ModmailGetConversationsInput(after, entity, "recent", state, limit));
        }

        /// <summary>
        /// Get recent conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="modmailGetConversationsInput">A valid ModmailGetConversationsInput instance</param>
        /// <returns>The requested conversations.</returns>
        public ConversationContainer GetRecentConversations(ModmailGetConversationsInput modmailGetConversationsInput)
        {
            modmailGetConversationsInput.sort = "recent";

            Recent = GetConversations(modmailGetConversationsInput);
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
        public ConversationContainer GetModConversations(string after = "", string entity = "", string state = "all", int limit = 25)
        {
            return GetModConversations(new ModmailGetConversationsInput(after, entity, "mod", state, limit));
        }

        /// <summary>
        /// Get mod conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="modmailGetConversationsInput">A valid ModmailGetConversationsInput instance</param>
        /// <returns>The requested conversations.</returns>
        public ConversationContainer GetModConversations(ModmailGetConversationsInput modmailGetConversationsInput)
        {
            modmailGetConversationsInput.sort = "mod";

            Mod = GetConversations(modmailGetConversationsInput);
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
        public ConversationContainer GetUserConversations(string after = "", string entity = "", string state = "all", int limit = 25)
        {
            return GetUserConversations(new ModmailGetConversationsInput(after, entity, "user", state, limit));
        }

        /// <summary>
        /// Get user conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="modmailGetConversationsInput">A valid ModmailGetConversationsInput instance</param>
        /// <returns>The requested conversations.</returns>
        public ConversationContainer GetUserConversations(ModmailGetConversationsInput modmailGetConversationsInput)
        {
            modmailGetConversationsInput.sort = "user";

            User = GetConversations(modmailGetConversationsInput);
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
        public ConversationContainer GetUnreadConversations(string after = "", string entity = "", string state = "all", int limit = 25)
        {
            return GetUnreadConversations(new ModmailGetConversationsInput(after, entity, "unread", state, limit));
        }

        /// <summary>
        /// Get user conversations for a logged in user or subreddits.
        /// </summary>
        /// <param name="modmailGetConversationsInput">A valid ModmailGetConversationsInput instance</param>
        /// <returns>The requested conversations.</returns>
        public ConversationContainer GetUnreadConversations(ModmailGetConversationsInput modmailGetConversationsInput)
        {
            modmailGetConversationsInput.sort = "unread";

            Unread = GetConversations(modmailGetConversationsInput);
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
        public ModmailConversationContainer NewConversation(string body = "", string subject = "", string srName = "", string to = "", bool isAuthorHidden = false)
        {
            return NewConversation(new ModmailNewConversationInput(body, isAuthorHidden, srName, subject, to));
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
        public async Task<ModmailConversationContainer> NewConversationAsync(string body = "", string subject = "", string srName = "", string to = "", bool isAuthorHidden = false)
        {
            return await NewConversationAsync(new ModmailNewConversationInput(body, isAuthorHidden, srName, subject, to));
        }

        /// <summary>
        /// Creates a new conversation for a particular SR.
        /// This endpoint will create a ModmailConversation object as well as the first ModmailMessage within the ModmailConversation object.
        /// </summary>
        /// <param name="modmailNewConversationInput">A valid ModmailNewConversationInput instance</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer NewConversation(ModmailNewConversationInput modmailNewConversationInput, string gRecaptchaResponse = "")
        {
            PrivateMessagesComposeInput privateMessagesComposeInput = new PrivateMessagesComposeInput
            {
                subject = modmailNewConversationInput.subject,
                text = modmailNewConversationInput.body,
                to = "/r/" + modmailNewConversationInput.srName
            };
            GenericContainer res = Validate(Dispatch.PrivateMessages.Compose(privateMessagesComposeInput, gRecaptchaResponse));

            return Validate(Dispatch.Modmail.NewConversation(modmailNewConversationInput));
        }

        /// <summary>
        /// Creates a new conversation for a particular SR asynchronously.
        /// This endpoint will create a ModmailConversation object as well as the first ModmailMessage within the ModmailConversation object.
        /// </summary>
        /// <param name="modmailNewConversationInput">A valid ModmailNewConversationInput instance</param>
        public async Task<ModmailConversationContainer> NewConversationAsync(ModmailNewConversationInput modmailNewConversationInput)
        {
            return Validate(await Dispatch.Modmail.NewConversationAsync(modmailNewConversationInput));
        }

        /// <summary>
        /// Returns all messages, mod actions and conversation metadata for a given conversation id.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="markRead">boolean value</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer GetConversation(string conversationId, bool markRead = false)
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
        public ModmailConversationContainer NewMessage(string conversationId, string body = "", bool isAuthorHidden = false, bool isInternal = false)
        {
            return NewMessage(conversationId, new ModmailNewMessageInput(body, isAuthorHidden, isInternal));
        }

        /// <summary>
        /// Creates a new message for a particular conversation asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="body">raw markdown text</param>
        /// <param name="isAuthorHidden">boolean value</param>
        /// <param name="isInternal">boolean value</param>
        public async Task<ModmailConversationContainer> NewMessageAsync(string conversationId, string body = "", bool isAuthorHidden = false, bool isInternal = false)
        {
            return await NewMessageAsync(conversationId, new ModmailNewMessageInput(body, isAuthorHidden, isInternal));
        }

        /// <summary>
        /// Creates a new message for a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="modmailNewMessageInput">A valid ModmailNewMessageInput instance</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer NewMessage(string conversationId, ModmailNewMessageInput modmailNewMessageInput)
        {
            LinksAndCommentsThingInput linksAndCommentsThingInput = new LinksAndCommentsThingInput
            {
                text = modmailNewMessageInput.body,
                thing_id = "t4_" + conversationId
            };
            return Validate(Dispatch.LinksAndComments.Comment<ModmailConversationContainer>(linksAndCommentsThingInput));
            //return Validate(Dispatch.Modmail.NewMessage(conversationId, modmailNewMessageInput));
        }

        /// <summary>
        /// Creates a new message for a particular conversation asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <param name="modmailNewMessageInput">A valid ModmailNewMessageInput instance</param>
        public async Task<ModmailConversationContainer> NewMessageAsync(string conversationId, ModmailNewMessageInput modmailNewMessageInput)
        {
            return Validate(await Dispatch.Modmail.NewMessageAsync(conversationId, modmailNewMessageInput));
        }

        /// <summary>
        /// Marks a conversation as highlighted.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer MarkHighlighted(string conversationId)
        {
            return Validate(Dispatch.Modmail.MarkHighlighted(conversationId));
        }

        /// <summary>
        /// Marks a conversation as highlighted asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task<ModmailConversationContainer> MarkHighlightedAsync(string conversationId)
        {
            return Validate(await Dispatch.Modmail.MarkHighlightedAsync(conversationId));
        }

        /// <summary>
        /// Removes a highlight from a conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer RemoveHighlight(string conversationId)
        {
            return Validate(Dispatch.Modmail.RemoveHighlight(conversationId));
        }

        /// <summary>
        /// Removes a highlight from a conversation asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task<ModmailConversationContainer> RemoveHighlightAsync(string conversationId)
        {
            return Validate(await Dispatch.Modmail.RemoveHighlightAsync(conversationId));
        }

        /// <summary>
        /// Mutes the non-mod user associated with a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer Mute(string conversationId)
        {
            return Validate(Dispatch.Modmail.Mute(conversationId));
        }

        /// <summary>
        /// Mutes the non-mod user associated with a particular conversation asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task<ModmailConversationContainer> MuteAsync(string conversationId)
        {
            return Validate(await Dispatch.Modmail.MuteAsync(conversationId));
        }

        /// <summary>
        /// Unmutes the non-mod user associated with a particular conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public ModmailConversationContainer Unmute(string conversationId)
        {
            return Validate(Dispatch.Modmail.UnMute(conversationId));
        }

        /// <summary>
        /// Unmutes the non-mod user associated with a particular conversation asynchronously.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the conversation data.</returns>
        public async Task<ModmailConversationContainer> UnmuteAsync(string conversationId)
        {
            return Validate(await Dispatch.Modmail.UnMuteAsync(conversationId));
        }

        /// <summary>
        /// Returns recent posts, comments and modmail conversations for the user that started this conversation.
        /// </summary>
        /// <param name="conversationId">base36 modmail conversation id</param>
        /// <returns>An object containing the user data.</returns>
        public ModmailUser UserHistory(string conversationId)
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
            await Dispatch.Modmail.MarkReadAsync(conversationIds);
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
            await Dispatch.Modmail.MarkUnreadAsync(conversationIds);
        }

        /// <summary>
        /// Returns a list of srs that the user moderates that are also enrolled in the new modmail.
        /// </summary>
        /// <returns>A list of subreddits.</returns>
        public ModmailSubredditContainer Subreddits()
        {
            return Validate(Dispatch.Modmail.Subreddits());
        }

        /// <summary>
        /// Endpoint to retrieve the unread conversation count by conversation state.
        /// </summary>
        /// <returns>An object with the int properties: highlighted, notifications, archived, new, inprogress, and mod.</returns>
        public ModmailUnreadCount GetUnreadCount()
        {
            UnreadCount = Validate(Dispatch.Modmail.UnreadCount());
            UnreadCountLastUpdated = DateTime.Now;

            return UnreadCount;
        }

        /// <summary>
        /// Monitor recent modmail messages as they arrive.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorRecent(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "ModmailMessagesRecent";
            return Monitor(key, new Thread(() => MonitorModmailMessagesThread(recent, key, "recent", monitoringDelayMs: monitoringDelayMs)), "ModmailMessages");
        }

        /// <summary>
        /// Monitor mod modmail messages as they arrive.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorMod(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "ModmailMessagesMod";
            return Monitor(key, new Thread(() => MonitorModmailMessagesThread(mod, key, "mod", monitoringDelayMs: monitoringDelayMs)), "ModmailMessages");
        }

        /// <summary>
        /// Monitor user modmail messages as they arrive.
        /// </summary>
        /// <param name="monitoringDelayMs">The number of milliseconds between each monitoring query; leave null to auto-manage</param>
        /// <param name="monitoringBaseDelayMs">The number of milliseconds between each monitoring query PER THREAD (default: 1500)</param>
        /// <param name="schedule">A list of one or more timeframes during which monitoring of this object will occur (default: 24/7)</param>
        /// <param name="breakOnFailure">If true, an exception will be thrown when a monitoring query fails; leave null to keep current setting (default: false)</param>
        /// <param name="monitoringExpiration">If set, monitoring will automatically stop after the specified DateTime is reached</param>
        /// <returns>Whether monitoring was successfully initiated.</returns>
        public bool MonitorUser(int? monitoringDelayMs = null, int? monitoringBaseDelayMs = null, List<MonitoringSchedule> schedule = null, bool? breakOnFailure = null,
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

            string key = "ModmailMessagesUser";
            return Monitor(key, new Thread(() => MonitorModmailMessagesThread(user, key, "user", monitoringDelayMs: monitoringDelayMs)), "ModmailMessages");
        }

        /// <summary>
        /// Monitor unread modmail messages as they arrive.
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

            string key = "ModmailMessagesUnread";
            return Monitor(key, new Thread(() => MonitorModmailMessagesThread(unread, key, "unread", monitoringDelayMs: monitoringDelayMs)), "ModmailMessages");
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

        private void MonitorModmailMessagesThread(ConversationContainer conversationContainer, string key, string type, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            if (startDelayMs > 0)
            {
                Thread.Sleep(startDelayMs);
            }

            monitoringDelayMs = (monitoringDelayMs.HasValue ? monitoringDelayMs : Monitoring.Count() * MonitoringWaitDelayMS);

            while (!Terminate
                && Monitoring.Get(key).Contains("ModmailMessages"))
            {
                if (MonitoringExpiration.HasValue
                    && DateTime.Now > MonitoringExpiration.Value)
                {
                    MonitorModel.RemoveMonitoringKey(key, "ModmailMessages", ref Monitoring);
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

                Dictionary<string, Conversation> oldConversationList;
                Dictionary<string, Conversation> newConversationList;
                Dictionary<string, ConversationMessage> oldMessageList;
                Dictionary<string, ConversationMessage> newMessageList;

                ConversationContainer res;
                try
                {
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

                    if (Diff(oldConversationList, newConversationList, out Dictionary<string, Conversation> addedC, out Dictionary<string, Conversation> removedC))
                    {
                        if (Diff(oldMessageList, newMessageList, out Dictionary<string, ConversationMessage> addedM, out Dictionary<string, ConversationMessage> removedM))
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
                }
                catch (Exception) when (!BreakOnFailure) { }

                Wait(monitoringDelayMs.Value);
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

        private bool Diff(Dictionary<string, Conversation> oldList, Dictionary<string, Conversation> newList,
            out Dictionary<string, Conversation> added, out Dictionary<string, Conversation> removed)
        {
            return Diff<Conversation>(oldList, newList, out added, out removed);
        }

        private bool Diff(Dictionary<string, ConversationMessage> oldList, Dictionary<string, ConversationMessage> newList,
            out Dictionary<string, ConversationMessage> added, out Dictionary<string, ConversationMessage> removed)
        {
            return Diff<ConversationMessage>(oldList, newList, out added, out removed);
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

        public bool ModmailMessagesRecentIsMonitored()
        {
            return IsMonitored("ModmailMessagesRecent", "ModmailMessages");
        }

        public bool ModmailMessagesModIsMonitored()
        {
            return IsMonitored("ModmailMessagesMod", "ModmailMessages");
        }

        public bool ModmailMessagesUserIsMonitored()
        {
            return IsMonitored("ModmailMessagesUser", "ModmailMessages");
        }

        public bool ModmailMessagesUnreadIsMonitored()
        {
            return IsMonitored("ModmailMessagesUnread", "ModmailMessages");
        }

        protected override Thread CreateMonitoringThread(string key, string subKey, int startDelayMs = 0, int? monitoringDelayMs = null)
        {
            switch (key)
            {
                default:
                    throw new RedditControllerException("Unrecognized key.");
                case "ModmailMessagesRecent":
                    return new Thread(() => MonitorModmailMessagesThread(recent, key, "recent", startDelayMs, monitoringDelayMs));
                case "ModmailMessagesMod":
                    return new Thread(() => MonitorModmailMessagesThread(mod, key, "mod", startDelayMs, monitoringDelayMs));
                case "ModmailMessagesUser":
                    return new Thread(() => MonitorModmailMessagesThread(user, key, "user", startDelayMs, monitoringDelayMs));
                case "ModmailMessagesUnread":
                    return new Thread(() => MonitorModmailMessagesThread(unread, key, "unread", startDelayMs, monitoringDelayMs));
            }
        }
    }
}
