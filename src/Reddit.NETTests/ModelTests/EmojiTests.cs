using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.Things;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class EmojiTests : BaseTests
    {
        public EmojiTests() : base() { }

        [TestMethod]
        public void All()
        {
            SnoomojiContainer snoomojiContainer = reddit.Models.Emoji.All("Pokemon");

            Validate(snoomojiContainer);

            Assert.IsTrue(snoomojiContainer.SubredditEmojis.ContainsKey("bs"));
        }
    }
}
