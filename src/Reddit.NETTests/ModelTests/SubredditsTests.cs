using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

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

            Assert.IsNotNull(subredditNames);
            Assert.IsNotNull(subredditNames.Names);
            Assert.IsTrue(subredditNames.Names.Count > 0);
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

            Assert.IsNotNull(subSearch);
            Assert.IsNotNull(subSearch.Subreddits);
            Assert.IsTrue(subSearch.Subreddits.Count > 0);
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

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.JSON);
            Assert.IsTrue(res.JSON.Errors == null || res.JSON.Errors.Count == 0);
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

            Assert.IsNotNull(res);
            Assert.IsNotNull(res.JSON);
            Assert.IsTrue(res.JSON.Errors == null || res.JSON.Errors.Count == 0);
        }
    }
}
