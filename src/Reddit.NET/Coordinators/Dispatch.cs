using API = Reddit.Models;
using Reddit.Models.EventArgs;
using RestSharp;

namespace Reddit.Coordinators
{
    public class Dispatch
    {
        public API.Account Account;
        public API.Captcha Captcha;
        public API.Emoji Emoji;
        public API.Flair Flair;
        public API.LinksAndComments LinksAndComments;
        public API.Listings Listings;
        public API.LiveThreads LiveThreads;
        public API.Misc Misc;
        public API.Moderation Moderation;
        public API.Modmail Modmail;
        public API.Multis Multis;
        public API.PrivateMessages PrivateMessages;
        public API.RedditGold RedditGold;
        public API.Search Search;
        public API.Subreddits Subreddits;
        public API.Users Users;
        public API.Widgets Widgets;
        public API.Wiki Wiki;

        internal API.Internal.Monitor Monitor;

        /// <summary>
        /// Instantiate endpoint singletons.
        /// </summary>
        /// <param name="refreshToken">The OAuth refresh token required to obtain a Reddit API access token.</param>
        /// <param name="accessToken">The OAuth access token required to access the Reddit API.</param>
        public Dispatch(string appId, string appSecret, string refreshToken, string accessToken, RestClient restClient, string deviceId = null)
        {
            Account = new API.Account(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Captcha = new API.Captcha(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Emoji = new API.Emoji(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Flair = new API.Flair(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            LinksAndComments = new API.LinksAndComments(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Listings = new API.Listings(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            LiveThreads = new API.LiveThreads(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Misc = new API.Misc(appId, refreshToken, appSecret, accessToken, ref restClient, deviceId);
            Moderation = new API.Moderation(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Modmail = new API.Modmail(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Multis = new API.Multis(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            PrivateMessages = new API.PrivateMessages(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            RedditGold = new API.RedditGold(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Search = new API.Search(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Subreddits = new API.Subreddits(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Users = new API.Users(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Widgets = new API.Widgets(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);
            Wiki = new API.Wiki(appId, appSecret, refreshToken, accessToken, ref restClient, deviceId);

            Monitor = new API.Internal.Monitor();

            Account.TokenUpdated += C_TokenUpdated;
            Captcha.TokenUpdated += C_TokenUpdated;
            Emoji.TokenUpdated += C_TokenUpdated;
            Flair.TokenUpdated += C_TokenUpdated;
            LinksAndComments.TokenUpdated += C_TokenUpdated;
            Listings.TokenUpdated += C_TokenUpdated;
            LiveThreads.TokenUpdated += C_TokenUpdated;
            Misc.TokenUpdated += C_TokenUpdated;
            Moderation.TokenUpdated += C_TokenUpdated;
            Modmail.TokenUpdated += C_TokenUpdated;
            Multis.TokenUpdated += C_TokenUpdated;
            PrivateMessages.TokenUpdated += C_TokenUpdated;
            RedditGold.TokenUpdated += C_TokenUpdated;
            Search.TokenUpdated += C_TokenUpdated;
            Subreddits.TokenUpdated += C_TokenUpdated;
            Users.TokenUpdated += C_TokenUpdated;
            Widgets.TokenUpdated += C_TokenUpdated;
            Wiki.TokenUpdated += C_TokenUpdated;

            Account.RequestsUpdated += C_RequestsUpdated;
            Captcha.RequestsUpdated += C_RequestsUpdated;
            Emoji.RequestsUpdated += C_RequestsUpdated;
            Flair.RequestsUpdated += C_RequestsUpdated;
            LinksAndComments.RequestsUpdated += C_RequestsUpdated;
            Listings.RequestsUpdated += C_RequestsUpdated;
            LiveThreads.RequestsUpdated += C_RequestsUpdated;
            Misc.RequestsUpdated += C_RequestsUpdated;
            Moderation.RequestsUpdated += C_RequestsUpdated;
            Modmail.RequestsUpdated += C_RequestsUpdated;
            Multis.RequestsUpdated += C_RequestsUpdated;
            PrivateMessages.RequestsUpdated += C_RequestsUpdated;
            RedditGold.RequestsUpdated += C_RequestsUpdated;
            Search.RequestsUpdated += C_RequestsUpdated;
            Subreddits.RequestsUpdated += C_RequestsUpdated;
            Users.RequestsUpdated += C_RequestsUpdated;
            Widgets.RequestsUpdated += C_RequestsUpdated;
            Wiki.RequestsUpdated += C_RequestsUpdated;

            Monitor.MonitoringUpdated += C_MonitoringUpdated;
        }

        public void C_TokenUpdated(object sender, TokenUpdateEventArgs e)
        {
            Account.UpdateAccessToken(e.AccessToken);
            Captcha.UpdateAccessToken(e.AccessToken);
            Emoji.UpdateAccessToken(e.AccessToken);
            Flair.UpdateAccessToken(e.AccessToken);
            LinksAndComments.UpdateAccessToken(e.AccessToken);
            Listings.UpdateAccessToken(e.AccessToken);
            LiveThreads.UpdateAccessToken(e.AccessToken);
            Misc.UpdateAccessToken(e.AccessToken);
            Moderation.UpdateAccessToken(e.AccessToken);
            Modmail.UpdateAccessToken(e.AccessToken);
            Multis.UpdateAccessToken(e.AccessToken);
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
            Captcha.UpdateRequests(e.Requests);
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
