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
            this.Apps = new Apps(Account, restClient);
            this.Captcha = new Captcha(Account, restClient);
            this.Flair = new Flair(Account, restClient);
            this.LinksAndComments = new LinksAndComments(Account, restClient);
            this.Listings = new Listings(Account, restClient);
            this.Moderation = new Moderation(Account, restClient);
            this.Multis = new Multis(Account, restClient);
            this.PrivateMessages = new PrivateMessages(Account, restClient);
            this.Search = new Search(Account, restClient);
            this.Subreddits = new Subreddits(Account, restClient);
            this.Users = new Users(Account, restClient);
            this.Wiki = new Wiki(Account, restClient);
        }
    }
}
