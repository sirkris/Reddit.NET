using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Things;
using RestSharp;

namespace RedditTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class WikiTests : BaseTests
    {
        public WikiTests() : base() { }

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
