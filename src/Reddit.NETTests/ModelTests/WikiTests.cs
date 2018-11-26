using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using RestSharp;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class WikiTests : BaseTests
    {
        public WikiTests() : base() { }

        [TestMethod]
        public void Pages()
        {
            WikiPageListing pages = reddit.Models.Wiki.Pages("ShittyEmails");

            Assert.IsNotNull(pages);
            Assert.IsTrue(pages.Data.Count > 0);
        }

        [TestMethod]
        public void Revisions()
        {
            WikiPageRevisionContainer revisions = reddit.Models.Wiki.Revisions(null, null, "ShittyEmails");

            Assert.IsNotNull(revisions);
        }

        [TestMethod]
        public void PageRevisions()
        {
            WikiPageRevisionContainer pageRevisions = reddit.Models.Wiki.PageRevisions("index", null, null, "ShittyEmails");

            Assert.IsNotNull(pageRevisions);
            Assert.IsTrue(pageRevisions.Data.Children.Count > 0);
        }

        [TestMethod]
        public void GetPermissions()
        {
            WikiPageSettingsContainer permissions = reddit.Models.Wiki.GetPermissions("index", "ShittyEmails");

            Validate(permissions);
        }

        [TestMethod]
        public void Page()
        {
            WikiPageContainer page = reddit.Models.Wiki.Page("index", null, null, "ShittyEmails");
            WikiPageContainer pageWithV = reddit.Models.Wiki.Page("index", "51c412fc-6b26-11e8-a963-0e7fba92da48", null, "ShittyEmails");
            WikiPageContainer pageWithV2 = reddit.Models.Wiki.Page("index", "51c412fc-6b26-11e8-a963-0e7fba92da48", "483f05ca-6b26-11e8-b04f-0e02e061d980", "ShittyEmails");

            Validate(page);
            Validate(pageWithV);
            Validate(pageWithV2);
        }

        [TestMethod]
        public void AllowAndDenyEditor()
        {
            try
            {
                reddit.Models.Wiki.AllowEditor("index", "RedditDotNetBot", testData["Subreddit"]);
                reddit.Models.Wiki.DenyEditor("index", "RedditDotNetBot", testData["Subreddit"]);
            }
            catch (System.Net.WebException ex)
            {
                if (!ex.Data.Contains("res")
                    || ((IRestResponse)ex.Data["res"]).StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    throw ex;
                }
                else
                {
                    Assert.Inconclusive("Subreddit does not have a wiki.  Please create one and retest.");
                }
            }
        }

        [TestMethod]
        public void Edit()
        {
            // Creates a new page or edits an existing page.  --Kris
            reddit.Models.Wiki.Edit("Lorem ipsum dolor sit amet, motherfucker.", "index", "", "Because I can.", testData["Subreddit"]);
        }

        [TestMethod]
        public void ModifyPage()
        {
            // Ordered by most recent first.  --Kris
            WikiPageRevisionContainer revisions = reddit.Models.Wiki.PageRevisions("index", null, null, testData["Subreddit"]);

            Validate(revisions);
            Assert.IsTrue(revisions.Data.Children != null && revisions.Data.Children.Count > 0);

            // Edit an existing page.  --Kris
            reddit.Models.Wiki.Edit("There are only 10 types of people in this world:  Those who understand binary and those who don't.", "index", 
                revisions.Data.Children[0].Id, "Because I said so.", testData["Subreddit"]);

            // Hide the page.  --Kris
            StatusResult hideRes = reddit.Models.Wiki.Hide("index", revisions.Data.Children[0].Id, testData["Subreddit"]);

            // Unhide the page.  --Kris
            StatusResult unhideRes = reddit.Models.Wiki.Hide("index", revisions.Data.Children[0].Id, testData["Subreddit"]);

            Validate(hideRes);
            Validate(unhideRes, false);

            // Revert to the original page version.  --Kris
            reddit.Models.Wiki.Revert("index", revisions.Data.Children[revisions.Data.Children.Count - 1].Id, testData["Subreddit"]);

            // Update the permissions.  --Kris
            WikiPageSettingsContainer res = reddit.Models.Wiki.UpdatePermissions("index", true, 0, testData["Subreddit"]);

            Validate(res);
        }
    }
}
