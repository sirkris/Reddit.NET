using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Controllers;
using Reddit.NET.Exceptions;
using RedditThings = Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class WikiTests : BaseTests
    {
        private Subreddit Subreddit
        {
            get
            {
                return subreddit ?? GetSubreddit(ref subreddit);
            }
            set
            {
                subreddit = value;
            }
        }
        private Subreddit subreddit;

        private WikiPage Index
        {
            get
            {
                return index ?? GetIndex();
            }
            set
            {
                index = value;
            }
        }
        private WikiPage index;

        public WikiTests() : base() { }

        private WikiPage GetIndex()
        {
            Index = Subreddit.Wiki.GetPage("index");
            return Index;
        }

        [TestMethod]
        public void AllowAndDenyEditor()
        {
            User patsy = GetTargetUser();

            Index.DenyEditor(patsy.Name);
            Index.AllowEditor(patsy.Name);
        }

        [TestMethod]
        public void Modify()
        {
            // Ordered by most recent first.  --Kris
            List<RedditThings.WikiPageRevision> revisions = Index.Revisions();

            Validate(revisions);
            Assert.IsTrue(revisions.Count > 0);

            // Edit an existing page.  --Kris
            Index = Index.EditAndReturn("Because I can.", "Do not fear the bots.  We are your friends.", revisions[0].Id);

            Validate(Index);

            // Hide and unhide the page.  --Kris
            Index.Hide(revisions[0].Id);
            Index.Hide(revisions[0].Id);

            // Revert to the original page version.  --Kris
            Index = Index.RevertAndReturn(revisions[revisions.Count - 1].Id);

            // Update the permissions.  --Kris
            Index.UpdatePermissions(true, 0);
        }
    }
}
