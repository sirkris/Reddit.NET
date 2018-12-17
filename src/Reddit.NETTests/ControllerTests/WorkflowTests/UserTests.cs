using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Controllers.EventArgs;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class UserTests : BaseTests
    {
        public UserTests() : base() { }

        [TestMethod]
        public void FriendAndUnfriend()
        {
            User patsy = reddit.User(GetTargetUserModel());

            patsy.AddRelationship("", "", "", "", 999, "+mail", "moderator_invite", testData["Subreddit"]);
            patsy.RemoveRelationship("", "moderator_invite", testData["Subreddit"]);
        }
    }
}
