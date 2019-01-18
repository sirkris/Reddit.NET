using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Exceptions;
using System;

namespace RedditTests.ControllerTests
{
    [TestClass]
    public class AccountTests : BaseTests
    {
        public AccountTests() : base() { }

        [TestMethod]
        public void Me()
        {
            Validate(reddit.Account.Me);
        }

        [TestMethod]
        public void Karma()
        {
            Validate(reddit.Account.Karma());
        }

        [TestMethod]
        public void Prefs()
        {
            Validate(reddit.Account.Prefs());
        }

        [TestMethod]
        public void UpdatePrefs()
        {
            // This just grabs your existing preferences and sends them right back.  --Kris
            Validate(reddit.Account.UpdatePrefs(new Reddit.Things.AccountPrefsSubmit(reddit.Account.Prefs(), "US", false, "")));
        }

        [TestMethod]
        public void Trophies()
        {
            Validate(reddit.Account.Trophies());
        }

        [TestMethod]
        public void Friends()
        {
            Validate(reddit.Account.Friends());
        }

        [TestMethod]
        public void Messaging()
        {
            Validate(reddit.Account.Messaging());
        }

        [TestMethod]
        public void Blocked()
        {
            Validate(reddit.Account.Blocked());
        }

        [TestMethod]
        public void Trusted()
        {
            Validate(reddit.Account.Trusted());
        }

        [TestMethod]
        public void Multis()
        {
            Validate(reddit.Account.Multis());
        }

        [TestMethod]
        public void MySubscribedSubreddits()
        {
            Validate(reddit.Account.MySubscribedSubreddits());
        }

        [TestMethod]
        public void MyContributingSubreddits()
        {
            Validate(reddit.Account.MyContributingSubreddits());
        }

        [TestMethod]
        public void MyModeratorSubreddits()
        {
            Validate(reddit.Account.MyModeratorSubreddits());
        }

        [TestMethod]
        public void MyStreamingSubreddits()
        {
            try
            {
                Validate(reddit.Account.MyStreamingSubreddits());
            }
            catch (RedditForbiddenException) { }
            catch (AggregateException ex) when (ex.InnerException is RedditForbiddenException) { }
        }

        [TestMethod]
        public void ModmailUnreadCount()
        {
            Validate(reddit.Account.ModmailUnreadCount());
        }
    }
}
