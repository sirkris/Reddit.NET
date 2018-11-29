using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ControllerTests
{
    [TestClass]
    public class WikiTests : BaseTests
    {
        private Subreddit Subreddit
        {
            get
            {
                return subreddit ?? GetSubreddit(ref subreddit);
            }
            set
            {
                subreddit = value;
            }
        }
        private Subreddit subreddit;

        public WikiTests() : base() { }

        [TestMethod]
        public void Pages()
        {
            Validate(Subreddit.Wiki.Pages);
        }

        [TestMethod]
        public void GetPage()
        {
            Validate(Subreddit.Wiki.GetPage("index"));
        }

        [TestMethod]
        public void GetContributors()
        {
            Validate(Subreddit.Wiki.GetContributors());
        }

        [TestMethod]
        public void GetBannedUsers()
        {
            Validate(Subreddit.Wiki.GetBannedUsers());
        }

        [TestMethod]
        public void GetRecentPageRevisions()
        {
            Validate(Subreddit.Wiki.GetRecentPageRevisions());
        }
    }
}
