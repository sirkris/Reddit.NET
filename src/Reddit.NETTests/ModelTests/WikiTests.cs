using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Exceptions;
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
        public void Edit()
        {
            // Creates the index page if it doesn't already exist.  The edit endpoint for existing pages is tested in the corresponding workflow tests.  --Kris
            try
            {
                reddit.Models.Wiki.Edit("Lorem ipsum dolor sit amet, motherfucker.", "index", "", "Because I can.", testData["Subreddit"]);
            }
            catch (RedditConflictException) { }
        }
    }
}
