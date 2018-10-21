using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Data;

namespace Reddit.NETTests
{
    [TestClass]
    public class AccountTests
    {
        private TestContext TestContextInstance;

        public TestContext TestContext
        {
            get
            {
                return TestContextInstance;
            }
            set
            {
                TestContextInstance = value;
            }
        }

        private Dictionary<string, string> GetData()
        {
            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.Load(AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\..\Reddit.NETTestsData.xml");

            return new Dictionary<string, string>
            {
                { "AppId", xmlDocument.GetElementsByTagName("AppId")[0].InnerText },
                { "RefreshToken", xmlDocument.GetElementsByTagName("RefreshToken")[0].InnerText },
                { "Subreddit", xmlDocument.GetElementsByTagName("Subreddit")[0].InnerText }
            };

            // TODO - Replace above workaround with commented code below for all test classes after .NET Core adds support for DataSourceAttribute.  --Kris
            // https://github.com/Microsoft/testfx/issues/233
            /*return new Dictionary<string, string>
            {
                { "AppId", (string) TestContext.DataRow["AppId"] },
                { "RefreshToken", (string) TestContext.DataRow["RefreshToken"] },
                { "Subreddit", (string) TestContext.DataRow["Subreddit"] }
            };*/
        }

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
    }
}
