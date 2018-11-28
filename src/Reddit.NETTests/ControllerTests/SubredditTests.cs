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
    public class SubredditTests : BaseTests
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

        private readonly byte[] ImageData;
        private readonly byte[] ImageBannerData;
        private readonly byte[] ImageIconData;

        public SubredditTests() : base()
        {
            ImageData = GetResourceFile("birdie.png");
            ImageBannerData = GetResourceFile("banner.jpg");
            ImageIconData = GetResourceFile("birdie256.jpg");
        }

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
        public void About()
        {
            Validate(Subreddit);
            Assert.AreEqual(testData["Subreddit"], Subreddit.Name);
        }

        [TestMethod]
        public void GetModerators()
        {
            Validate(Subreddit.Moderators);
            Assert.IsTrue(Subreddit.Moderators.Count > 0);
        }

        [TestMethod]
        public void GetContributors()
        {
            Validate(Subreddit.GetContributors());
        }

        [TestMethod]
        public void GetMutedUsers()
        {
            Validate(Subreddit.GetMutedUsers());
        }

        [TestMethod]
        public void GetBannedUsers()
        {
            Validate(Subreddit.GetBannedUsers());
        }

        [TestMethod]
        public void GetSubmitText()
        {
            Validate(Subreddit.SubmitText);
        }

        [TestMethod]
        public void UpdateStyleSheet()
        {
            Subreddit.UpdateStylesheet("This is a test.", ".whatever{}");
        }

        [TestMethod]
        public void Unsubscribe()
        {
            Subreddit.Unsubscribe();
        }

        [TestMethod]
        public void Subscribe()
        {
            Subreddit.Subscribe();
        }

        [TestMethod]
        public void DeleteBanner()
        {
            Subreddit.DeleteBanner();
        }

        [TestMethod]
        public void DeleteHeader()
        {
            Subreddit.DeleteHeader();
        }

        [TestMethod]
        public void DeleteIcon()
        {
            Subreddit.DeleteIcon();
        }

        [TestMethod]
        public void DeleteImg()
        {
            Subreddit.DeleteImg("boogeyboogeyboogey");
        }

        [TestMethod]
        public void UploadImg()
        {
            Validate(Subreddit.UploadImg(ImageData, "birdie"));
        }

        [TestMethod]
        public void UploadHeader()
        {
            Validate(Subreddit.UploadHeader(ImageData, "birdie"));
        }

        [TestMethod]
        public void UploadIcon()
        {
            Validate(Subreddit.UploadIcon(ImageIconData, "birdie"));
        }

        [TestMethod]
        public void UploadBanner()
        {
            Validate(Subreddit.UploadBanner(ImageBannerData, "birdie"));
        }

        [TestMethod]
        public void GetSettings()
        {
            Validate(Subreddit.GetSettings());
        }

        [TestMethod]
        public void GetRules()
        {
            Validate(Subreddit.GetRules());
        }

        [TestMethod]
        public void GetTraffic()
        {
            Validate(Subreddit.GetTraffic());
        }

        [TestMethod]
        public void ClearLinkFlairTemplates()
        {
            Subreddit.ClearLinkFlairTemplates();
        }

        [TestMethod]
        public void ClearUserFlairTemplates()
        {
            Subreddit.ClearUserFlairTemplates();
        }

        [TestMethod]
        public void DeleteFlair()
        {
            Subreddit.DeleteFlair("KrisCraig");
        }

        [TestMethod]
        public void SavedMediaText()
        {
            Validate(Subreddit.SavedMediaText("https://e.thumbs.redditmedia.com/bOToSJt13ylszjE4.png"));
        }

        [TestMethod]
        public void GetLog()
        {
            Subreddit.GetLog();
        }

        [TestMethod]
        public void Stylesheet()
        {
            Subreddit.Stylesheet();
        }

        [TestMethod]
        public void Best()
        {
            Validate(Subreddit.Posts.Best);
        }

        [TestMethod]
        public void Hot()
        {
            Validate(Subreddit.Posts.Hot);
        }

        [TestMethod]
        public void New()
        {
            Validate(Subreddit.Posts.New);
        }

        [TestMethod]
        public void Rising()
        {
            Validate(Subreddit.Posts.Rising);
        }

        [TestMethod]
        public void Top()
        {
            Validate(Subreddit.Posts.Top);
        }

        [TestMethod]
        public void Controversial()
        {
            Validate(Subreddit.Posts.Controversial);
        }

        [TestMethod]
        public void ModQueue()
        {
            Validate(Subreddit.Posts.ModQueue);
        }

        [TestMethod]
        public void ModQueueReports()
        {
            Validate(Subreddit.Posts.ModQueueReports);
        }

        [TestMethod]
        public void ModQueueSpam()
        {
            Validate(Subreddit.Posts.ModQueueSpam);
        }

        [TestMethod]
        public void ModQueueUnmoderated()
        {
            Validate(Subreddit.Posts.ModQueueUnmoderated);
        }

        [TestMethod]
        public void ModQueueEdited()
        {
            Validate(Subreddit.Posts.ModQueueEdited);
        }
    }
}
