using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Exceptions;
using Reddit.Models.Inputs.Multis;
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

        private LabeledMultiContainer Rename(string multiPath, string multiPathRenamed, string multiDisplayName, bool retry = true)
        {
            try
            {
                LabeledMultiContainer labeledMultiContainer = reddit.Models.Multis.Rename(new MultiURLInput(multiDisplayName, multiPath, multiPathRenamed));

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
                        reddit.Models.Multis.Delete(multiPathRenamed, false);

                        return Rename(multiPath, multiPathRenamed, multiDisplayName, false);
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
            string multiPathCopyRenamed = "/user/" + me.Name + "/m/my_test_multi_renamed/";
            string multiDisplayName = "My Test Multi";
            string multiDescription = "Test multi created by [Reddit.NET](https://github.com/sirkris/Reddit.NET).";

            LabeledMultiContainer labeledMultiContainer = Create(multiPath, multiDisplayName, multiDescription);

            Validate(labeledMultiContainer);
            Validate(labeledMultiContainer.Data);
            Assert.AreEqual(multiPath, labeledMultiContainer.Data.Path);
            Assert.AreEqual(multiDescription, labeledMultiContainer.Data.DescriptionMd);

            LabeledMultiContainer labeledMultiContainerCopy = Copy(multiPath, multiPathCopy, multiDisplayName);

            Validate(labeledMultiContainerCopy);
            Validate(labeledMultiContainerCopy.Data);
            Assert.AreEqual(multiPathCopy, labeledMultiContainerCopy.Data.Path);

            labeledMultiContainerCopy = Rename(multiPathCopy, multiPathCopyRenamed, multiDisplayName);

            Validate(labeledMultiContainerCopy);
            Validate(labeledMultiContainerCopy.Data);
            Assert.AreEqual(multiPathCopyRenamed, labeledMultiContainerCopy.Data.Path);

            labeledMultiContainerCopy = reddit.Models.Multis.Update(multiPathCopyRenamed,
                new LabeledMultiSubmit(multiDescription, multiDisplayName, "", "",
                        new List<string> { testData["Subreddit"], "RedditDotNETBot" }, "public", "fresh"), false);

            Validate(labeledMultiContainerCopy);
            Validate(labeledMultiContainerCopy.Data);
            Assert.AreEqual(multiPathCopyRenamed, labeledMultiContainerCopy.Data.Path);

            LabeledMultiDescriptionContainer labeledMultiDescriptionContainer = reddit.Models.Multis.UpdateDescription(multiPathCopyRenamed, multiDescription);

            Validate(labeledMultiDescriptionContainer);
            Assert.AreEqual(multiDescription, labeledMultiDescriptionContainer.Data.BodyMd);

            NamedObj namedObj = reddit.Models.Multis.AddMultiSub(multiPathCopyRenamed, "WayOfTheBern");

            Validate(namedObj);
            Assert.AreEqual("WayOfTheBern", namedObj.Name);

            reddit.Models.Multis.DeleteMultiSub(multiPathCopyRenamed, "WayOfTheBern");

            reddit.Models.Multis.Delete(multiPathCopyRenamed, false);
            reddit.Models.Multis.Delete(multiPath, false);
        }
    }
}
