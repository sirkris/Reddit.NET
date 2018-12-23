using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reddit.NET.Models.Structures;

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

            Validate(snoomojiContainer);
        }
    }
}
