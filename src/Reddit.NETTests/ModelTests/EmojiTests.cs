using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class EmojiTests : BaseTests
    {
        [TestMethod]
        public void All()
        {
            Dictionary<string, string> testData = GetData();
            RedditAPI reddit = new RedditAPI(testData["AppId"], testData["RefreshToken"]);

            SnoomojiContainer snoomojiContainer = reddit.Models.Emoji.All("WayOfTheBern");

            Assert.IsNotNull(snoomojiContainer);
        }
    }
}
