using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Exceptions;
using Reddit.Inputs.Multis;
using Reddit.Things;
using System.Collections.Generic;

namespace RedditTests.ModelTests.WorkflowTests
{
    [TestClass]
    public class MultisTests : BaseTests
    {
        public MultisTests() : base() { }

        private LabeledMultiContainer Create(string multiPath, string multiDisplayName, string multiDescription, bool retry = true)
        {
            try
            {
                LabeledMultiContainer labeledMultiContainer = reddit.Models.Multis.Create(multiPath,
                    new LabeledMultiSubmit(multiDescription, multiDisplayName, "", "",
                        new List<string> { testData["Subreddit"], "RedditDotNETBot" }, "public", "classic"), false);

                return labeledMultiContainer;
            }
            catch (RedditConflictException ex)
            {
                try
                {
                    CheckBadRequest("MULTI_EXISTS", "Target multireddit cannot exist when the test begins.", ex);

                    throw ex;
                }
                catch (AssertInconclusiveException aex)
                {
                    if (retry)
                    {
                        reddit.Models.Multis.Delete(multiPath, false);

                        return Create(multiPath, multiDisplayName, multiDescription, false);
                    }
                    else
                    {
                        throw aex;
                    }
                }
            }
        }

        private LabeledMultiContainer Copy(string multiPath, string multiPathCopy, string multiDisplayName, bool retry = true)
        {
            try
            {
                LabeledMultiContainer labeledMultiContainer = reddit.Models.Multis.Copy(new MultiURLInput(multiDisplayName, multiPath, multiPathCopy));

                return labeledMultiContainer;
            }
            catch (RedditConflictException ex)
            {
                try
                {
                    CheckBadRequest("MULTI_EXISTS", "Target multireddit cannot exist when the test begins.", ex);

                    throw ex;
                }
                catch (AssertInconclusiveException aex)
                {
                    if (retry)
                    {
                        reddit.Models.Multis.Delete(multiPathCopy, false);

                        return Copy(multiPath, multiPathCopy, multiDisplayName, false);
                    }
                    else
                    {
                        throw aex;
                    }
                }
            }
        }

        [TestMethod]
        public void CreateAndDestroy()
        {
            User me = reddit.Models.Account.Me();
            string multiPath = "/user/" + me.Name + "/m/my_test_multi/";
            string multiPathCopy = "/user/" + me.Name + "/m/my_test_multi_copy/";
            string multiDisplayName = "My Test Multi";
            string multiDescription = "Test multi created by [Reddit.NET](https://github.com/sirkris/Reddit.NET).";

            LabeledMultiContainer labeledMultiContainer = Create(multiPath, multiDisplayName, multiDescription);

            Validate(labeledMultiContainer);
            Validate(labeledMultiContainer.Data);
            Assert.AreEqual(multiPath, labeledMultiContainer.Data.Path, true);
            Assert.AreEqual(multiDescription, labeledMultiContainer.Data.DescriptionMd, true);

            LabeledMultiContainer labeledMultiContainerCopy = Copy(multiPath, multiPathCopy, multiDisplayName);

            Validate(labeledMultiContainerCopy);
            Validate(labeledMultiContainerCopy.Data);
            Assert.AreEqual(multiPathCopy, labeledMultiContainerCopy.Data.Path, true);

            labeledMultiContainerCopy = reddit.Models.Multis.Update(multiPathCopy,
                new LabeledMultiSubmit(multiDescription, multiDisplayName, "", "",
                        new List<string> { testData["Subreddit"], "RedditDotNETBot" }, "public", "fresh"), false);

            Validate(labeledMultiContainerCopy);
            Validate(labeledMultiContainerCopy.Data);
            Assert.AreEqual(multiPathCopy, labeledMultiContainerCopy.Data.Path, true);

            LabeledMultiDescriptionContainer labeledMultiDescriptionContainer = reddit.Models.Multis.UpdateDescription(multiPathCopy, multiDescription);

            Validate(labeledMultiDescriptionContainer);
            Assert.AreEqual(multiDescription, labeledMultiDescriptionContainer.Data.BodyMd, true);

            NamedObj namedObj = reddit.Models.Multis.AddMultiSub(multiPathCopy, "WayOfTheBern");

            Validate(namedObj);
            Assert.AreEqual("WayOfTheBern", namedObj.Name, true);

            reddit.Models.Multis.DeleteMultiSub(multiPathCopy, "WayOfTheBern");

            reddit.Models.Multis.Delete(multiPathCopy, false);
            reddit.Models.Multis.Delete(multiPath, false);
        }
    }
}
