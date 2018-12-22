using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET.Controllers;
using RedditThings = Reddit.NET.Models.Structures;
using System;

namespace Reddit.NETTests.ControllerTests.WorkflowTests
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
            RedditThings.FlairV2 linkFlair = Subreddit.Flairs.CreateLinkFlairTemplateV2("V2-" + DateTime.Now.ToString("fffffff"));
            RedditThings.FlairV2 userFlair = Subreddit.Flairs.CreateUserFlairTemplateV2("V2-" + DateTime.Now.ToString("fffffff"));

            Validate(linkFlair);
            Validate(userFlair);

            Subreddit.Flairs.DeleteFlairTemplate(linkFlair.Id);
            Subreddit.Flairs.DeleteFlairTemplate(userFlair.Id);
        }
    }
}
