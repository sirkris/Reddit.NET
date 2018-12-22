using Reddit.NET.Controllers;
using Reddit.NET.Controllers.Structures;
using RedditThings = Reddit.NET.Models.Structures;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Reddit.NET
{
    public class RedditAPI
    {
        public Dispatch Models;

        public Account Account
        {
            get
            {
                return account ?? GetAccount();
            }
            private set
            {
                account = value;
            }
        }
        private Account account;

        public RedditAPI(string appId, string refreshToken = null, string accessToken = null)
        {
            /*
             * If refreshToken is supplied, the lib will automatically request a new access token when the current one expires (or if none was passed).
             * Otherwise it's left up to the calling app and the API calls will fail once the access token expires.
             * 
             * --Kris
             */
            if (!string.IsNullOrWhiteSpace(refreshToken)
                || !string.IsNullOrWhiteSpace(accessToken))
            {
                // Passing "null" instead of null forces the Reddit API to return a non-200 status code on auth failure, freeing us from having to parse the content string.  --Kris
                Models = new Dispatch(appId, refreshToken, (!string.IsNullOrWhiteSpace(accessToken) ? accessToken : "null"), new RestClient("https://oauth.reddit.com"));
            }
            else
            {
                // App-only authentication.  --Kris
                Models = new Dispatch(appId, null, null, new RestClient("https://oauth.reddit.com"), GenerateDeviceId());
            }
        }

        /// <summary>
        /// Generates a unique Device ID required for app-only authentication.
        /// </summary>
        /// <returns>A random alphanumeric string of 30 characters.</returns>
        private string GenerateDeviceId()
        {
            string salt = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            Random rand = new Random();

            char[] res = new char[30];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = salt[rand.Next(res.Length)];
            }

            return new string(res);
        }

        private Account GetAccount()
        {
            Account = new Account(ref Models);
            return Account;
        }

        /// <summary>
        /// Wait until the requests queue is either empty or down to the specified number of remaining requests.
        /// </summary>
        /// <param name="waitUntilRequestsAt">The wait ends when the number of requests count goes down to less than or equal to this value</param>
        public void WaitForRequestQueue(int waitUntilRequestsAt = 0)
        {
            while (!Models.Account.RequestReady((waitUntilRequestsAt + 1))) { }
        }

        public Comment Comment(string name)
        {
            return new Comment(ref Models, name);
        }

        public LinkPost LinkPost(string name)
        {
            return new LinkPost(ref Models, name);
        }

        public SelfPost SelfPost(string name)
        {
            return new SelfPost(ref Models, name);
        }

        public Post Post(string name)
        {
            return new Post(ref Models, name);
        }

        public LiveThread LiveThread(RedditThings.LiveUpdateEvent liveUpdateEvent)
        {
            return new LiveThread(ref Models, liveUpdateEvent);
        }

        public LiveThread LiveThread(LiveThread liveThread)
        {
            return new LiveThread(ref Models, liveThread);
        }

        public LiveThread LiveThread(string title = null, string description = null, bool nsfw = false, string resources = null,
            string id = null, string name = null, string websocketUrl = null, string announcementUrl = null, string state = null,
            string icon = null, int? totalViews = null, int viewerCount = 0, DateTime created = default(DateTime))
        {
            return new LiveThread(ref Models, title, description, nsfw, resources, id, name, websocketUrl, announcementUrl, state,
                icon, totalViews, viewerCount, created);
        }

        public LiveThread LiveThread(string id)
        {
            return new LiveThread(ref Models, id);
        }

        public User User(RedditThings.User user)
        {
            return new User(ref Models, user);
        }

        public User User(User user)
        {
            return new User(ref Models, user);
        }

        public User User(string name, string id = null, bool isFriend = false, bool profanityFilter = false, bool isSuspended = false,
            bool hasGoldSubscription = false, int numFriends = 0, bool IsVerified = false, bool hasNewModmail = false, bool over18 = false,
            bool isGold = false, bool isMod = false, bool hasVerifiedEmail = false, string iconImg = null, bool hasModmail = false, int linkKarma = 0, int inboxCount = 0,
            bool hasMail = false, DateTime created = default(DateTime), int commentKarma = 0, bool hasSubscribed = false)
        {
            return new User(ref Models, name, id, isFriend, profanityFilter, isSuspended, hasGoldSubscription, numFriends, IsVerified, hasNewModmail, over18, isGold, isMod,
                hasVerifiedEmail, iconImg, hasModmail, linkKarma, inboxCount, hasMail, created, commentKarma, hasSubscribed);
        }

        public User User()
        {
            return new User(ref Models);
        }

        public Subreddit Subreddit(Subreddit subreddit)
        {
            return new Subreddit(ref Models, subreddit);
        }

        public Subreddit Subreddit(RedditThings.Subreddit subreddit)
        {
            return new Subreddit(ref Models, subreddit);
        }

        public Subreddit Subreddit(RedditThings.SubredditChild subredditChild)
        {
            return new Subreddit(ref Models, subredditChild);
        }

        public Subreddit Subreddit(string name, string title = "", string description = "", string sidebar = "",
            string submissionText = null, string lang = "en", string subredditType = "public", string submissionType = "any",
            string submitLinkLabel = null, string submitTextLabel = null, bool wikiEnabled = false, bool over18 = false,
            bool allowDiscovery = true, bool allowSpoilers = true, bool showMedia = true, bool showMediaPreview = true,
            bool allowImages = true, bool allowVideos = true, bool collapseDeletedComments = false, string suggestedCommentSort = null,
            int commentScoreHideMins = 0, byte[] headerImage = null, byte[] iconImage = null, string primaryColor = null, string keyColor = null, 
            string fullname = null)
        {
            return new Subreddit(ref Models, name, title, description, sidebar, submissionText, lang, subredditType, submissionType, submitLinkLabel, submitTextLabel,
                wikiEnabled, over18, allowDiscovery, allowSpoilers, showMedia, showMediaPreview, allowImages, allowVideos, collapseDeletedComments,
                suggestedCommentSort, commentScoreHideMins, headerImage, iconImage, primaryColor, keyColor, fullname);
        }

        public Subreddit Subreddit()
        {
            return new Subreddit(ref Models);
        }

        /// <summary>
        /// Get a listing of posts by fullname.
        /// </summary>
        /// <param name="fullnames">A list of post fullnames</param>
        /// <returns>A list of populated posts.</returns>
        public List<Post> GetPosts(List<string> fullnames)
        {
            return Account.Listings.GetPosts(Account.Validate(Models.Listings.GetByNames(string.Join(",", fullnames))), Models);
        }

        /// <summary>
        /// Get a listing of posts by fullname.
        /// </summary>
        /// <param name="posts">A list of post objects with valid Fullnames</param>
        /// <returns>A list of populated posts.</returns>
        public List<Post> GetPosts(List<Post> posts)
        {
            List<string> fullnames = new List<string>();
            foreach (Post post in posts)
            {
                fullnames.Add(post.Fullname);
            }

            return GetPosts(fullnames);
        }

        /// <summary>
        /// Retrieve descriptions of reddit's OAuth2 scopes.
        /// If no scopes are given, information on all scopes are returned.
        /// Invalid scope(s) will result in a 400 error with body that indicates the invalid scope(s).
        /// </summary>
        /// <param name="scopes">(optional) An OAuth2 scope string</param>
        /// <returns>A list of scopes.</returns>
        public Dictionary<string, RedditThings.Scope> Scopes(string scopes = null)
        {
            return Account.Validate(Models.Misc.Scopes(scopes));
        }

        /// <summary>
        /// List subreddit names that begin with a query string.
        /// Subreddits whose names begin with query will be returned.
        /// If include_over_18 is false, subreddits with over-18 content restrictions will be filtered from the results.
        /// If include_unadvertisable is False, subreddits that have hide_ads set to True or are on the anti_ads_subreddits list will be filtered.
        /// If exact is true, only an exact match will be returned. Exact matches are inclusive of over_18 subreddits, but not hide_ad subreddits when include_unadvertisable is False.
        /// </summary>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <param name="exact">boolean value</param>
        /// <param name="includeOver18">boolean value</param>
        /// <param name="includeUnadvertisable">boolean value</param>
        /// <returns>A list of subreddit names.</returns>
        public List<string> SearchRedditNames(string query, bool exact = false, bool includeOver18 = true, bool includeUnadvertisable = true)
        {
            return ((RedditThings.SubredditNames)Account.Validate(Models.Subreddits.SearchRedditNames(exact, includeOver18, includeUnadvertisable, query))).Names;
        }

        /// <summary>
        /// List subreddits that begin with a query string.
        /// Subreddits whose names begin with query will be returned.
        /// If include_over_18 is false, subreddits with over-18 content restrictions will be filtered from the results.
        /// If include_unadvertisable is False, subreddits that have hide_ads set to True or are on the anti_ads_subreddits list will be filtered.
        /// If exact is true, only an exact match will be returned.Exact matches are inclusive of over_18 subreddits, but not hide_ad subreddits when include_unadvertisable is False.
        /// </summary>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <param name="exact">boolean value</param>
        /// <param name="includeOver18">boolean value</param>
        /// <param name="includeUnadvertisable">boolean value</param>
        /// <returns>A list of subreddit listings.</returns>
        public List<RedditThings.SubSearchResult> SearchSubredditNames(string query, bool exact = false, bool includeOver18 = true, bool includeUnadvertisable = true)
        {
            return ((RedditThings.SubSearch)Account.Validate(Models.Subreddits.SearchSubreddits(exact, includeOver18, includeUnadvertisable, query))).Subreddits;
        }

        /// <summary>
        /// Search subreddits by title and description.
        /// </summary>
        /// <param name="query">a search query</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="showUsers">boolean value</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="sort">one of (relevance, activity)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>A list of subreddit objects.</returns>
        public List<Subreddit> SearchSubreddits(string query, int limit = 25, bool showUsers = false, string after = "", string before = "", string sort = "relevance",
            string show = "all", bool srDetail = false, int count = 0)
        {
            return Account.Listings.GetSubreddits(Account.Validate(Models.Subreddits.Search(after, before, query, showUsers, sort, count, limit, show, srDetail)), Models);
        }

        /// <summary>
        /// Return a list of subreddits and data for subreddits whose names start with 'query'.
        /// Uses typeahead endpoint to recieve the list of subreddits names. 
        /// Typeahead provides exact matches, typo correction, fuzzy matching and boosts subreddits to the top that the user is subscribed to.
        /// </summary>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <param name="includeOver18">boolean value</param>
        /// <param name="includeProfiles">boolean value</param>
        /// <returns>Matching subreddits.</returns>
        public List<RedditThings.SubredditAutocompleteResult> SubredditAutocomplete(string query, bool includeOver18 = true, bool includeProfiles = true)
        {
            return ((RedditThings.SubredditAutocompleteResultContainer)Account.Validate(Models.Subreddits.SubredditAutocomplete(includeOver18, includeProfiles, query))).Subreddits;
        }

        /// <summary>
        /// Version 2 of SubredditAutocomplete.
        /// </summary>
        /// <param name="query">a string up to 50 characters long, consisting of printable characters</param>
        /// <param name="includeOver18"boolean value></param>
        /// <param name="includeProfiles">boolean value</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="limit">an integer between 1 and 10 (default: 5)</param>
        /// <returns>Matching subreddits.</returns>
        public List<Subreddit> SubredditAutocompleteV2(string query, bool includeOver18 = true, bool includeProfiles = true, bool includeCategories = true, int limit = 5)
        {
            return Account.Listings.GetSubreddits(Account.Validate(Models.Subreddits.SubredditAutocompleteV2(includeCategories, includeOver18, includeProfiles, query, limit)), Models);
        }

        // TODO - Split this up and maybe create a new Subreddits controller for these?  --Kris
        /// <summary>
        /// Get all subreddits.
        /// The where parameter chooses the order in which the subreddits are displayed.
        /// popular sorts on the activity of the subreddit and the position of the subreddits can shift around.
        /// new sorts the subreddits based on their creation date, newest first.
        /// </summary>
        /// <param name="where">One of (popular, new, gold, default)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>List of subreddit objects.</returns>
        public List<Subreddit> GetSubreddits(string where, int limit = 25, string after = "", string before = "", bool includeCategories = false,
            string show = "all", bool srDetail = false, int count = 0)
        {
            return Account.Listings.GetSubreddits(Account.Validate(Models.Subreddits.Get(where, after, before, includeCategories, count, limit, show, srDetail)), Models);
        }

        // TODO - Split this up and maybe create a new Subreddits controller for these?  --Kris
        /// <summary>
        /// Get all subreddits.
        /// The where parameter chooses the order in which the subreddits are displayed.
        /// popular sorts on the activity of the subreddit and the position of the subreddits can shift around.
        /// new sorts the subreddits based on their creation date, newest first.
        /// </summary>
        /// <param name="where">One of (popular, new, gold, default)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">(optional) expand subreddits</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <returns>List of subreddit objects.</returns>
        public List<Subreddit> GetUserSubreddits(string where, int limit = 25, string after = "", string before = "", bool includeCategories = false,
            string show = "all", bool srDetail = false, int count = 0)
        {
            return Account.Listings.GetSubreddits(Account.Validate(Models.Subreddits.GetUserSubreddits(where, after, before, includeCategories, count, limit, show, srDetail)), Models);
        }

        /// <summary>
        /// Get user data by account IDs.
        /// </summary>
        /// <param name="fullnames">A list of account fullnames</param>
        /// <returns>A dictionary of user summary objects.</returns>
        public List<User> UserDataByAccountIds(List<string> fullnames)
        {
            return Account.Validate(Models.Users.UserDataByAccountIds(string.Join(",", fullnames)));
        }

        /// <summary>
        /// Get user data by account IDs.
        /// </summary>
        /// <param name="users">A list of user objects with valid Fullnames</param>
        /// <returns>A dictionary of user summary objects.</returns>
        public List<User> UserDataByAccountIds(List<User> users)
        {
            List<string> fullnames = new List<string>();
            foreach (User user in users)
            {
                fullnames.Add(user.Fullname);
            }

            return UserDataByAccountIds(fullnames);
        }
    }
}
