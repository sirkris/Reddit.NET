using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class FlairTests : BaseTests
    {
        public FlairTests() : base() { }

        [TestMethod]
        public void CreateAndDeleteFlairTemplate()
        {
            FlairV2 linkFlair = reddit.Models.Flair.FlairTemplateV2("#88AAFF", "", "LINK_FLAIR", false,
                    "V2-" + DateTime.Now.ToString("fffffff"), "dark", false, testData["Subreddit"]);
            FlairV2 userFlair = reddit.Models.Flair.FlairTemplateV2("#88AAFF", "", "USER_FLAIR", false,
                    "V2-" + DateTime.Now.ToString("fffffff"), "dark", false, testData["Subreddit"]);

            Assert.IsNotNull(linkFlair);
            Assert.IsNotNull(userFlair);

            GenericContainer resLink = reddit.Models.Flair.DeleteFlairTemplate(linkFlair.Id, testData["Subreddit"]);
            GenericContainer resUser = reddit.Models.Flair.DeleteFlairTemplate(userFlair.Id, testData["Subreddit"]);

            Validate(resLink);
            Validate(resUser);
        }
    }
}
