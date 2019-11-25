using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Controllers;
using System;

namespace RedditTests.ControllerTests.WorkflowTests
{
    [TestClass]
    public class FlairsTests : BaseTests
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

        public FlairsTests() : base() { }

        [TestMethod]
        public void CreateAndDeleteFlairTemplate()
        {
            Reddit.Things.FlairV2 linkFlair = Subreddit.Flairs.CreateLinkFlairTemplateV2("V2-" + DateTime.Now.ToString("fffffff"));
            Reddit.Things.FlairV2 userFlair = Subreddit.Flairs.CreateUserFlairTemplateV2("V2-" + DateTime.Now.ToString("fffffff"));

            Validate(linkFlair);
            Validate(userFlair);

            Subreddit.Flairs.DeleteFlairTemplate(linkFlair.Id);
            Subreddit.Flairs.DeleteFlairTemplate(userFlair.Id);
        }

        [TestMethod]
        public void SetLinkFlair()
        {
            LinkPost linkPost = Subreddit.LinkPost("Test Link Flair Post", "https://www.nuget.org/packages/Reddit").Submit(resubmit: true);

            linkPost.SetFlair("NuGet Package");
        }

        [TestMethod]
        public void SetLinkFlairWithTemplate()
        {
            LinkPost linkPost = Subreddit.LinkPost("Test Link Flair Post", "https://www.nuget.org/packages/Reddit").Submit(resubmit: true);

            // Get the available templates and use the first one that isn't empty.  --Kris
            Reddit.Things.FlairSelectorResultContainer flairSelectorResultContainer = Subreddit.Flairs.FlairSelector(link: linkPost.Fullname);
            Validate(flairSelectorResultContainer);

            string flairTemplateId = null;
            foreach (Reddit.Things.FlairSelectorResult flairSelectorResult in flairSelectorResultContainer.Choices)
            {
                if (!string.IsNullOrWhiteSpace(flairSelectorResult.FlairTemplateId))
                {
                    flairTemplateId = flairSelectorResult.FlairTemplateId;
                    break;
                }
            }

            if (string.IsNullOrWhiteSpace(flairTemplateId))
            {
                Assert.Inconclusive("Unable to find a flair template ID for this subreddit.  Please create one then retry.");
            }

            linkPost.SetFlair("NuGet Package", flairTemplateId);
        }
    }
}
