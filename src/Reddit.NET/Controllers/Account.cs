using Reddit.Inputs;
using Reddit.Things;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reddit.Controllers
{
    /// <summary>
    /// Controller class for tasks pertaining to the authenticated user.
    /// </summary>
    public class Account : BaseController
    {
        /// <summary>
        /// The authenticated user's data.
        /// </summary>
        public User Me
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

        public PrivateMessages Messages
        {
            get
            {
                return messages ?? new PrivateMessages(Dispatch);
            }
            set
            {
                messages = value;
            }
        }
        private PrivateMessages messages;

        public Modmail Modmail
        {
            get
            {
                return modmail ?? new Modmail(Dispatch);
            }
            set
            {
                modmail = value;
            }
        }
        private Modmail modmail;

        public Dispatch Dispatch;

        /// <summary>
        /// Creates a new Account instance.  Note that this is already taken care of in the main class.
        /// </summary>
        /// <param name="dispatch"></param>
        public Account(Dispatch dispatch)
        {
            Dispatch = dispatch;
        }

        /// <summary>
        /// Returns a User instance with the data returned from a call to the "me" endpoint.
        /// </summary>
        public User GetMe()
        {
            Me = new User(Dispatch, Dispatch.Account.Me());
            return Me;
        }

        /// <summary>
        /// Return a breakdown of subreddit karma.
        /// </summary>
        /// <returns>A breakdown of subreddit karma.</returns>
        public List<UserKarma> Karma()
        {
            return ((UserKarmaContainer)Validate(Dispatch.Account.Karma())).Data;
        }

        /// <summary>
        /// Return the preference settings of the logged in user.
        /// </summary>
        /// <returns>The preference settings of the logged in user.</returns>
        public AccountPrefs Prefs()
        {
            return Validate(Dispatch.Account.Prefs());
        }

        /// <summary>
        /// Update preferences.
        /// </summary>
        /// <param name="accountPrefs">A valid AccountPrefs instance.</param>
        /// <returns>The updated preference settings of the logged in user.</returns>
        public AccountPrefs UpdatePrefs(AccountPrefsSubmit accountPrefs)
        {
            return Validate(Dispatch.Account.UpdatePrefs(accountPrefs));
        }

        /// <summary>
        /// Update preferences asynchronously.
        /// </summary>
        /// <param name="accountPrefs">A valid AccountPrefs instance.</param>
        public async Task<AccountPrefs> UpdatePrefsAsync(AccountPrefsSubmit accountPrefs)
        {
            AccountPrefs res = await Dispatch.Account.UpdatePrefsAsync(accountPrefs);
            return res;
        }

        /// <summary>
        /// Return a list of trophies for the current user.
        /// </summary>
        /// <returns>A list of trophies for the current user.</returns>
        public List<Award> Trophies()
        {
            TrophyList trophyList = Dispatch.Account.Trophies();
            if (trophyList == null || trophyList.Data == null || trophyList.Data.Trophies == null)
            {
                return null;
            }

            List<Award> res = new List<Award>();
            foreach (AwardContainer awardContainer in trophyList.Data.Trophies)
            {
                res.Add(awardContainer.Data);
            }

            return res;
        }

        /// <summary>
        /// Get users whom the current user has friended.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of users.</returns>
        public List<UserPrefs> Friends(int limit = 25, string after = "", string before = "", string show = "all", bool srDetail = false,
            bool includeCategories = false, int count = 0)
        {
            List<UserPrefs> res = new List<UserPrefs>();
            foreach (UserPrefsContainer userPrefsContainer in Validate(Dispatch.Account.PrefsList("friends", new CategorizedSrListingInput(after, before, count, limit,
                show, srDetail, includeCategories))))
            {
                res.AddRange(userPrefsContainer.Data.Children);
            }

            return res;
        }

        /// <summary>
        /// Get users with whom the current user is messaging.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of users.</returns>
        public List<UserPrefs> Messaging(int limit = 25, string after = "", string before = "", string show = "all", bool srDetail = false,
            bool includeCategories = false, int count = 0)
        {
            List<UserPrefs> res = new List<UserPrefs>();
            foreach (UserPrefsContainer userPrefsContainer in Validate(Dispatch.Account.PrefsList("messaging", new CategorizedSrListingInput(after, before, count, limit,
                show, srDetail, includeCategories))))
            {
                res.AddRange(userPrefsContainer.Data.Children);
            }

            return res;
        }

        /// <summary>
        /// Get users whom the current user has blocked.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of users.</returns>
        public List<UserPrefs> Blocked(int limit = 25, string after = "", string before = "", string show = "all", bool srDetail = false,
            bool includeCategories = false, int count = 0)
        {
            return Validate(Dispatch.Account.PrefsSingle("blocked", new CategorizedSrListingInput(after, before, count, limit, show, srDetail, includeCategories))).Data.Children;
        }

        /// <summary>
        /// Get users whom the current user has trusted.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of users.</returns>
        public List<UserPrefs> Trusted(int limit = 25, string after = "", string before = "", string show = "all", bool srDetail = false,
            bool includeCategories = false, int count = 0)
        {
            return Validate(Dispatch.Account.PrefsSingle("trusted", new CategorizedSrListingInput(after, before, count, limit, show, srDetail, includeCategories))).Data.Children;
        }

        /// <summary>
        /// Stop being friends with a user.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        public void DeleteFriend(string username)
        {
            Dispatch.Users.DeleteFriend(username);
        }

        /// <summary>
        /// Asynchronously stop being friends with a user.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        public async Task DeleteFriendAsync(string username)
        {
            await Dispatch.Users.DeleteFriendAsync(username);
        }

        /// <summary>
        /// Get information about a specific 'friend', such as notes.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <returns>An object containing basic info on the target user and the datetime of this action.</returns>
        public UserActionResult GetFriend(string username)
        {
            return Validate(Dispatch.Users.GetFriend(username));
        }

        /// <summary>
        /// Create or update a "friend" relationship.
        /// This operation is idempotent. It can be used to add a new friend, or update an existing friend (e.g., add/change the note on that friend).
        /// The JSON fields can only be included if you have a Reddit Gold subscription, for some reason.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <param name="json">{
        /// "name": A valid, existing reddit username
        /// "note": a string no longer than 300 characters
        /// }</param>
        /// <returns>An object containing basic info on the target user and the datetime of this action.</returns>
        public UserActionResult UpdateFriend(string username, string json = "{}")
        {
            return Validate(Dispatch.Users.UpdateFriend(username, json));
        }

        /// <summary>
        /// Create or update a "friend" relationship asynchronously.
        /// This operation is idempotent. It can be used to add a new friend, or update an existing friend (e.g., add/change the note on that friend).
        /// The JSON fields can only be included if you have a Reddit Gold subscription, for some reason.
        /// </summary>
        /// <param name="username">A valid, existing reddit username</param>
        /// <param name="json">{
        /// "name": A valid, existing reddit username
        /// "note": a string no longer than 300 characters
        /// }</param>
        public async Task UpdateFriendAsync(string username, string json = "{}")
        {
            await Validate(Dispatch.Users.UpdateFriendAsync(username, json));
        }

        /// <summary>
        /// Fetch a list of multis belonging to the current user.
        /// </summary>
        /// <param name="expandSrs">boolean value</param>
        /// <returns>A list of multis.</returns>
        public List<LabeledMulti> Multis(bool expandSrs = false)
        {
            List<LabeledMultiContainer> labeledMultiContainers = Dispatch.Multis.Mine(expandSrs);

            List<LabeledMulti> res = new List<LabeledMulti>();
            if (labeledMultiContainers != null)
            {
                foreach (LabeledMultiContainer labeledMultiContainer in labeledMultiContainers)
                {
                    res.Add(labeledMultiContainer.Data);
                }
            }

            return res;
        }

        /// <summary>
        /// Get subreddits that the current user is subscribed to.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of Subreddit objects.</returns>
        public List<Subreddit> MySubscribedSubreddits(int limit = 25, string after = "", string before = "", string show = "all",
            bool srDetail = true, bool includeCategories = false, int count = 0)
        {
            return Lists.GetSubreddits(Dispatch.Subreddits.Mine("subscriber", new CategorizedSrListingInput(after, before, count, limit, show, srDetail, includeCategories)), Dispatch);
        }

        /// <summary>
        /// Get subreddits that the current user is an approved submitter in.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of Subreddit objects.</returns>
        public List<Subreddit> MyContributingSubreddits(int limit = 25, string after = "", string before = "", string show = "all",
            bool srDetail = true, bool includeCategories = false, int count = 0)
        {
            return Lists.GetSubreddits(Dispatch.Subreddits.Mine("contributor", new CategorizedSrListingInput(after, before, count, limit, show, srDetail, includeCategories)), Dispatch);
        }

        /// <summary>
        /// Get subreddits that the current user is a moderator of.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of Subreddit objects.</returns>
        public List<Subreddit> MyModeratorSubreddits(int limit = 25, string after = "", string before = "", string show = "all",
            bool srDetail = true, bool includeCategories = false, int count = 0)
        {
            return Lists.GetSubreddits(Dispatch.Subreddits.Mine("moderator", new CategorizedSrListingInput(after, before, count, limit, show, srDetail, includeCategories)), Dispatch);
        }

        /// <summary>
        /// Get subreddits that the current user is subscribed to that contain hosted video links.
        /// </summary>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of Subreddit objects.</returns>
        public List<Subreddit> MyStreamingSubreddits(int limit = 25, string after = "", string before = "", string show = "all",
            bool srDetail = true, bool includeCategories = false, int count = 0)
        {
            return Lists.GetSubreddits(Dispatch.Subreddits.Mine("streams", new CategorizedSrListingInput(after, before, count, limit, show, srDetail, includeCategories)), Dispatch);
        }

        /// <summary>
        /// Returns a list of srs that the user moderates that are also enrolled in the new modmail.
        /// </summary>
        /// <returns>A list of subreddits.</returns>
        public Dictionary<string, ModmailSubreddit> ModmailSubreddits()
        {
            return ((ModmailSubredditContainer)Validate(Dispatch.Modmail.Subreddits())).Subreddits;
        }

        /// <summary>
        /// Endpoint to retrieve the unread conversation count by conversation state.
        /// </summary>
        /// <returns>An object with the int properties: highlighted, notifications, archived, new, inprogress, and mod.</returns>
        public ModmailUnreadCount ModmailUnreadCount()
        {
            return Validate(Dispatch.Modmail.UnreadCount());
        }

        /// <summary>
        /// Accept a pending invitation to contribute to a live thread.
        /// </summary>
        /// <param name="thread">id</param>
        public void AcceptLiveThreadInvite(string thread)
        {
            Validate(Dispatch.LiveThreads.AcceptContributorInvite(thread));
        }

        /// <summary>
        /// Asynchronously accept a pending invitation to contribute to a live thread.
        /// </summary>
        /// <param name="thread">id</param>
        public async Task AcceptLiveThreadInviteAsync(string thread)
        {
            await Validate(Dispatch.LiveThreads.AcceptContributorInviteAsync(thread));
        }

        /// <summary>
        /// Abdicate contributorship of the thread.
        /// </summary>
        /// <param name="thread">id</param>
        public void AbandonLiveThread(string thread)
        {
            Validate(Dispatch.LiveThreads.LeaveContributor(thread));
        }

        /// <summary>
        /// Abdicate contributorship of the thread asynchronously.
        /// </summary>
        /// <param name="thread">id</param>
        public async Task AbandonLiveThreadAsync(string thread)
        {
            await Validate(Dispatch.LiveThreads.LeaveContributorAsync(thread));
        }
    }
}
