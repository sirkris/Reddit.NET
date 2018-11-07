using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class AccountTests : BaseTests
    {
        [TestMethod]
        // TODO - Uncomment below and add to other TestMethods in all test classes when .NET Core adds support for this.  --Kris
        /*[DeploymentItem("Reddit.NETTests\\Reddit.NETTestsData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                   "|DataDirectory|\\Reddit.NETTestsData.xml",
                   "Row",
                    DataAccessMethod.Sequential)]*/
        public void Me()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            User me = reddit.Models.Account.Me();

            Assert.IsNotNull(me);
            Assert.IsFalse(me.Created.Equals(default(DateTime)));
            Assert.IsFalse(string.IsNullOrWhiteSpace(me.Name));
        }

        [TestMethod]
        public void Karma()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            UserKarmaContainer karma = reddit.Models.Account.Karma();

            Assert.IsNotNull(karma);
        }

        [TestMethod]
        public void Prefs()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            AccountPrefs prefs = reddit.Models.Account.Prefs();
            List<UserPrefsContainer> prefsFriends = reddit.Models.Account.PrefsList("friends");
            List<UserPrefsContainer> prefsMessaging = reddit.Models.Account.PrefsList("messaging");
            UserPrefsContainer prefsBlocked = reddit.Models.Account.PrefsSingle("blocked");
            UserPrefsContainer prefsTrusted = reddit.Models.Account.PrefsSingle("trusted");

            Assert.IsNotNull(prefs);
            Assert.IsNotNull(prefsFriends);
            Assert.IsNotNull(prefsBlocked);
            Assert.IsNotNull(prefsMessaging);
            Assert.IsNotNull(prefsTrusted);
        }

        [TestMethod]
        public void Trophies()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            TrophyList trophies = reddit.Models.Account.Trophies();

            Assert.IsNotNull(trophies);
        }

        [TestMethod]
        public void UpdatePrefs()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            // This just grabs your existing preferences and sends them right back.  --Kris
            AccountPrefs res = reddit.Models.Account.UpdatePrefs(new AccountPrefsSubmit(reddit.Models.Account.Prefs(), "US", false, ""));

            Assert.IsNotNull(res);
        }
    }
}
