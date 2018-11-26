using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Controllers = Reddit.NET.Controllers;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;
using System.IO;

namespace Reddit.NETTests
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
        protected readonly RedditAPI reddit;

        public BaseTests()
        {
            testData = GetData();
            reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);
        }

        public Dictionary<string, string> GetData()
        {
            // Begin .NET Core workaround.  --Kris
            string xmlData;
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Reddit.NETTests.Reddit.NETTestsData.xml"))
            {
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(stream))
                {
                    xmlData = streamReader.ReadToEnd();
                }
            }

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.LoadXml(xmlData);

            return new Dictionary<string, string>
            {
                { "AppId", xmlDocument.GetElementsByTagName("AppId")[0].InnerText },
                { "RefreshToken", xmlDocument.GetElementsByTagName("RefreshToken")[0].InnerText },
                { "Subreddit", xmlDocument.GetElementsByTagName("Subreddit")[0].InnerText }
            };
            // End .NET Core workaround.  --Kris

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

        public void Validate(dynamic dynamic)
        {
            Assert.IsNotNull(dynamic);
        }

        public void Validate(User user)
        {
            Assert.IsNotNull(user);
            Assert.IsFalse(user.Created.Equals(default(DateTime)));
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

        public void Validate(StatusResult statusResult, bool status = true)
        {
            Assert.IsNotNull(statusResult);
            if (status)
            {
                Assert.IsTrue(statusResult.Status);
            }
            else
            {
                Assert.IsFalse(statusResult.Status);
            }
        }
    }
}
