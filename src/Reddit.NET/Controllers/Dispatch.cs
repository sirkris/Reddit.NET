using Reddit.NET.Models;
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
        public Dispatch(string accessToken)
        {
            this.Account = new Account(accessToken);
            this.Apps = new Apps(Account);
            this.Captcha = new Captcha(Account);
            this.Flair = new Flair(Account);
            this.LinksAndComments = new LinksAndComments(Account);
            this.Listings = new Listings(Account);
            this.Moderation = new Moderation(Account);
            this.Multis = new Multis(Account);
            this.PrivateMessages = new PrivateMessages(Account);
            this.Search = new Search(Account);
            this.Subreddits = new Subreddits(Account);
            this.Users = new Users(Account);
            this.Wiki = new Wiki(Account);
        }
    }
}
