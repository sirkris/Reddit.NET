using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs;
using Reddit.Things;
using System.Collections.Generic;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class AccountTests : BaseTests
    {
        public AccountTests() : base() { }

        [TestMethod]
        // TODO - Uncomment below and add to other TestMethods in all test classes when .NET Core adds support for this.  --Kris
        /*[DeploymentItem("Reddit.NETTests\\Reddit.NETTestsData.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
                   "|DataDirectory|\\Reddit.NETTestsData.xml",
                   "Row",
                    DataAccessMethod.Sequential)]*/
        public void Me()
        {
            User me = reddit.Models.Account.Me();

            Validate(me);
        }

        [TestMethod]
        public void Karma()
        {
            UserKarmaContainer karma = reddit.Models.Account.Karma();

            Assert.IsNotNull(karma);
        }

        [TestMethod]
        public void Prefs()
        {
            AccountPrefs prefs = reddit.Models.Account.Prefs();
            List<UserPrefsContainer> prefsFriends = reddit.Models.Account.PrefsList("friends", new CategorizedSrListingInput());
            List<UserPrefsContainer> prefsMessaging = reddit.Models.Account.PrefsList("messaging", new CategorizedSrListingInput());
            UserPrefsContainer prefsBlocked = reddit.Models.Account.PrefsSingle("blocked", new CategorizedSrListingInput());
            UserPrefsContainer prefsTrusted = reddit.Models.Account.PrefsSingle("trusted", new CategorizedSrListingInput());

            Assert.IsNotNull(prefs);
            Assert.IsNotNull(prefsFriends);
            Assert.IsNotNull(prefsBlocked);
            Assert.IsNotNull(prefsMessaging);
            Assert.IsNotNull(prefsTrusted);
        }

        [TestMethod]
        public void Trophies()
        {
            TrophyList trophies = reddit.Models.Account.Trophies();

            Assert.IsNotNull(trophies);
        }

        [TestMethod]
        public void UpdatePrefs()
        {
            // This just grabs your existing preferences and sends them right back.  --Kris
            AccountPrefs res = reddit.Models.Account.UpdatePrefs(new AccountPrefsSubmit(reddit.Models.Account.Prefs(), "US", false, ""));

            Assert.IsNotNull(res);
        }
    }
}
