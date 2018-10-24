using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class WikiTests : BaseTests
    {
        [TestMethod]
        public void Pages()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            WikiPageListing pages = reddit.Models.Wiki.Pages("ShittyEmails");

            Assert.IsNotNull(pages);
            Assert.IsTrue(pages.Data.Count > 0);
        }

        [TestMethod]
        public void Revisions()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            WikiPageRevisionContainer revisions = reddit.Models.Wiki.Revisions(null, null, "ShittyEmails");

            Assert.IsNotNull(revisions);
            Assert.IsTrue(revisions.Data.Children.Count > 0);
        }

        [TestMethod]
        public void PageRevisions()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            WikiPageRevisionContainer pageRevisions = reddit.Models.Wiki.PageRevisions("index", null, null, "ShittyEmails");

            Assert.IsNotNull(pageRevisions);
            Assert.IsTrue(pageRevisions.Data.Children.Count > 0);
        }

        [TestMethod]
        public void GetPermissions()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            WikiPageSettingsContainer permissions = reddit.Models.Wiki.GetPermissions("index", "ShittyEmails");

            Assert.IsNotNull(permissions);
            Assert.IsNotNull(permissions.Data);
        }

        [TestMethod]
        public void Page()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            WikiPageContainer page = reddit.Models.Wiki.Page("index", null, null, "ShittyEmails");
            WikiPageContainer pageWithV = reddit.Models.Wiki.Page("index", "51c412fc-6b26-11e8-a963-0e7fba92da48", null, "ShittyEmails");
            WikiPageContainer pageWithV2 = reddit.Models.Wiki.Page("index", "51c412fc-6b26-11e8-a963-0e7fba92da48", "483f05ca-6b26-11e8-b04f-0e02e061d980", "ShittyEmails");

            Assert.IsNotNull(page);
            Assert.IsNotNull(page.Data);
            Assert.IsNotNull(pageWithV);
            Assert.IsNotNull(pageWithV.Data);
            Assert.IsNotNull(pageWithV2);
            Assert.IsNotNull(pageWithV2.Data);
        }
    }
}
