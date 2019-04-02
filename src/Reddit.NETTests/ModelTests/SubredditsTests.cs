using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Exceptions;
using Reddit.Inputs;
using Reddit.Inputs.Subreddits;
using Reddit.Things;
using System.Collections.Generic;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class SubredditsTests : BaseTests
    {
        public SubredditsTests() : base() { }

        [TestMethod]
        public void About()
        {
            SubredditChild about = reddit.Models.Subreddits.About("WayOfTheMueller");
            DynamicShortListingContainer aboutBanned = reddit.Models.Subreddits.About("banned", new SubredditsAboutInput(), "StillSandersForPres");
            DynamicShortListingContainer aboutMuted = reddit.Models.Subreddits.About("muted", new SubredditsAboutInput(), "StillSandersForPres");
            DynamicShortListingContainer aboutWikiBanned = reddit.Models.Subreddits.About("wikibanned", new SubredditsAboutInput(), "StillSandersForPres");
            DynamicShortListingContainer aboutContributors = reddit.Models.Subreddits.About("contributors", new SubredditsAboutInput(), "StillSandersForPres");
            DynamicShortListingContainer aboutWikiContributors = reddit.Models.Subreddits.About("wikicontributors", new SubredditsAboutInput(), "StillSandersForPres");
            DynamicShortListingContainer aboutModerators = reddit.Models.Subreddits.About("moderators", new SubredditsAboutInput(), "StillSandersForPres");
            
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
            SubredditNames subredditNames = reddit.Models.Subreddits.SearchRedditNames(new SubredditsSearchNamesInput("Shitty"));

            Validate(subredditNames);
        }

        [TestMethod]
        public void SubmitText()
        {
            SubredditSubmitText subredditSubmitText = reddit.Models.Subreddits.SubmitText("WayOfTheBern");

            Assert.IsNotNull(subredditSubmitText);
        }

        [TestMethod]
        public void SearchSubreddits()
        {
            SubSearch subSearch = reddit.Models.Subreddits.SearchSubreddits(new SubredditsSearchNamesInput("Shitty"));

            Validate(subSearch);
        }

        [TestMethod]
        public void Rules()
        {
            RulesContainer rules = reddit.Models.Subreddits.Rules("WayOfTheBern");

            Assert.IsNotNull(rules);
        }

        [TestMethod]
        public void Traffic()
        {
            Traffic traffic = reddit.Models.Subreddits.Traffic("StillSandersForPres");

            Assert.IsNotNull(traffic);
        }

        [TestMethod]
        public void Mine()
        {
            SubredditContainer mineSubscriber = reddit.Models.Subreddits.Mine("subscriber", new CategorizedSrListingInput());
            SubredditContainer mineContributor = reddit.Models.Subreddits.Mine("contributor", new CategorizedSrListingInput());
            SubredditContainer mineModerator = reddit.Models.Subreddits.Mine("moderator", new CategorizedSrListingInput());

            Assert.IsNotNull(mineSubscriber);
            Assert.IsNotNull(mineContributor);
            Assert.IsNotNull(mineModerator);
        }

        [TestMethod]
        public void Search()
        {
            SubredditContainer search = reddit.Models.Subreddits.Search(new SubredditsSearchInput("Sanders"));
            SubredditContainer searchWithShowUsers = reddit.Models.Subreddits.Search(new SubredditsSearchInput("Sanders", true));

            Assert.IsNotNull(search);
            Assert.IsNotNull(searchWithShowUsers);
        }

        [TestMethod]
        public void Get()
        {
            SubredditContainer popularSubs = reddit.Models.Subreddits.Get("popular", new CategorizedSrListingInput());
            SubredditContainer newSubs = reddit.Models.Subreddits.Get("new", new CategorizedSrListingInput());
            SubredditContainer defaultSubs = reddit.Models.Subreddits.Get("default", new CategorizedSrListingInput());

            Assert.IsNotNull(popularSubs);
            Assert.IsNotNull(newSubs);
            Assert.IsNotNull(defaultSubs);
        }

        [TestMethod]
        public void GetUserSubreddits()
        {
            SubredditContainer popularUserSubs = reddit.Models.Subreddits.GetUserSubreddits("popular", new CategorizedSrListingInput());
            SubredditContainer newUserSubs = reddit.Models.Subreddits.GetUserSubreddits("new", new CategorizedSrListingInput());

            Assert.IsNotNull(popularUserSubs);
            Assert.IsNotNull(newUserSubs);
        }

        // Datarows subreddit must be either non-existing or existing with mod access.  --Kris
        [TestMethod]
        public void SiteAdmin()
        {
            // Attempt to create a new subreddit.  --Kris
            GenericContainer res = reddit.Models.Subreddits.SiteAdmin(new SubredditsSiteAdminInput(false, true, true, true, true, true, false, "Test subreddit created by Reddit.NET.",
                false, true, false, "#0000FF", "en-US", "any", testData["Subreddit"], true, false, "Test subreddit created by Reddit.NET.",
                true, true, "low", "high", "high", true, null, "New Bot Link!", "Robots and humans are welcome to post here.  Please adhere to Reddit's rules.",
                "New Bot Post", "new", null, false, "Reddit.NET Bot Testing", "public", "modonly"), null, "Reddit.NET Bot Testing");

            // If sub already exists, attempt an update, instead.  --Kris
            if (res.JSON != null && res.JSON.Errors != null && res.JSON.Errors.Count > 0
                && res.JSON.Errors[0][0].Equals("SUBREDDIT_EXISTS"))
            {
                SubredditChild testSub = reddit.Models.Subreddits.About(testData["Subreddit"]);

                res = reddit.Models.Subreddits.SiteAdmin(new SubredditsSiteAdminInput(false, true, true, true, true, true, false, "Test subreddit maintained by Reddit.NET.",
                    false, true, false, "#0000FF", "en-US", "any", null, true, false, "Test subreddit maintained by Reddit.NET.",
                    true, true, "low", "high", "high", true, testSub.Data.Name, "New Bot Link!", "Robots and humans are welcome to post here.  Please adhere to Reddit's rules.",
                    "New Bot Post", "new", null, false, "Reddit.NET Bot Testing", "public", "modonly"), null, "Reddit.NET Bot Testing");
            }

            Validate(res);
        }

        [TestMethod]
        public void SubredditAutocomplete()
        {
            SubredditAutocompleteResultContainer subs = reddit.Models.Subreddits.SubredditAutocomplete(new SubredditsAutocompleteInput("Shitty"));

            Assert.IsNotNull(subs);
        }

        [TestMethod]
        public void SubredditAutocompleteV2()
        {
            SubredditContainer subs = reddit.Models.Subreddits.SubredditAutocompleteV2(new SubredditsAutocompleteV2Input("Shitty"));

            Assert.IsNotNull(subs);
        }

        [TestMethod]
        public void SubredditStylesheet()
        {
            GenericContainer res = reddit.Models.Subreddits.SubredditStylesheet(new SubredditsSubredditStylesheetInput(".whatever{}", "This is a test."), testData["Subreddit"]);

            Validate(res);
        }

        [TestMethod]
        public void SubscribeByFullname()
        {
            reddit.Models.Subreddits.SubscribeByFullname(new SubredditsSubByFullnameInput("t5_3fblp"));
        }

        [TestMethod]
        public void Subscribe()
        {
            try
            {
                reddit.Models.Subreddits.Subscribe(new SubredditsSubByNameInput(testData["Subreddit"], "unsub"));  // Unsubscribe
            }
            catch (RedditNotFoundException) { }

            reddit.Models.Subreddits.Subscribe(new SubredditsSubByNameInput(testData["Subreddit"], "sub"));  // Subscribe
        }

        [TestMethod]
        public void Edit()
        {
            SubredditSettingsContainer settings = reddit.Models.Subreddits.Edit(testData["Subreddit"], new SubredditsEditInput());

            Validate(settings);
        }

        [TestMethod]
        public void Recommended()
        {
            IEnumerable<SubredditRecommendations> subredditRecommendations = reddit.Models.Subreddits.Recommended(testData["Subreddit"], new SubredditsRecommendInput());

            Validate(subredditRecommendations);
        }
    }
}
