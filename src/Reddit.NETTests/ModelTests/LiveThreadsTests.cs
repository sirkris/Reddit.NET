using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RedditTests.ModelTests
{
    [TestClass]
    public class LiveThreadsTests : BaseTests
    {
        public LiveThreadsTests() : base() { }

        // All endpoints being tested require an existing live thread, so we'll just stick them into a single workflow method until sorted tests are supported.  --Kris
    }
}
