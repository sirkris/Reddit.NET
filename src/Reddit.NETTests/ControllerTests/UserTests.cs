using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using Reddit.Exceptions;
using Things = Reddit.Things;
using System;

namespace RedditTests.ControllerTests
{
    [TestClass]
    public class UserTests : BaseTests
    {
        private User User
        {
            get
            {
                return user ?? GetUser();
            }
            set
            {
                user = value;
            }
        }
        private User user;

        public UserTests() : base() { }

        private User GetUser()
        {
            User = reddit.Account.Me;
            return User;
        }

        [TestMethod]
        public void About()
        {
            Validate(User);
            Assert.AreEqual(User.Name, User.About().Name);
        }

        [TestMethod]
        public void SetPermissions()
        {
            SetPermissions(true);
        }

        private void SetPermissions(bool retry)
        {
            try
            {
                reddit.User("RedditDotNetBot").SetPermissions(testData["Subreddit"], "+wiki", "moderator_invite");
            }
            catch (Exception ex) when (ex is RedditInvalidPermissionTypeException || ex is RedditInternalServerErrorException)
            {
                if (retry)
                {
                    InviteMod();
                }
                else
                {
                    throw ex;
                }
            }
        }

        private void InviteMod()
        {
            reddit.User("RedditDotNetBot").AddRelationship(null, null, null, null, 999, "+mail", "moderator_invite", testData["Subreddit"]);
            SetPermissions(false);
        }

        [TestMethod]
        public void UsernameAvailable()
        {
            Assert.IsFalse(User.UsernameAvailable().Value);
        }

        [TestMethod]
        public void Trophies()
        {
            Validate(User.Trophies());
        }

        [TestMethod]
        public void ModeratedSubreddits()
        {
            Validate(reddit.User("KrisCraig").ModeratedSubreddits);
            bool found = false;
            foreach (Things.ModeratedListItem moderatedListItem in reddit.User("KrisCraig").ModeratedSubreddits)
            {
                if (moderatedListItem.Name.Equals("t5_3fblp"))
                {
                    found = true;
                    break;
                }
            }

            Assert.IsTrue(found);
        }

        [TestMethod]
        public void Overview()
        {
            Validate(User.Overview);
        }

        [TestMethod]
        public void PostHistory()
        {
            Validate(User.GetPostHistory());
        }

        [TestMethod]
        public void CommentHistory()
        {
            Validate(User.GetCommentHistory());
        }

        [TestMethod]
        public void DeleteFlair()
        {
            User.DeleteFlair(testData["Subreddit"]);
        }

        [TestMethod]
        public void CreateFlair()
        {
            User.CreateFlair(testData["Subreddit"], "Robot");
        }

        [TestMethod]
        public void FlairList()
        {
            Validate(User.FlairList(testData["Subreddit"]));
        }

        [TestMethod]
        public void FlairSelector()
        {
            Validate(User.FlairSelector(testData["Subreddit"]));
        }

        [TestMethod]
        public void Multis()
        {
            Validate(User.Multis());
        }

        [TestMethod]
        public void DenyWikiEdit()
        {
            reddit.User("RedditDotNetBot").DenyWikiEdit("index", testData["Subreddit"]);
        }

        [TestMethod]
        public void AllowWikiEdit()
        {
            reddit.User("RedditDotNetBot").AllowWikiEdit("index", testData["Subreddit"]);
        }

        [TestMethod]
        public void Block()
        {
            reddit.User("michaelconfoy").Block();
        }
    }
}
