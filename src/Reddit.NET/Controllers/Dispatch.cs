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
        /// <param name="accessToken">The OAuth access token required to access the Reddit API.</param>
        public Dispatch(string accessToken, RestClient restClient)
        {
            this.Account = new Account(accessToken, restClient);
            this.Apps = new Apps(accessToken, restClient);
            this.Captcha = new Captcha(accessToken, restClient);
            this.Flair = new Flair(accessToken, restClient);
            this.LinksAndComments = new LinksAndComments(accessToken, restClient);
            this.Listings = new Listings(accessToken, restClient);
            this.Moderation = new Moderation(accessToken, restClient);
            this.Multis = new Multis(accessToken, restClient);
            this.PrivateMessages = new PrivateMessages(accessToken, restClient);
            this.Search = new Search(accessToken, restClient);
            this.Subreddits = new Subreddits(accessToken, restClient);
            this.Users = new Users(accessToken, restClient);
            this.Wiki = new Wiki(accessToken, restClient);
        }
    }
}
