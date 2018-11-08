using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System.Collections.Generic;
using System.IO;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class SubredditsTests : BaseTests
    {
        [TestMethod]
        public void About()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SubredditChild about = reddit.Models.Subreddits.About("WayOfTheMueller");
            DynamicShortListingContainer aboutBanned = reddit.Models.Subreddits.About("banned", null, null, null, false, "StillSandersForPres");
            DynamicShortListingContainer aboutMuted = reddit.Models.Subreddits.About("muted", null, null, null, false, "StillSandersForPres");
            DynamicShortListingContainer aboutWikiBanned = reddit.Models.Subreddits.About("wikibanned", null, null, null, false, "StillSandersForPres");
            DynamicShortListingContainer aboutContributors = reddit.Models.Subreddits.About("contributors", null, null, null, false, "StillSandersForPres");
            DynamicShortListingContainer aboutWikiContributors = reddit.Models.Subreddits.About("wikicontributors", null, null, null, false, "StillSandersForPres");
            DynamicShortListingContainer aboutModerators = reddit.Models.Subreddits.About("moderators", null, null, null, false, "StillSandersForPres");
            
            Assert.IsNotNull(about);
            Assert.IsTrue(about.Data.DisplayName.Equals("WayOfTheMueller"));
            Assert.IsNotNull(aboutBanned);
            Assert.IsNotNull(aboutMuted);
            Assert.IsNotNull(aboutWikiBanned);
            Assert.IsNotNull(aboutContributors);
            Assert.IsNotNull(aboutWikiContributors);
            Assert.IsNotNull(aboutModerators);
        }

        [TestMethod]
        public void SearchRedditNames()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SubredditNames subredditNames = reddit.Models.Subreddits.SearchRedditNames(false, true, true, "Shitty");

            Validate(subredditNames);
        }

        [TestMethod]
        public void SubmitText()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SubredditSubmitText subredditSubmitText = reddit.Models.Subreddits.SubmitText("WayOfTheBern");

            Assert.IsNotNull(subredditSubmitText);
        }

        [TestMethod]
        public void SearchSubreddits()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SubSearch subSearch = reddit.Models.Subreddits.SearchSubreddits(false, true, true, "Shitty");

            Validate(subSearch);
        }

        [TestMethod]
        public void Rules()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            RulesContainer rules = reddit.Models.Subreddits.Rules("WayOfTheBern");

            Assert.IsNotNull(rules);
        }

        [TestMethod]
        public void Traffic()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            Traffic traffic = reddit.Models.Subreddits.Traffic("StillSandersForPres");

            Assert.IsNotNull(traffic);
        }

        [TestMethod]
        public void Mine()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SubredditContainer mineSubscriber = reddit.Models.Subreddits.Mine("subscriber", null, null, false);
            SubredditContainer mineContributor = reddit.Models.Subreddits.Mine("contributor", null, null, false);
            SubredditContainer mineModerator = reddit.Models.Subreddits.Mine("moderator", null, null, false);

            Assert.IsNotNull(mineSubscriber);
            Assert.IsNotNull(mineContributor);
            Assert.IsNotNull(mineModerator);
        }

        [TestMethod]
        public void Search()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SubredditContainer search = reddit.Models.Subreddits.Search(null, null, "Sanders", false, "relevance");
            SubredditContainer searchWithShowUsers = reddit.Models.Subreddits.Search(null, null, "Sanders", true, "relevance");

            Assert.IsNotNull(search);
            Assert.IsNotNull(searchWithShowUsers);
        }

        [TestMethod]
        public void Get()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SubredditContainer popularSubs = reddit.Models.Subreddits.Get("popular", null, null, false);
            SubredditContainer newSubs = reddit.Models.Subreddits.Get("new", null, null, false);
            SubredditContainer defaultSubs = reddit.Models.Subreddits.Get("default", null, null, false);

            Assert.IsNotNull(popularSubs);
            Assert.IsNotNull(newSubs);
            Assert.IsNotNull(defaultSubs);
        }

        [TestMethod]
        public void GetUserSubreddits()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SubredditContainer popularUserSubs = reddit.Models.Subreddits.GetUserSubreddits("popular", null, null, false);
            SubredditContainer newUserSubs = reddit.Models.Subreddits.GetUserSubreddits("new", null, null, false);

            Assert.IsNotNull(popularUserSubs);
            Assert.IsNotNull(newUserSubs);
        }

        // Datarows subreddit must be either non-existing or existing with mod access.  --Kris
        [TestMethod]
        public void SiteAdmin()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            // Attempt to create a new subreddit.  --Kris
            GenericContainer res = reddit.Models.Subreddits.SiteAdmin(false, true, true, true, true, true, false, "Test subreddit created by Reddit.NET.",
                false, true, null, "Reddit.NET Bot Testing", false, "#0000FF", "en-US", "any", testData["Subreddit"], true, false, "Test subreddit created by Reddit.NET.",
                true, true, "low", "high", "high", true, null, "New Bot Link!", "Robots and humans are welcome to post here.  Please adhere to Reddit's rules.",
                "New Bot Post", "new", null, false, "Reddit.NET Bot Testing", "public", "modonly");

            // If sub already exists, attempt an update, instead.  --Kris
            if (res.JSON != null && res.JSON.Errors != null && res.JSON.Errors.Count > 0
                && res.JSON.Errors[0][0].Equals("SUBREDDIT_EXISTS"))
            {
                SubredditChild testSub = reddit.Models.Subreddits.About(testData["Subreddit"]);

                res = reddit.Models.Subreddits.SiteAdmin(false, true, true, true, true, true, false, "Test subreddit maintained by Reddit.NET.",
                    false, true, null, "Reddit.NET Bot Testing", false, "#0000FF", "en-US", "any", null, true, false, "Test subreddit maintained by Reddit.NET.",
                    true, true, "low", "high", "high", true, testSub.Data.Name, "New Bot Link!", "Robots and humans are welcome to post here.  Please adhere to Reddit's rules.",
                    "New Bot Post", "new", null, false, "Reddit.NET Bot Testing", "public", "modonly");
            }

            Validate(res);
        }

        [TestMethod]
        public void SubredditAutocomplete()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SubredditAutocompleteResultContainer subs = reddit.Models.Subreddits.SubredditAutocomplete(false, true, "Shitty");

            Assert.IsNotNull(subs);
        }

        [TestMethod]
        public void SubredditAutocompleteV2()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SubredditContainer subs = reddit.Models.Subreddits.SubredditAutocompleteV2(true, false, true, "Shitty");

            Assert.IsNotNull(subs);
        }

        [TestMethod]
        public void SubredditStylesheet()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            GenericContainer res = reddit.Models.Subreddits.SubredditStylesheet("save", "This is a test.", ".whatever{}", testData["Subreddit"]);

            Validate(res);
        }

        [TestMethod]
        public void SubscribeByFullname()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            reddit.Models.Subreddits.SubscribeByFullname("sub", false, "t5_3fblp");
        }

        [TestMethod]
        public void Subscribe()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            reddit.Models.Subreddits.Subscribe("unsub", false, testData["Subreddit"]);  // Unsubscribe
            reddit.Models.Subreddits.Subscribe("sub", false, testData["Subreddit"]);  // Subscribe
        }

        [TestMethod]
        public void Edit()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SubredditSettingsContainer settings = reddit.Models.Subreddits.Edit(testData["Subreddit"], false, "");

            Validate(settings);
        }

        [TestMethod]
        public void SubredditImages()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            // Get the embedded test images.  --Kris
            byte[] imageData = GetResourceFile("birdie.png");
            byte[] imageBannerData = GetResourceFile("banner.jpg");
            byte[] imageIconData = GetResourceFile("birdie256.jpg");

            // Add the images (two succeed, two fail due to size constraints).  --Kris
            ImageUploadResult resHeader = reddit.Models.Subreddits.UploadSrImg(imageData, 1, "birdie", "header", testData["Subreddit"], "png");
            ImageUploadResult resImg = reddit.Models.Subreddits.UploadSrImg(imageData, 0, "birdie", "img", testData["Subreddit"], "png");
            ImageUploadResult resIcon = reddit.Models.Subreddits.UploadSrImg(imageData, 0, "birdie", "icon", testData["Subreddit"], "png");
            ImageUploadResult resBanner = reddit.Models.Subreddits.UploadSrImg(imageData, 0, "birdie", "banner", testData["Subreddit"], "png");

            Validate(resHeader);
            Validate(resImg);

            Assert.IsNotNull(resIcon);
            Assert.IsTrue(resIcon.Errors != null && resIcon.Errors.Count == 1);
            Assert.IsTrue(resIcon.Errors[0].Equals("IMAGE_ERROR"));
            Assert.IsTrue(resIcon.ErrorsValues != null && resIcon.ErrorsValues.Count == 1);
            Assert.IsTrue(resIcon.ErrorsValues[0].Equals("must be 256x256 pixels"));
            Assert.IsTrue(string.IsNullOrWhiteSpace(resIcon.ImgSrc));

            Assert.IsNotNull(resBanner);
            Assert.IsTrue(resBanner.Errors != null && resBanner.Errors.Count == 1);
            Assert.IsTrue(resBanner.Errors[0].Equals("IMAGE_ERROR"));
            Assert.IsTrue(resBanner.ErrorsValues != null && resBanner.ErrorsValues.Count == 1);
            Assert.IsTrue(resBanner.ErrorsValues[0].Equals("10:3 aspect ratio required"));
            Assert.IsTrue(string.IsNullOrWhiteSpace(resBanner.ImgSrc));

            // Add the remaining two images (both succeed).  --Kris
            resIcon = reddit.Models.Subreddits.UploadSrImg(imageIconData, 0, "birdieIcon", "icon", testData["Subreddit"], "jpg");
            resBanner = reddit.Models.Subreddits.UploadSrImg(imageBannerData, 0, "birdieBanner", "banner", testData["Subreddit"], "png");

            Validate(resIcon);
            Validate(resBanner);

            // Delete the images.  --Kris
            GenericContainer resDelHeader = reddit.Models.Subreddits.DeleteSrHeader(testData["Subreddit"]);
            GenericContainer resDelImg = reddit.Models.Subreddits.DeleteSrImg("birdie", testData["Subreddit"]);
            GenericContainer resDelBanner = reddit.Models.Subreddits.DeleteSrBanner(testData["Subreddit"]);
            GenericContainer resDelIcon = reddit.Models.Subreddits.DeleteSrIcon(testData["Subreddit"]);

            Validate(resDelHeader);
            Validate(resDelImg);
            Validate(resDelBanner);
            Validate(resDelIcon);
        }

        private byte[] GetResourceFile(string filename)
        {
            using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Reddit.NETTests.Resources." + filename))
            {
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    return binaryReader.ReadBytes(int.MaxValue / 2);
                }
            }
        }
    }
}
