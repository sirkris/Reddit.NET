using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using Reddit.Controllers.EventArgs;
using System;
using System.Collections.Generic;

namespace RedditTests.ControllerTests.WorkflowTests
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

        private Dictionary<string, bool> Pages;
        private bool PageUpdated;

        public WikiTests() : base()
        {
            Pages = new Dictionary<string, bool>();
            PageUpdated = false;
        }

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
            List<Reddit.Things.WikiPageRevision> revisions = Index.Revisions();

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

            // Create a new wiki page.  --Kris
            WikiPage myTestPage = Subreddit.Wiki.Page("TestPage" + DateTime.Now.ToString("yyyyMMddHHmmssfffff"))
                                    .CreateAndReturn("Because I have a god complex.", "This is the content of my test page.");

            Validate(myTestPage);

            // Update the permissions.  --Kris
            myTestPage.UpdatePermissions(true, 0);
        }

        [TestMethod]
        public void MonitorPages()
        {
            Subreddit.Wiki.GetPages();  // This call prevents any existing wiki pages from triggering the update event.  --Kris
            Subreddit.Wiki.MonitorPages();
            Subreddit.Wiki.PagesUpdated += C_PagesUpdated;

            for (int i = 1; i <= 10; i++)
            {
                // Despite what VS says, we don't want to use await here.  --Kris
                Subreddit.Wiki.Page("Test Page " + DateTime.Now.ToString("yyyyMMddHHmmssfffff") + "-" + i.ToString()).CreateAsync("None of your business.", "This is a test.");
            }

            DateTime start = DateTime.Now;
            while (Pages.Count < 10
                && start.AddMinutes(1) > DateTime.Now) { }

            Subreddit.Wiki.PagesUpdated -= C_PagesUpdated;
            Subreddit.Wiki.MonitorPages();

            Assert.AreEqual(10, Pages.Count);
        }

        [TestMethod]
        public void MonitorPage()
        {
            // Initialize so the page will be different.  --Kris
            Index.Edit("Initialization", "This page is about to be updated.", Index.Revisions()[0].Id);

            Index.MonitorPage();
            Index.PageUpdated += C_PageUpdated;

            // Despite what VS says, we don't want to use await here.  --Kris
            Index.EditAsync("You wouldn't understand.", "This page has been updated.", Index.Revisions()[0].Id);

            DateTime start = DateTime.Now;
            while (!PageUpdated
                && start.AddMinutes(1) > DateTime.Now) { }

            Index.PageUpdated -= C_PageUpdated;
            Index.MonitorPage();

            Assert.IsTrue(PageUpdated);
        }

        // When a new wiki page is detected in MonitorPages, this method will add it/them to the list.  --Kris
        private void C_PagesUpdated(object sender, WikiPagesUpdateEventArgs e)
        {
            foreach (string page in e.Added)
            {
                if (!Pages.ContainsKey(page))
                {
                    Pages.Add(page, true);
                }
            }
        }

        private void C_PageUpdated(object sender, WikiPageUpdateEventArgs e)
        {
            PageUpdated = true;
        }
    }
}
