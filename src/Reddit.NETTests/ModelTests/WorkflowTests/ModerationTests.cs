using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using RestSharp;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class ModerationTests : BaseTests
    {
        public ModerationTests() : base() { }

        // Requires existing subreddit with mod privilages.  --Kris
        [TestMethod]
        public void Approve()
        {
            Post post = reddit.Models.Listings.New(null, null, true, testData["Subreddit"]).Data.Children[0].Data;

            reddit.Models.Moderation.Approve(post.Name);

            post = reddit.Models.LinksAndComments.Info(post.Name).Posts[0];

            Assert.IsNotNull(post);
            Assert.IsTrue(post.Approved);
        }
    }
}
