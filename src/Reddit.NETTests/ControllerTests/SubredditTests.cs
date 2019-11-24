using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using Reddit.Exceptions;
using Reddit.Inputs.Search;
using System.Collections.Generic;

namespace RedditTests.ControllerTests
{
    [TestClass]
    public class SubredditTests : BaseTests
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

        private readonly byte[] ImageData;
        private readonly byte[] ImageBannerData;
        private readonly byte[] ImageIconData;

        public SubredditTests() : base()
        {
            ImageData = GetResourceFile("birdie.png");
            ImageBannerData = GetResourceFile("banner.jpg");
            ImageIconData = GetResourceFile("birdie256.jpg");
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
            // TODO - The Reddit API sometimes returns 404 on valid unsubscribe requests.  The requests are still successful (I checked), so it must be an API bug.  --Kris
            try
            {
                Subreddit.Unsubscribe();
            }
            catch (RedditNotFoundException) { }
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
        public void IBest()
        {
            Validate(Subreddit.Posts.IBest);
        }

        [TestMethod]
        public void Hot()
        {
            Validate(Subreddit.Posts.Hot);
        }

        [TestMethod]
        public void IHot()
        {
            Validate(Subreddit.Posts.IHot);
        }

        [TestMethod]
        public void New()
        {
            Validate(Subreddit.Posts.New);
        }

        [TestMethod]
        public void INew()
        {
            Validate(Subreddit.Posts.INew);
        }

        [TestMethod]
        public void Rising()
        {
            Validate(Subreddit.Posts.Rising);
        }

        [TestMethod]
        public void IRising()
        {
            Validate(Subreddit.Posts.IRising);
        }

        [TestMethod]
        public void Top()
        {
            Validate(Subreddit.Posts.Top);
        }

        [TestMethod]
        public void ITop()
        {
            Validate(Subreddit.Posts.ITop);
        }

        [TestMethod]
        public void Controversial()
        {
            Validate(Subreddit.Posts.Controversial);
        }

        [TestMethod]
        public void IControversial()
        {
            Validate(Subreddit.Posts.IControversial);
        }

        [TestMethod]
        public void ModQueue()
        {
            Validate(Subreddit.Posts.ModQueue);
        }

        [TestMethod]
        public void IModQueue()
        {
            Validate(Subreddit.Posts.IModQueue);
        }

        [TestMethod]
        public void ModQueueReports()
        {
            Validate(Subreddit.Posts.ModQueueReports);
        }

        [TestMethod]
        public void IModQueueReports()
        {
            Validate(Subreddit.Posts.IModQueueReports);
        }

        [TestMethod]
        public void ModQueueSpam()
        {
            Validate(Subreddit.Posts.ModQueueSpam);
        }

        [TestMethod]
        public void IModQueueSpam()
        {
            Validate(Subreddit.Posts.IModQueueSpam);
        }

        [TestMethod]
        public void ModQueueUnmoderated()
        {
            Validate(Subreddit.Posts.ModQueueUnmoderated);
        }

        [TestMethod]
        public void IModQueueUnmoderated()
        {
            Validate(Subreddit.Posts.IModQueueUnmoderated);
        }

        [TestMethod]
        public void ModQueueEdited()
        {
            Validate(Subreddit.Posts.ModQueueEdited);
        }

        [TestMethod]
        public void IModQueueEdited()
        {
            Validate(Subreddit.Posts.IModQueueEdited);
        }

        [TestMethod]
        public void Search()
        {
            List<Post> posts = Subreddit.Search(new SearchGetSearchInput("Test"));

            Validate(posts);
            Assert.IsTrue(posts.Count > 0);
        }
    }
}
