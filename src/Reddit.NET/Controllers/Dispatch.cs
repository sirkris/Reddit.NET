using API = Reddit.Models;
using Reddit.Models.EventArgs;
using RestSharp;

namespace Reddit.Controllers
{
    public class Dispatch
    {
        public API.Account Account
        {
            get
            {
                if (account == null)
                {
                    account = new API.Account(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    account.TokenUpdated += C_TokenUpdated;
                    account.RequestsUpdated += C_RequestsUpdated;
                }

                return account;
            }
            set
            {
                account = value;
            }
        }
        private API.Account account;

        public API.Emoji Emoji
        {
            get
            {
                if (emoji == null)
                {
                    emoji = new API.Emoji(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    emoji.TokenUpdated += C_TokenUpdated;
                    emoji.RequestsUpdated += C_RequestsUpdated;
                }

                return emoji;
            }
            set
            {
                emoji = value;
            }
        }
        private API.Emoji emoji;

        public API.Flair Flair
        {
            get
            {
                if (flair == null)
                {
                    flair = new API.Flair(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    flair.TokenUpdated += C_TokenUpdated;
                    flair.RequestsUpdated += C_RequestsUpdated;
                }

                return flair;
            }
            set
            {
                flair = value;
            }
        }
        private API.Flair flair;

        public API.LinksAndComments LinksAndComments
        {
            get
            {
                if (linksAndComments == null)
                {
                    linksAndComments = new API.LinksAndComments(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    linksAndComments.TokenUpdated += C_TokenUpdated;
                    linksAndComments.RequestsUpdated += C_RequestsUpdated;
                }

                return linksAndComments;
            }
            set
            {
                linksAndComments = value;
            }
        }
        private API.LinksAndComments linksAndComments;

        public API.Listings Listings
        {
            get
            {
                if (listings == null)
                {
                    listings = new API.Listings(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    listings.TokenUpdated += C_TokenUpdated;
                    listings.RequestsUpdated += C_RequestsUpdated;
                }

                return listings;
            }
            set
            {
                listings = value;
            }
        }
        private API.Listings listings;

        public API.LiveThreads LiveThreads
        {
            get
            {
                if (liveThreads == null)
                {
                    liveThreads = new API.LiveThreads(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    liveThreads.TokenUpdated += C_TokenUpdated;
                    liveThreads.RequestsUpdated += C_RequestsUpdated;
                }

                return liveThreads;
            }
            set
            {
                liveThreads = value;
            }
        }
        private API.LiveThreads liveThreads;

        public API.Misc Misc
        {
            get
            {
                if (misc == null)
                {
                    misc = new API.Misc(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    misc.TokenUpdated += C_TokenUpdated;
                    misc.RequestsUpdated += C_RequestsUpdated;
                }

                return misc;
            }
            set
            {
                misc = value;
            }
        }
        private API.Misc misc;

        public API.Moderation Moderation
        {
            get
            {
                if (moderation == null)
                {
                    moderation = new API.Moderation(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    moderation.TokenUpdated += C_TokenUpdated;
                    moderation.RequestsUpdated += C_RequestsUpdated;
                }

                return moderation;
            }
            set
            {
                moderation = value;
            }
        }
        private API.Moderation moderation;

        public API.Modmail Modmail
        {
            get
            {
                if (modmail == null)
                {
                    modmail = new API.Modmail(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    modmail.TokenUpdated += C_TokenUpdated;
                    modmail.RequestsUpdated += C_RequestsUpdated;
                }

                return modmail;
            }
            set
            {
                modmail = value;
            }
        }
        private API.Modmail modmail;

        public API.Multis Multis
        {
            get
            {
                if (multis == null)
                {
                    multis = new API.Multis(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    multis.TokenUpdated += C_TokenUpdated;
                    multis.RequestsUpdated += C_RequestsUpdated;
                }

                return multis;
            }
            set
            {
                multis = value;
            }
        }
        private API.Multis multis;

        public API.PrivateMessages PrivateMessages
        {
            get
            {
                if (privateMessages == null)
                {
                    privateMessages = new API.PrivateMessages(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    privateMessages.TokenUpdated += C_TokenUpdated;
                    privateMessages.RequestsUpdated += C_RequestsUpdated;
                }

                return privateMessages;
            }
            set
            {
                privateMessages = value;
            }
        }
        private API.PrivateMessages privateMessages;

        public API.RedditGold RedditGold
        {
            get
            {
                if (redditGold == null)
                {
                    redditGold = new API.RedditGold(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    redditGold.TokenUpdated += C_TokenUpdated;
                    redditGold.RequestsUpdated += C_RequestsUpdated;
                }

                return redditGold;
            }
            set
            {
                redditGold = value;
            }
        }
        private API.RedditGold redditGold;

        public API.Search Search
        {
            get
            {
                if (search == null)
                {
                    search = new API.Search(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    search.TokenUpdated += C_TokenUpdated;
                    search.RequestsUpdated += C_RequestsUpdated;
                }

                return search;
            }
            set
            {
                search = value;
            }
        }
        private API.Search search;

        public API.Subreddits Subreddits
        {
            get
            {
                if (subreddits == null)
                {
                    subreddits = new API.Subreddits(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    subreddits.TokenUpdated += C_TokenUpdated;
                    subreddits.RequestsUpdated += C_RequestsUpdated;
                }

                return subreddits;
            }
            set
            {
                subreddits = value;
            }
        }
        private API.Subreddits subreddits;

        public API.Users Users
        {
            get
            {
                if (users == null)
                {
                    users = new API.Users(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    users.TokenUpdated += C_TokenUpdated;
                    users.RequestsUpdated += C_RequestsUpdated;
                }

                return users;
            }
            set
            {
                users = value;
            }
        }
        private API.Users users;

        public API.Widgets Widgets
        {
            get
            {
                if (widgets == null)
                {
                    widgets = new API.Widgets(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    widgets.TokenUpdated += C_TokenUpdated;
                    widgets.RequestsUpdated += C_RequestsUpdated;
                }

                return widgets;
            }
            set
            {
                widgets = value;
            }
        }
        private API.Widgets widgets;

        public API.Wiki Wiki
        {
            get
            {
                if (wiki == null)
                {
                    wiki = new API.Wiki(AppID, AppSecret, RefreshToken, AccessToken, ref RestClient, DeviceID, UserAgent);

                    wiki.TokenUpdated += C_TokenUpdated;
                    wiki.RequestsUpdated += C_RequestsUpdated;
                }

                return wiki;
            }
            set
            {
                wiki = value;
            }
        }
        private API.Wiki wiki;

        /// <summary>
        /// Makes the Reddit OAuth credentials accessible to the calling app.
        /// Aside from populating these values for that reason, this class is not used by the library, itself.
        /// Each model class stores these credentials internally.
        /// </summary>
        public API.OAuthCredentials OAuthCredentials
        {
            get
            {
                if (oAuthCredentials == null)
                {
                    oAuthCredentials = new API.OAuthCredentials(AppID, AppSecret, RefreshToken, AccessToken, DeviceID);
                }

                return oAuthCredentials;
            }
            set
            {
                oAuthCredentials = value;
            }
        }
        private API.OAuthCredentials oAuthCredentials;

        internal API.Internal.Monitor Monitor
        {
            get
            {
                if (monitor == null)
                {
                    monitor = new API.Internal.Monitor();
                    monitor.MonitoringUpdated += C_MonitoringUpdated;
                }

                return monitor;
            }
            set
            {
                monitor = value;
            }
        }
        private API.Internal.Monitor monitor;

        private string AppID { get; set; }
        private string AppSecret { get; set; }
        private string RefreshToken { get; set; }
        private string AccessToken { get; set; }
        private RestClient RestClient;
        private string DeviceID { get; set; }
        private string UserAgent { get; set; }

        /// <summary>
        /// Instantiate endpoint singletons.
        /// </summary>
        /// <param name="appId">The OAuth application ID</param>
        /// <param name="appSecret">The OAuth application secret; this parameter is required for 'script' apps which use a secret to authenticate</param>
        /// <param name="refreshToken">The OAuth refresh token required to obtain a Reddit API access token</param>
        /// <param name="accessToken">The OAuth access token required to access the Reddit API</param>
        /// <param name="restClient">A valid RestClient instance</param>
        /// <param name="deviceId">(optional) A unique Device ID required for app-only authentication</param>
        /// <param name="userAgent">(optional) A custom string for the User-Agent header</param>
        public Dispatch(string appId, string appSecret, string refreshToken, string accessToken, RestClient restClient, string deviceId = null, string userAgent = null)
        {
            AppID = appId;
            AppSecret = appSecret;
            RefreshToken = refreshToken;
            AccessToken = accessToken;
            RestClient = restClient;
            DeviceID = deviceId;
            UserAgent = userAgent;
        }

        public void C_TokenUpdated(object sender, TokenUpdateEventArgs e)
        {
            Account.UpdateAccessToken(e.AccessToken);
            Emoji.UpdateAccessToken(e.AccessToken);
            Flair.UpdateAccessToken(e.AccessToken);
            LinksAndComments.UpdateAccessToken(e.AccessToken);
            Listings.UpdateAccessToken(e.AccessToken);
            LiveThreads.UpdateAccessToken(e.AccessToken);
            Misc.UpdateAccessToken(e.AccessToken);
            Moderation.UpdateAccessToken(e.AccessToken);
            Modmail.UpdateAccessToken(e.AccessToken);
            Multis.UpdateAccessToken(e.AccessToken);
            OAuthCredentials.UpdateAccessToken(e.AccessToken);
            PrivateMessages.UpdateAccessToken(e.AccessToken);
            RedditGold.UpdateAccessToken(e.AccessToken);
            Search.UpdateAccessToken(e.AccessToken);
            Subreddits.UpdateAccessToken(e.AccessToken);
            Users.UpdateAccessToken(e.AccessToken);
            Widgets.UpdateAccessToken(e.AccessToken);
            Wiki.UpdateAccessToken(e.AccessToken);
        }

        public void C_RequestsUpdated(object sender, RequestsUpdateEventArgs e)
        {
            Account.UpdateRequests(e.Requests);
            Emoji.UpdateRequests(e.Requests);
            Flair.UpdateRequests(e.Requests);
            LinksAndComments.UpdateRequests(e.Requests);
            Listings.UpdateRequests(e.Requests);
            LiveThreads.UpdateRequests(e.Requests);
            Misc.UpdateRequests(e.Requests);
            Moderation.UpdateRequests(e.Requests);
            Modmail.UpdateRequests(e.Requests);
            Multis.UpdateRequests(e.Requests);
            PrivateMessages.UpdateRequests(e.Requests);
            RedditGold.UpdateRequests(e.Requests);
            Search.UpdateRequests(e.Requests);
            Subreddits.UpdateRequests(e.Requests);
            Users.UpdateRequests(e.Requests);
            Widgets.UpdateRequests(e.Requests);
            Wiki.UpdateRequests(e.Requests);
        }

        public void C_MonitoringUpdated(object sender, MonitoringUpdateEventArgs e)
        {
            Monitor.UpdateMonitoring(e);
        }
    }
}
