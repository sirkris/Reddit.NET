﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ControllerTests
{
    [TestClass]
    public class FlairsTests : BaseTests
    {
        private Subreddit Subreddit
        {
            get
            {
                return subreddit ?? GetSubreddit();
            }
            set
            {
                subreddit = value;
            }
        }
        private Subreddit subreddit;

        public FlairsTests() : base() { }

        /// <summary>
        /// Retrieves your test subreddit.  It is assumed that the subreddit already exists at this point.
        /// </summary>
        /// <returns>The populated Subreddit data.</returns>
        private Subreddit GetSubreddit()
        {
            Subreddit = reddit.Subreddit(testData["Subreddit"]).About();
            return Subreddit;
        }

        [TestMethod]
        public void ClearLinkFlairTemplates()
        {
            Subreddit.Flairs.ClearLinkFlairTemplates();
        }

        [TestMethod]
        public void ClearUserFlairTemplates()
        {
            Subreddit.Flairs.ClearUserFlairTemplates();
        }

        [TestMethod]
        public void DeleteFlair()
        {
            Subreddit.Flairs.DeleteFlair("KrisCraig");
        }

        [TestMethod]
        public void FlairList()
        {
            Validate(Subreddit.Flairs.FlairList);
        }

        [TestMethod]
        public void LinkFlair()
        {
            Validate(Subreddit.Flairs.LinkFlair);
        }

        [TestMethod]
        public void LinkFlairV2()
        {
            Validate(Subreddit.Flairs.LinkFlairV2);
        }

        [TestMethod]
        public void UserFlair()
        {
            Validate(Subreddit.Flairs.UserFlair);
        }

        [TestMethod]
        public void UserFlairV2()
        {
            Validate(Subreddit.Flairs.UserFlairV2);
        }

        [TestMethod]
        public void CreateFlair()
        {
            Subreddit.Flairs.CreateFlair("KrisCraig", "Human");
        }

        [TestMethod]
        public void FlairConfig()
        {
            Subreddit.Flairs.FlairConfig(true, "right", true, "right", true);
        }

        [TestMethod]
        public void FlairCSV()
        {
            Validate(Subreddit.Flairs.FlairCSV("KrisCraig,Human," + Environment.NewLine + "RedditDotNetBot,Robot,"), 2);
        }

        [TestMethod]
        public void FlairSelector()
        {
            Validate(Subreddit.Flairs.FlairSelector("KrisCraig"));
        }

        [TestMethod]
        public void CreateLinkFlairTemplate()
        {
            Subreddit.Flairs.CreateLinkFlairTemplate(DateTime.Now.ToString("fffffff"));
        }

        [TestMethod]
        public void CreateUserFlairTemplate()
        {
            Subreddit.Flairs.CreateUserFlairTemplate(DateTime.Now.ToString("fffffff"));
        }

        [TestMethod]
        public void CreateLinkFlairTemplateV2()
        {
            Subreddit.Flairs.CreateLinkFlairTemplateV2("V2-" + DateTime.Now.ToString("fffffff"));
        }

        [TestMethod]
        public void CreateUserFlairTemplateV2()
        {
            Subreddit.Flairs.CreateUserFlairTemplateV2("V2-" + DateTime.Now.ToString("fffffff"));
        }

        [TestMethod]
        public void SetFlairEnabled()
        {
            Subreddit.Flairs.SetFlairEnabled();
        }
    }
}
