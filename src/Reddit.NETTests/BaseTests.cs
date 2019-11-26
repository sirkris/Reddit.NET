using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Reddit;
using Controllers = Reddit.Controllers;
using Reddit.Inputs.LinksAndComments;
using Reddit.Things;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace RedditTests
{
    public abstract class BaseTests
    {
        private TestContext TestContextInstance;

        public TestContext TestContext
        {
            get
            {
                return TestContextInstance;
            }
            set
            {
                TestContextInstance = value;
            }
        }

        protected readonly Dictionary<string, string> testData;
        protected readonly RedditClient reddit;
        protected readonly RedditClient reddit2;
        protected readonly RedditClient reddit3;

        public BaseTests()
        {
            testData = GetData();

            // Primary test user's instance.  --Kris
            reddit = new RedditClient(testData["AppId"], testData["RefreshToken"], userAgent: "Reddit.NETTests");

            try
            {
                // Secondary test user's instance.  --Kris
                reddit2 = new RedditClient(testData["AppId"], testData["RefreshToken2"], userAgent: "Reddit.NETTests");
            }
            catch (Exception) { }

            // App-only instance.  --Kris
            reddit3 = new RedditClient(testData["AppId"], userAgent: "Reddit.NETTests");
        }

        public Dictionary<string, string> GetData()
        {
            // Begin .NET Core workaround.  --Kris
            string xmlData;
            using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Reddit.NETTests.Reddit.NETTestsData.xml"))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    xmlData = streamReader.ReadToEnd();
                }
            }

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.LoadXml(xmlData);

            Dictionary<string, string> res = new Dictionary<string, string>
            {
                { "AppId", xmlDocument.GetElementsByTagName("AppId")[0].InnerText },
                { "RefreshToken", xmlDocument.GetElementsByTagName("RefreshToken")[0].InnerText },
                { "RefreshToken2", xmlDocument.GetElementsByTagName("RefreshToken2")[0].InnerText },
                { "Subreddit", xmlDocument.GetElementsByTagName("Subreddit")[0].InnerText }
            };
            // End .NET Core workaround.  --Kris

            if (res["AppId"].Equals("Paste Reddit App ID here")
                || res["RefreshToken"].Equals("Paste Reddit Refresh Token here")
                || res["RefreshToken2"].Equals("Paste second account's Reddit Refresh Token here (required for WorkflowTests)")
                || res["Subreddit"].Equals("Paste test subreddit (new or existing with full mod privs) here"))
            {
                Assert.Inconclusive("You must replace all default values in Reddit.NETTestsData.xml before running the tests.");
            }

            return res;

            // TODO - Replace above workaround with commented code below for all test classes after .NET Core adds support for DataSourceAttribute.  --Kris
            // https://github.com/Microsoft/testfx/issues/233
            /*return new Dictionary<string, string>
            {
                { "AppId", (string) TestContext.DataRow["AppId"] },
                { "RefreshToken", (string) TestContext.DataRow["RefreshToken"] },
                { "Subreddit", (string) TestContext.DataRow["Subreddit"] }
            };*/
        }

        protected byte[] GetResourceFile(string filename)
        {
            using (Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Reddit.NETTests.Resources." + filename))
            {
                using (BinaryReader binaryReader = new BinaryReader(stream))
                {
                    return binaryReader.ReadBytes(int.MaxValue / 2);
                }
            }
        }

        public PostResultShortContainer TestPost()
        {
            return reddit.Models.LinksAndComments.Submit(new LinksAndCommentsSubmitInput(false, "", "", "", "",
                    "link", false, true, null, true, false, testData["Subreddit"], "",
                    "UPDATE:  As of " + DateTime.Now.ToString("f") + ", she's still looking into it....", "http://iwilllookintoit.com/", null));
        }

        public CommentResultContainer TestComment(string parentFullname)
        {
            return reddit.Models.LinksAndComments.Comment<CommentResultContainer>(new LinksAndCommentsThingInput("This is a test comment.  So there.", parentFullname));
        }

        public CommentResultContainer TestCommentReply(string parentFullname)
        {
            return reddit.Models.LinksAndComments.Comment<CommentResultContainer>(new LinksAndCommentsThingInput("This is a reply to a test comment.", parentFullname));
        }

        /// <summary>
        /// Retrieves your test subreddit.  It is assumed that the subreddit already exists at this point.
        /// </summary>
        /// <returns>The populated Subreddit data.</returns>
        protected Controllers.Subreddit GetSubreddit(ref Controllers.Subreddit subreddit)
        {
            subreddit = reddit.Subreddit(testData["Subreddit"]).About();
            return subreddit;
        }

        /// <summary>
        /// Retrieves your secondary test user.
        /// </summary>
        /// <returns>The populated User data.</returns>
        protected Controllers.User GetTargetUser()
        {
            return reddit2.Account.Me;
        }

        /// <summary>
        /// Retrieves your secondary test user.
        /// </summary>
        /// <returns>The populated User model structure data.</returns>
        protected User GetTargetUserModel()
        {
            return reddit2.Models.Account.Me();
        }

        protected void CheckBadRequest(string reason, string message, Exception ex)
        {
            BadRequest badRequest = null;
            try
            {
                badRequest = JsonConvert.DeserializeObject<BadRequest>(((RestResponse)ex.Data["res"]).Content);
            }
            catch (Exception) { }

            if (badRequest == null
                || !badRequest.Reason.Equals(reason))
            {
                throw ex;
            }
            else
            {
                Assert.Inconclusive(message);
            }
        }

        public void Validate(dynamic dynamic)
        {
            Assert.IsNotNull(dynamic);
        }

        public OverviewContainer Validate(OverviewContainer overviewContainer, int minResults = 1)
        {
            Assert.IsNotNull(overviewContainer);
            Assert.IsNotNull(overviewContainer.Data);
            Assert.IsNotNull(overviewContainer.Data.Children);
            Assert.IsTrue(overviewContainer.Data.Children.Count >= minResults);

            // Each CommentOrPost entry should have exactly one comment or one post-- not neither and not both.  --Kris
            foreach (CommentOrPost commentOrPost in overviewContainer.Data.Children)
            {
                Assert.IsTrue(commentOrPost.Comment != null || commentOrPost.Post != null);
                Assert.IsTrue(commentOrPost.Comment == null || commentOrPost.Post == null);
            }

            return overviewContainer;
        }

        public SubredditContainer Validate(SubredditContainer subredditContainer, int minResults)
        {
            Assert.IsNotNull(subredditContainer);
            Assert.IsTrue(subredditContainer.Data.Children.Count >= minResults);

            return subredditContainer;
        }

        public PostContainer Validate(PostContainer postContainer, int minResults)
        {
            Assert.IsNotNull(postContainer);
            Assert.IsTrue(postContainer.Data.Children.Count >= minResults);

            return postContainer;
        }

        public UserContainer Validate(UserContainer userContainer, int minResults)
        {
            Assert.IsNotNull(userContainer);
            Assert.IsTrue(userContainer.Data.Children.Count >= minResults);

            return userContainer;
        }

        public Controllers.LiveThread Validate(Controllers.LiveThread liveThread)
        {
            Assert.IsNotNull(liveThread);
            Assert.IsNotNull(liveThread.Id);

            return liveThread;
        }

        public void Validate(ModmailConversationContainer modmailConversationContainer)
        {
            Assert.IsNotNull(modmailConversationContainer);
            Assert.IsNotNull(modmailConversationContainer.Conversation);
            Assert.IsFalse(modmailConversationContainer.Conversation.LastUpdated.Equals(default(DateTime)));
            Assert.IsNotNull(modmailConversationContainer.Messages);
            Assert.IsTrue(modmailConversationContainer.Messages.Count > 0);
        }

        public void Validate(User user)
        {
            Assert.IsNotNull(user);
            Assert.IsFalse(user.CreatedUTC.Equals(default(DateTime)));
            Assert.IsFalse(string.IsNullOrWhiteSpace(user.Name));
        }

        public void Validate(Controllers.User user)
        {
            Assert.IsNotNull(user);
            Assert.IsFalse(user.Created.Equals(default(DateTime)));
            Assert.IsFalse(string.IsNullOrWhiteSpace(user.Name));

            Validate(user.UserData);
        }

        public void Validate(GenericContainer genericContainer)
        {
            Assert.IsNotNull(genericContainer);
            Validate(genericContainer.JSON);
        }

        public void Validate(Generic generic)
        {
            Assert.IsNotNull(generic);
            Assert.IsTrue(generic.Errors.Count == 0);
        }

        public void Validate(PostResultShortContainer postResultShortContainer)
        {
            Assert.IsNotNull(postResultShortContainer);
            Assert.IsNotNull(postResultShortContainer.JSON);
            Assert.IsNotNull(postResultShortContainer.JSON.Data);
        }

        public void Validate(CommentResultContainer commentResultContainer)
        {
            Assert.IsNotNull(commentResultContainer);
            Assert.IsNotNull(commentResultContainer.JSON);
            Assert.IsNotNull(commentResultContainer.JSON.Data);
            Assert.IsNotNull(commentResultContainer.JSON.Data.Things);
            Assert.IsTrue(commentResultContainer.JSON.Data.Things.Count > 0);
            Assert.IsNotNull(commentResultContainer.JSON.Data.Things[0].Data);
        }

        public void Validate(PostResultContainer postResultContainer)
        {
            Assert.IsNotNull(postResultContainer);
            Assert.IsNotNull(postResultContainer.JSON);
            Assert.IsNotNull(postResultContainer.JSON.Data);
            Assert.IsNotNull(postResultContainer.JSON.Data.Things);
        }

        public void Validate(JQueryReturn jQueryReturn)
        {
            Assert.IsNotNull(jQueryReturn);
            Assert.IsTrue(jQueryReturn.Success);
        }

        public void Validate(MoreChildren moreChildren)
        {
            Assert.IsNotNull(moreChildren);
            Assert.IsNotNull(moreChildren.Comments);
            Assert.IsNotNull(moreChildren.MoreData);
        }

        public void Validate(PostContainer postContainer, bool allowEmpty = false)
        {
            Assert.IsNotNull(postContainer);
            Assert.IsNotNull(postContainer.Data);
            Assert.IsNotNull(postContainer.Data.Children);
            Assert.IsTrue(postContainer.Kind.Equals("Listing"));

            if (!allowEmpty)
            {
                Assert.IsTrue(postContainer.Data.Children.Count > 0);
                Assert.IsTrue(postContainer.Data.Children[0].Kind.Equals("t3"));
                Assert.IsNotNull(postContainer.Data.Children[0].Data);
            }
        }

        public void Validate(CommentContainer commentContainer, bool allowEmpty = false)
        {
            Assert.IsNotNull(commentContainer);
            Assert.IsNotNull(commentContainer.Data);
            Assert.IsNotNull(commentContainer.Data.Children);
            Assert.IsTrue(commentContainer.Kind.Equals("Listing"));

            if (!allowEmpty)
            {
                Assert.IsTrue(commentContainer.Data.Children.Count > 0);
                Assert.IsTrue(commentContainer.Data.Children[0].Kind.Equals("t1"));
                Assert.IsNotNull(commentContainer.Data.Children[0].Data);
            }
        }

        public void Validate(LiveThreadCreateResultContainer liveThreadCreateResultContainer)
        {
            Assert.IsNotNull(liveThreadCreateResultContainer);
            Assert.IsNotNull(liveThreadCreateResultContainer.JSON);
            Assert.IsNotNull(liveThreadCreateResultContainer.JSON.Data);
            Assert.IsNotNull(liveThreadCreateResultContainer.JSON.Data.Id);
            Assert.IsTrue(liveThreadCreateResultContainer.JSON.Errors == null || liveThreadCreateResultContainer.JSON.Errors.Count == 0);
        }

        public void Validate(LiveUpdateEventContainer liveUpdateEventContainer)
        {
            Assert.IsNotNull(liveUpdateEventContainer);
            Assert.IsNotNull(liveUpdateEventContainer.Data);
        }

        public void Validate(LiveUpdateContainer liveUpdateContainer)
        {
            Assert.IsNotNull(liveUpdateContainer);
            Assert.IsNotNull(liveUpdateContainer.Data);
            Assert.IsNotNull(liveUpdateContainer.Data.Children);
        }

        public void Validate(SubredditNames subredditNames)
        {
            Assert.IsNotNull(subredditNames);
            Assert.IsNotNull(subredditNames.Names);
            Assert.IsTrue(subredditNames.Names.Count > 0);
        }

        public void Validate(SubSearch subSearch)
        {
            Assert.IsNotNull(subSearch);
            Assert.IsNotNull(subSearch.Subreddits);
            Assert.IsTrue(subSearch.Subreddits.Count > 0);
        }

        public void Validate(SubredditSettingsContainer subredditSettingsContainer)
        {
            Assert.IsNotNull(subredditSettingsContainer);
            Assert.IsNotNull(subredditSettingsContainer.Data);
        }

        public void Validate(ImageUploadResult imageUploadResult)
        {
            Assert.IsNotNull(imageUploadResult);
            Assert.IsTrue(imageUploadResult.Errors == null || imageUploadResult.Errors.Count == 0);
            Assert.IsTrue(imageUploadResult.ErrorsValues == null || imageUploadResult.ErrorsValues.Count == 0);
            Assert.IsFalse(string.IsNullOrWhiteSpace(imageUploadResult.ImgSrc));
        }

        public void Validate(WidgetResults widgetResults)
        {
            Assert.IsNotNull(widgetResults);
            Assert.IsNotNull(widgetResults.Items);
        }

        public void Validate(WikiPageSettingsContainer wikiPageSettingsContainer)
        {
            Assert.IsNotNull(wikiPageSettingsContainer);
            Assert.IsNotNull(wikiPageSettingsContainer.Data);
        }

        public void Validate(WikiPageContainer wikiPageContainer)
        {
            Assert.IsNotNull(wikiPageContainer);
            Assert.IsNotNull(wikiPageContainer.Data);
        }

        public void Validate(WikiPageRevisionContainer wikiPageRevisionContainer)
        {
            Assert.IsNotNull(wikiPageRevisionContainer);
            Assert.IsNotNull(wikiPageRevisionContainer.Data);
        }

        public void Validate(List<ActionResult> actionResults, int minCount = 0)
        {
            Assert.IsNotNull(actionResults);
            Assert.IsTrue(actionResults.Count >= minCount);

            foreach (ActionResult actionResult in actionResults)
            {
                Validate(actionResult);
            }
        }

        public void Validate(ActionResult actionResult)
        {
            Assert.IsNotNull(actionResult);
            Assert.IsTrue(actionResult.Ok);
        }
    }
}
