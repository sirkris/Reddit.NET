using Reddit.NET.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Controllers
{
    public class Dispatch
    {
        public Account Account;
        public Apps Apps;
        public Captcha Captcha;
        public Emoji Emoji;
        public Flair Flair;
        public LinksAndComments LinksAndComments;
        public Listings Listings;
        public Moderation Moderation;
        public Multis Multis;
        public PrivateMessages PrivateMessages;
        public Search Search;
        public Subreddits Subreddits;
        public Users Users;
        public Wiki Wiki;

        /// <summary>
        /// Instantiate endpoint singletons.
        /// </summary>
        /// <param name="refreshToken">The OAuth refresh token required to obtain a Reddit API access token.</param>
        /// <param name="accessToken">The OAuth access token required to access the Reddit API.</param>
        public Dispatch(string appId, string refreshToken, string accessToken, RestClient restClient)
        {
            this.Account = new Account(appId, refreshToken, accessToken, restClient);
            this.Apps = new Apps(appId, refreshToken, accessToken, restClient);
            this.Captcha = new Captcha(appId, refreshToken, accessToken, restClient);
            this.Emoji = new Emoji(appId, refreshToken, accessToken, restClient);
            this.Flair = new Flair(appId, refreshToken, accessToken, restClient);
            this.LinksAndComments = new LinksAndComments(appId, refreshToken, accessToken, restClient);
            this.Listings = new Listings(appId, refreshToken, accessToken, restClient);
            this.Moderation = new Moderation(appId, refreshToken, accessToken, restClient);
            this.Multis = new Multis(appId, refreshToken, accessToken, restClient);
            this.PrivateMessages = new PrivateMessages(appId, refreshToken, accessToken, restClient);
            this.Search = new Search(appId, refreshToken, accessToken, restClient);
            this.Subreddits = new Subreddits(appId, refreshToken, accessToken, restClient);
            this.Users = new Users(appId, refreshToken, accessToken, restClient);
            this.Wiki = new Wiki(appId, refreshToken, accessToken, restClient);
        }
    }
}
