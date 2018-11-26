using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET;
using Reddit.NET.Models.Structures;
using System.Collections.Generic;

namespace Reddit.NETTests.ModelTests
{
    [TestClass]
    public class EmojiTests : BaseTests
    {
        public EmojiTests() : base() { }

        [TestMethod]
        public void All()
        {
            SnoomojiContainer snoomojiContainer = reddit.Models.Emoji.All("WayOfTheBern");

            Assert.IsNotNull(snoomojiContainer);
        }
    }
}
