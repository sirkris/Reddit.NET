using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Inputs.Flair;
using Reddit.Things;
using System;

namespace RedditTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class FlairTests : BaseTests
    {
        public FlairTests() : base() { }

        [TestMethod]
        public void CreateAndDeleteFlairTemplate()
        {
            FlairV2 linkFlair = reddit.Models.Flair.FlairTemplateV2(
                new FlairTemplateV2Input("V2-" + DateTime.Now.ToString("fffffff"), "LINK_FLAIR", false, "dark", "#88AAFF"), testData["Subreddit"]);
            FlairV2 userFlair = reddit.Models.Flair.FlairTemplateV2(
                new FlairTemplateV2Input("V2-" + DateTime.Now.ToString("fffffff"), "USER_FLAIR", false, "dark", "#88AAFF"), testData["Subreddit"]);

            Assert.IsNotNull(linkFlair);
            Assert.IsNotNull(userFlair);

            GenericContainer resLink = reddit.Models.Flair.DeleteFlairTemplate(linkFlair.Id, testData["Subreddit"]);
            GenericContainer resUser = reddit.Models.Flair.DeleteFlairTemplate(userFlair.Id, testData["Subreddit"]);

            Validate(resLink);
            Validate(resUser);
        }
    }
}
