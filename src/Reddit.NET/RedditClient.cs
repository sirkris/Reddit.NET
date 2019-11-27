using Reddit.Controllers;
using Reddit.Inputs;
using Reddit.Inputs.Search;
using Reddit.Inputs.Subreddits;
using RestSharp;
using System;
using System.Collections.Generic;

/// <summary>
/// A Reddit API library for .NET Standard with OAuth support.
/// </summary>
namespace Reddit
{
    /// <summary>
    /// The main Reddit API class.
    /// </summary>
    public class RedditClient
    {
        /// <summary>
        /// Endpoint wrapper classes/methods.
        /// </summary>
        public Dispatch Models;

        /// <summary>
        /// Data/methods pertaining to the authenticated user.
        /// </summary>
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

        /// <summary>
        /// Create a new instance of the Reddit.NET API library.
        /// This instance will be bound to a single Reddit user.
        /// </summary>
        /// <param name="appId">The OAuth application ID</param>
        /// <param name="refreshToken">The OAuth refresh token for the user we wish to authenticate</param>
        /// <param name="appSecret">The OAuth application secret; this parameter is required for 'script' apps which use a secret to authenticate</param>
        /// <param name="accessToken">(optional) An OAuth access token; if not provided, one will be automatically obtained using the refresh token</param>
        /// <param name="userAgent">(optional) A custom string for the User-Agent header</param>
        public RedditClient(string appId = null, string refreshToken = null, string appSecret = null, string accessToken = null, string userAgent = null)
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
                Models = new Dispatch(appId, appSecret, refreshToken, (!string.IsNullOrWhiteSpace(accessToken) ? accessToken : "null"), new RestClient("https://oauth.reddit.com"), 
                    userAgent: userAgent);
            }
            else
            {
                // App-only authentication.  --Kris
                Models = new Dispatch(appId, appSecret, null, null, new RestClient("https://oauth.reddit.com"), GenerateDeviceId(), userAgent);
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
            Account = new Account(Models);
            return Account;
        }

        /// <summary>
        /// Wait until the requests queue is either empty or down to the specified number of remaining requests.
        /// </summary>
        /// <param name="waitUntilRequestsAt">The wait ends when the number of requests count goes down to less than or equal to this value</param>
        public void WaitForRequestQueue(int waitUntilRequestsAt = 0)
        {
            while (!Models.Account.RequestReady((waitUntilRequestsAt + 1)))
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Create a new comment controller instance, populated only with its fullname.
        /// </summary>
        /// <param name="name">fullname of a thing</param>
        /// <returns>A new comment controller instance.</returns>
        public Comment Comment(string name)
        {
            return new Comment(Models, name);
        }

        /// <summary>
        /// Create a new link post controller instance, populated only with its fullname.
        /// </summary>
        /// <param name="name">fullname of a thing</param>
        /// <returns>A new link post controller instance.</returns>
        public LinkPost LinkPost(string name)
        {
            return new LinkPost(Models, name);
        }

        /// <summary>
        /// Create a new self post controller instance, populated only with its fullname.
        /// </summary>
        /// <param name="name">fullname of a thing</param>
        /// <returns>A new self post controller instance.</returns>
        public SelfPost SelfPost(string name)
        {
            return new SelfPost(Models, name);
        }

        /// <summary>
        /// Create a new post controller instance, populated only with its fullname.
        /// </summary>
        /// <param name="name">fullname of a thing</param>
        /// <returns>A new post controller instance.</returns>
        public Post Post(string name)
        {
            return new Post(Models, name);
        }

        /// <summary>
        /// Create a new live thread controller instance from API return data.
        /// </summary>
        /// <param name="liveUpdateEvent"></param>
        /// <returns>A new live thread controller instance.</returns>
        public LiveThread LiveThread(Things.LiveUpdateEvent liveUpdateEvent)
        {
            return new LiveThread(Models, liveUpdateEvent);
        }

        /// <summary>
        /// Create a copy of an existing live thread controller instance.
        /// </summary>
        /// <param name="liveThread">A valid live thread controller instance</param>
        /// <returns>A new live thread controller instance.</returns>
        public LiveThread LiveThread(LiveThread liveThread)
        {
            return new LiveThread(Models, liveThread);
        }

        /// <summary>
        /// Create a new live thread controller instance, populated manually.
        /// </summary>
        /// <param name="title">Title of the live thread</param>
        /// <param name="description">Description of the live thread</param>
        /// <param name="nsfw">Whether the live thread is NSFW</param>
        /// <param name="resources"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="websocketUrl"></param>
        /// <param name="announcementUrl"></param>
        /// <param name="state"></param>
        /// <param name="icon"></param>
        /// <param name="totalViews"></param>
        /// <param name="viewerCount"></param>
        /// <param name="created"></param>
        /// <returns>A new live thread controller instance.</returns>
        public LiveThread LiveThread(string title = null, string description = null, bool nsfw = false, string resources = null,
            string id = null, string name = null, string websocketUrl = null, string announcementUrl = null, string state = null,
            string icon = null, int? totalViews = null, int viewerCount = 0, DateTime created = default(DateTime))
        {
            return new LiveThread(Models, title, description, nsfw, resources, id, name, websocketUrl, announcementUrl, state,
                icon, totalViews, viewerCount, created);
        }

        /// <summary>
        /// Create a new live thread controller instance, populated only with its ID.
        /// </summary>
        /// <param name="id">A valid live thread ID</param>
        /// <returns>A new live thread controller instance.</returns>
        public LiveThread LiveThread(string id)
        {
            return new LiveThread(Models, id);
        }

        /// <summary>
        /// Create a new user controller instance from API return data.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>A new user controller instance.</returns>
        public User User(Things.User user)
        {
            return new User(Models, user);
        }

        /// <summary>
        /// Create a copy of an existing user controller instance.
        /// </summary>
        /// <param name="user">A valid user controller instance.</param>
        /// <returns>A new user controller instance.</returns>
        public User User(User user)
        {
            return new User(Models, user);
        }

        /// <summary>
        /// Create a new user controller instance, populated manually.
        /// </summary>
        /// <param name="name">A valid Reddit username</param>
        /// <param name="id"></param>
        /// <param name="isFriend"></param>
        /// <param name="profanityFilter"></param>
        /// <param name="isSuspended"></param>
        /// <param name="hasGoldSubscription"></param>
        /// <param name="numFriends"></param>
        /// <param name="IsVerified"></param>
        /// <param name="hasNewModmail"></param>
        /// <param name="over18"></param>
        /// <param name="isGold"></param>
        /// <param name="isMod"></param>
        /// <param name="hasVerifiedEmail"></param>
        /// <param name="iconImg"></param>
        /// <param name="hasModmail"></param>
        /// <param name="linkKarma"></param>
        /// <param name="inboxCount"></param>
        /// <param name="hasMail"></param>
        /// <param name="created"></param>
        /// <param name="commentKarma"></param>
        /// <param name="hasSubscribed"></param>
        /// <returns>A new user controller instance.</returns>
        public User User(string name, string id = null, bool isFriend = false, bool profanityFilter = false, bool isSuspended = false,
            bool hasGoldSubscription = false, int numFriends = 0, bool IsVerified = false, bool hasNewModmail = false, bool over18 = false,
            bool isGold = false, bool isMod = false, bool hasVerifiedEmail = false, string iconImg = null, bool hasModmail = false, int linkKarma = 0, int inboxCount = 0,
            bool hasMail = false, DateTime created = default(DateTime), int commentKarma = 0, bool hasSubscribed = false)
        {
            return new User(Models, name, id, isFriend, profanityFilter, isSuspended, hasGoldSubscription, numFriends, IsVerified, hasNewModmail, over18, isGold, isMod,
                hasVerifiedEmail, iconImg, hasModmail, linkKarma, inboxCount, hasMail, created, commentKarma, hasSubscribed);
        }

        /// <summary>
        /// Create an empty user controller instance.
        /// </summary>
        /// <returns>A new user controller instance.</returns>
        public User User()
        {
            return new User(Models);
        }

        /// <summary>
        /// Create a copy of an existing subreddit controller instance.
        /// </summary>
        /// <param name="subreddit">A valid subreddit controller instance</param>
        /// <returns>A new subreddit controller instance.</returns>
        public Subreddit Subreddit(Subreddit subreddit)
        {
            return new Subreddit(Models, subreddit);
        }

        /// <summary>
        /// Create a new subreddit instance from API return data.
        /// </summary>
        /// <param name="subreddit"></param>
        /// <returns>A new subreddit controller instance.</returns>
        public Subreddit Subreddit(Things.Subreddit subreddit)
        {
            return new Subreddit(Models, subreddit);
        }

        /// <summary>
        /// Create a new subreddit instance from API return data.
        /// </summary>
        /// <param name="subredditChild"></param>
        /// <returns>A new subreddit controller instance.</returns>
        public Subreddit Subreddit(Things.SubredditChild subredditChild)
        {
            return new Subreddit(Models, subredditChild);
        }

        /// <summary>
        /// Create a new subreddit controller instance, populated manually.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="sidebar"></param>
        /// <param name="submissionText"></param>
        /// <param name="lang"></param>
        /// <param name="subredditType"></param>
        /// <param name="submissionType"></param>
        /// <param name="submitLinkLabel"></param>
        /// <param name="submitTextLabel"></param>
        /// <param name="wikiEnabled"></param>
        /// <param name="over18"></param>
        /// <param name="allowDiscovery"></param>
        /// <param name="allowSpoilers"></param>
        /// <param name="showMedia"></param>
        /// <param name="showMediaPreview"></param>
        /// <param name="allowImages"></param>
        /// <param name="allowVideos"></param>
        /// <param name="collapseDeletedComments"></param>
        /// <param name="suggestedCommentSort"></param>
        /// <param name="commentScoreHideMins"></param>
        /// <param name="headerImage"></param>
        /// <param name="iconImage"></param>
        /// <param name="primaryColor"></param>
        /// <param name="keyColor"></param>
        /// <param name="fullname"></param>
        /// <returns>A new subreddit controller instance.</returns>
        public Subreddit Subreddit(string name, string title = "", string description = "", string sidebar = "",
            string submissionText = null, string lang = "en", string subredditType = "public", string submissionType = "any",
            string submitLinkLabel = null, string submitTextLabel = null, bool wikiEnabled = false, bool over18 = false,
            bool allowDiscovery = true, bool allowSpoilers = true, bool showMedia = true, bool showMediaPreview = true,
            bool allowImages = true, bool allowVideos = true, bool collapseDeletedComments = false, string suggestedCommentSort = null,
            int commentScoreHideMins = 0, byte[] headerImage = null, byte[] iconImage = null, string primaryColor = null, string keyColor = null, 
            string fullname = null)
        {
            return new Subreddit(Models, name, title, description, sidebar, submissionText, lang, subredditType, submissionType, submitLinkLabel, submitTextLabel,
                wikiEnabled, over18, allowDiscovery, allowSpoilers, showMedia, showMediaPreview, allowImages, allowVideos, collapseDeletedComments,
                suggestedCommentSort, commentScoreHideMins, headerImage, iconImage, primaryColor, keyColor, fullname);
        }

        /// <summary>
        /// Create an empty subreddit controller instance.
        /// </summary>
        /// <returns>A new subreddit controller instance.</returns>
        public Subreddit Subreddit()
        {
            return new Subreddit(Models);
        }

        /// <summary>
        /// Get a listing of posts by fullname.
        /// </summary>
        /// <param name="fullnames">A list of post fullnames</param>
        /// <returns>A list of populated posts.</returns>
        public List<Post> GetPosts(List<string> fullnames)
        {
            return Account.Lists.GetPosts(Account.Validate(Models.Listings.GetByNames(string.Join(",", fullnames))), Models);
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
        public Dictionary<string, Things.Scope> Scopes(string scopes = null)
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
            return ((Things.SubredditNames)Account.Validate(Models.Subreddits.SearchRedditNames(new SubredditsSearchNamesInput(query, exact, includeOver18, includeUnadvertisable)))).Names;
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
        public List<Things.SubSearchResult> SearchSubredditNames(string query, bool exact = false, bool includeOver18 = true, bool includeUnadvertisable = true)
        {
            return ((Things.SubSearch)Account.Validate(Models.Subreddits.SearchSubreddits(new SubredditsSearchNamesInput(query, exact, includeOver18, includeUnadvertisable)))).Subreddits;
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
            return Account.Lists.GetSubreddits(Account.Validate(Models.Subreddits.Search(
                new SubredditsSearchInput(query, showUsers, after, before, sort, count, limit, show, srDetail))), Models);
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
        public List<Things.SubredditAutocompleteResult> SubredditAutocomplete(string query, bool includeOver18 = true, bool includeProfiles = true)
        {
            return ((Things.SubredditAutocompleteResultContainer)Account.Validate(Models.Subreddits.SubredditAutocomplete(
                new SubredditsAutocompleteInput(query, includeProfiles, includeOver18)))).Subreddits;
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
            return Account.Lists.GetSubreddits(Account.Validate(Models.Subreddits.SubredditAutocompleteV2(
                new SubredditsAutocompleteV2Input(query, includeCategories, includeOver18, includeProfiles, limit))), Models);
        }

        /// <summary>
        /// Search Reddit for matching users.
        /// </summary>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance</param>
        /// <returns>A list of users that match the search criteria.</returns>
        public List<Subreddit> SearchSubreddits(SearchGetSearchInput searchGetSearchInput)
        {
            searchGetSearchInput.restrict_sr = false;
            return Account.Lists.GetSubreddits(Account.Validate(Models.Search.SearchSubreddits(searchGetSearchInput)), Models);
        }

        /// <summary>
        /// Search Reddit for matching users.
        /// </summary>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance</param>
        /// <returns>A list of users that match the search criteria.</returns>
        public List<User> SearchUsers(SearchGetSearchInput searchGetSearchInput)
        {
            searchGetSearchInput.restrict_sr = false;
            return Account.Lists.GetUsers(Account.Validate(Models.Search.SearchUsers(searchGetSearchInput)), Models);
        }

        /// <summary>
        /// Search Reddit for matching things.
        /// This method can return links, subreddits, and/or users.  To include all of them, set type to "link,sr,user".
        /// </summary>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance</param>
        /// <returns>A list of things that match the search criteria.</returns>
        public Things.MixedListingContainer MixedSearch(SearchGetSearchInput searchGetSearchInput)
        {
            return Account.Validate(Models.Search.MultiSearch(searchGetSearchInput));
        }

        /// <summary>
        /// Search all subreddits for posts.
        /// To search a specific subreddit for posts, use the Subreddit controller.
        /// </summary>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance</param>
        /// <returns>A list of posts that match the search criteria.</returns>
        public List<Post> Search(SearchGetSearchInput searchGetSearchInput)
        {
            searchGetSearchInput.restrict_sr = false;
            return Account.Lists.GetPosts(Account.Validate(Models.Search.SearchPosts(searchGetSearchInput)), Models);
        }

        /// <summary>
        /// Search all subreddits for posts.
        /// To search a specific subreddit for posts, use the Subreddit controller.
        /// </summary>
        /// <param name="q">A valid search query</param>
        /// <param name="searchGetSearchInput">A valid SearchGetSearchInput instance (optional)</param>
        /// <returns>A list of posts that match the search criteria.</returns>
        public List<Post> Search(string q, SearchGetSearchInput searchGetSearchInput = null)
        {
            if (searchGetSearchInput == null)
            {
                searchGetSearchInput = new SearchGetSearchInput();
            }

            searchGetSearchInput.q = q;
            searchGetSearchInput.restrict_sr = false;

            return Search(searchGetSearchInput);
        }

        /// <summary>
        /// Search all subreddits for posts.
        /// To search a specific subreddit for posts, use the Subreddit controller.
        /// </summary>
        /// <param name="q">a string no longer than 512 characters</param>
        /// <param name="restrictSr">boolean value</param>
        /// <param name="sort">one of (relevance, hot, top, new, comments)</param>
        /// <param name="category">a string no longer than 5 characters</param>
        /// <param name="includeFacets">boolean value</param>
        /// <param name="type">(optional) comma-delimited list of result types (sr, link, user)</param>
        /// <param name="t">one of (hour, day, week, month, year, all)</param>
        /// <param name="after">fullname of a thing</param>
        /// <param name="before">fullname of a thing</param>
        /// <param name="includeCategories">boolean value</param>
        /// <param name="count">a positive integer (default: 0)</param>
        /// <param name="limit">the maximum number of items desired (default: 25, maximum: 100)</param>
        /// <param name="show">(optional) the string all</param>
        /// <param name="srDetail">boolean value</param>
        /// <returns>A list of posts that match the search criteria.</returns>
        public List<Post> Search(string q = "", string sort = "new", string category = "", bool includeFacets = false, string type = null,
            string t = "all", string after = null, string before = null, bool includeCategories = false, int count = 0, int limit = 25,
            string show = "all", bool srDetail = false)
        {
            return Account.Lists.GetPosts(Account.Validate(Models.Search.SearchPosts(
                new SearchGetSearchInput(q, false, sort, category, includeFacets, type, t, after, before,
                    includeCategories, count, limit, show, srDetail))), Models);
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
            return Account.Lists.GetSubreddits(Account.Validate(Models.Subreddits.Get(where, 
                new CategorizedSrListingInput(after, before, count, limit, show, srDetail, includeCategories))), Models);
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
            return Account.Lists.GetSubreddits(Account.Validate(Models.Subreddits.GetUserSubreddits(where, 
                new CategorizedSrListingInput(after, before, count, limit, show, srDetail, includeCategories))), Models);
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

    /// <summary>
    /// (deprecated) The old name for the main Reddit API class.
    /// </summary>
    [Obsolete("The main class has been renamed.  Please use " + nameof(RedditClient) + " instead.")]
    public class RedditAPI : RedditClient
    {
        /// <summary>
        /// (deprecated) Create a new instance of the Reddit.NET API library.
        /// This instance will be bound to a single Reddit user.
        /// </summary>
        /// <param name="appId">The OAuth application ID</param>
        /// <param name="refreshToken">The OAuth refresh token for the user we wish to authenticate</param>
        /// <param name="appSecret">The OAuth application secret; this parameter is required for 'script' apps which use a secret to authenticate</param>
        /// <param name="accessToken">(optional) An OAuth access token; if not provided, one will be automatically obtained using the refresh token</param>
        /// <param name="userAgent">(optional) A custom string for the User-Agent header</param>
        public RedditAPI(string appId = null, string refreshToken = null, string appSecret = null, string accessToken = null, string userAgent = null)
            : base(appId, refreshToken, appSecret, accessToken, userAgent) { }
    }
}
